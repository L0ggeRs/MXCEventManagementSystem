using Microsoft.EntityFrameworkCore;
using MXC.Application.Services.EventManagementService;
using MXC.Application.Validators.EventManagement;
using MXC.Domain.DataTransferObjects;
using MXC.Domain.DataTransferObjects.EventManagement;
using MXC.Domain.Entities;
using MXC.Domain.Enums;
using MXC.Infrastructure.Context;
using MXC.Shared.Enum;
using MXC.Shared.ResultType;

namespace MXC.WebApi.Services.EventManagementService;

internal class EventManagementService(
    ApplicationNoTrackingDbContext noTrackingDbContext,
    ApplicationTrackingDbContext trackingDbContext,
    IEventManagementValidators eventManagementValidators,
    ILogger<EventManagementService> logger) : IEventManagementService
{
    /// <summary>
    /// Creates a new event in the database.
    /// </summary>
    public async Task<Result> CreateEventItem(EventItemCreateDTO eventItemCreate, CancellationToken cancellationToken)
    {
        if (eventItemCreate is null)
        {
            logger.LogError("{MethodName} failed: eventItemCreate DTO is null.", nameof(CreateEventItem));
            return Result.Failure(ErrorType.NotSet);
        }

        var validation = await eventManagementValidators.EventCreateValidator.ValidateAsync(eventItemCreate, cancellationToken);

        if (!validation.IsValid)
        {
            var errorMessages = validation.Errors.Select(e => e.ErrorMessage);

            logger.LogError("{MethodName} failed: validation errors: {Errors}", nameof(CreateEventItem), string.Join(", ", errorMessages));
            return Result.Failure(ErrorType.Validation, errorMessages);
        }

        trackingDbContext.Events.Add(new EventEntity()
        {
            EventName = eventItemCreate.EventName,
            EventLocation = eventItemCreate.Location,
            Country = eventItemCreate.Country,
            Capacity = eventItemCreate.Capacity
        });

        await trackingDbContext.SaveChangesAsync(cancellationToken);
        logger.LogInformation("{MethodName} succeeded: event '{EventName}' created.", nameof(CreateEventItem), eventItemCreate.EventName);
        return Result.Success();
    }

    /// <summary>
    /// Updates an existing event in the database.
    /// </summary>
    public async Task<Result> UpdateEventItem(EventItemDTO eventItem, CancellationToken cancellationToken)
    {
        if (eventItem is null)
        {
            logger.LogError("{MethodName} failed: eventItem DTO is null.", nameof(UpdateEventItem));
            return Result.Failure(ErrorType.NotSet);
        }

        var validation = await eventManagementValidators.EventUpdateValidator.ValidateAsync(eventItem, cancellationToken);

        if (!validation.IsValid)
        {
            var errorMessages = validation.Errors.Select(e => e.ErrorMessage);

            logger.LogError("{MethodName} failed: validation errors: {Errors}", nameof(UpdateEventItem), string.Join(", ", errorMessages));
            return Result.Failure(ErrorType.Validation, errorMessages);
        }

        var item = await trackingDbContext.Events.SingleOrDefaultAsync(e => e.Id == eventItem.EventId, cancellationToken);

        if (item is null)
        {
            logger.LogError("{MethodName} failed: event with ID {EventId} not found.", nameof(UpdateEventItem), eventItem.EventId);
            return Result.Failure(ErrorType.NotFound);
        }

        item.EventName = eventItem.EventName;
        item.EventLocation = eventItem.Location;
        item.Country = eventItem.Country;
        item.Capacity = eventItem.Capacity;

        await trackingDbContext.SaveChangesAsync(cancellationToken);
        logger.LogInformation("{MethodName} succeeded: event with ID {EventId} updated.", nameof(UpdateEventItem), eventItem.EventId);
        return Result.Success();
    }

    /// <summary>
    /// Deletes an event from the database.
    /// </summary>
    public async Task<Result> DeleteEventItem(int eventId, CancellationToken cancellationToken)
    {
        var item = await trackingDbContext.Events.SingleOrDefaultAsync(e => e.Id == eventId, cancellationToken);

        if (item is null)
        {
            logger.LogError("{MethodName} failed: event with ID {EventId} not found.", nameof(DeleteEventItem), eventId);
            return Result.Failure(ErrorType.NotFound);
        }

        trackingDbContext.Events.Remove(item);
        await trackingDbContext.SaveChangesAsync(cancellationToken);
        logger.LogInformation("{MethodName} succeeded: event with ID {EventId} deleted.", nameof(DeleteEventItem), eventId);
        return Result.Success();
    }

    /// <summary>
    /// Retrieves the details of an event.
    /// </summary>
    public async Task<Result<EventItemDTO>> GetEventItemByEventId(int eventId, CancellationToken cancellationToken)
    {
        var eventItem = await noTrackingDbContext.Events
            .Where(e => e.Id == eventId)
            .Select(e => new EventItemDTO()
            {
                EventId = e.Id,
                EventName = e.EventName,
                Location = e.EventLocation,
                Country = e.Country,
                Capacity = e.Capacity
            })
            .SingleOrDefaultAsync(cancellationToken);

        if (eventItem is null)
        {
            logger.LogError("{MethodName} failed: event with ID {EventId} not found.", nameof(GetEventItemByEventId), eventId);
            return Result<EventItemDTO>.Failure(ErrorType.NotFound);
        }

        logger.LogInformation("{MethodName} succeeded: event with ID {EventId} retrieved.", nameof(GetEventItemByEventId), eventId);
        return Result<EventItemDTO>.Success(eventItem);
    }

    /// <summary>
    /// Retrieves a paginated list of events based on filtering and ordering criteria.
    /// </summary>
    public async Task<Result<PaginationWrapperDTO<EventManagementItemDTO>>> GetEventManagementItems(EventManagementFilterDTO eventManagementFilter, CancellationToken cancellationToken)
    {
        if (eventManagementFilter is null)
        {
            logger.LogError("{MethodName} failed: eventManagementFilter DTO is null.", nameof(GetEventManagementItems));
            return Result<PaginationWrapperDTO<EventManagementItemDTO>>.Failure(ErrorType.NotSet);
        }

        var isAscending = eventManagementFilter.OrderDirection == OrderDirection.Asc;
        var searchQuery = noTrackingDbContext.Events.AsQueryable();

        searchQuery = eventManagementFilter.EventManagementOrderBy switch
        {
            EventManagementOrderBy.EventName => isAscending
                ? searchQuery.OrderBy(e => e.EventName)
                : searchQuery.OrderByDescending(e => e.EventName),
            EventManagementOrderBy.EventLocation => isAscending
                ? searchQuery.OrderBy(e => e.EventLocation)
                : searchQuery.OrderByDescending(e => e.EventLocation),
            EventManagementOrderBy.Capacity => isAscending
                ? searchQuery.OrderBy(e => e.Capacity)
                : searchQuery.OrderByDescending(e => e.Capacity),
            _ => isAscending ? searchQuery.OrderBy(e => e.EventName) : searchQuery.OrderByDescending(e => e.EventName)
        };

        var searchResultCount = await searchQuery.CountAsync(cancellationToken);
        var eventItems = await searchQuery
            .Select(sq => new EventManagementItemDTO()
            {
                EventId = sq.Id,
                EventName = sq.EventName,
                Place = $"{sq.EventLocation}, {sq.Country ?? string.Empty}",
                Capacity = sq.Capacity
            })
            .Skip(eventManagementFilter.PageNumber * eventManagementFilter.ItemsOnPage)
            .Take(eventManagementFilter.ItemsOnPage)
            .ToListAsync(cancellationToken);

        logger.LogInformation(
            "{MethodName} succeeded: retrieved {ItemCount} items for page {PageNumber} (items per page: {ItemsOnPage}).",
            nameof(GetEventManagementItems),
            eventItems.Count,
            eventManagementFilter.PageNumber,
            eventManagementFilter.ItemsOnPage);

        return Result<PaginationWrapperDTO<EventManagementItemDTO>>.Success(new PaginationWrapperDTO<EventManagementItemDTO>()
        {
            Items = eventItems,
            ItemCount = searchResultCount,
            ItemsOnPage = eventManagementFilter.ItemsOnPage,
            PageNumber = eventManagementFilter.PageNumber
        });
    }
}