using Microsoft.Extensions.Logging;
using MXC.Application.Validators.EventManagement;
using MXC.Domain.DataTransferObjects.Common;
using MXC.Domain.DataTransferObjects.EventManagement;
using MXC.Domain.Entities;
using MXC.Infrastructure.Repositories.NoTracking.CountriesRepository;
using MXC.Infrastructure.Repositories.NoTracking.EventsRepository;
using MXC.Infrastructure.Repositories.NoTracking.LocationsRepository;
using MXC.Infrastructure.Repositories.Tracking.EventsRepository;
using MXC.Shared.Enum;
using MXC.Shared.ResultType;

namespace MXC.Application.Services.EventManagementService;

public class EventManagementService(
    IEventsTrackingRepository eventsTrackingRepository,
    IEventsNoTrackingRepository eventsNoTrackingRepository,
    ICountriesNoTrackingRepository countriesNoTrackingRepository,
    ILocationsNoTrackingRepository locationsNoTrackingRepository,
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

        eventsTrackingRepository.Create(new EventEntity()
        {
            EventName = eventItemCreate.EventName,
            LocationId = eventItemCreate.LocationId,
            CountryId = eventItemCreate.CountryId,
            Capacity = eventItemCreate.Capacity
        });

        await eventsTrackingRepository.SaveAsync(cancellationToken);
        logger.LogInformation("{MethodName} succeeded: event '{EventName}' created.", nameof(CreateEventItem), eventItemCreate.EventName);
        return Result.Success();
    }

    /// <summary>
    /// Updates an existing event in the database.
    /// </summary>
    public async Task<Result> UpdateEventItem(EventItemUpdateDTO eventItem, CancellationToken cancellationToken)
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

        var eventEntity = await eventsTrackingRepository.FindEventById(eventItem.EventId, cancellationToken);

        if (eventEntity is null)
        {
            logger.LogError("{MethodName} failed: event with ID {EventId} not found.", nameof(UpdateEventItem), eventItem.EventId);
            return Result.Failure(ErrorType.NotFound);
        }

        eventEntity.EventName = eventItem.EventName;
        eventEntity.LocationId = eventItem.LocationId;
        eventEntity.CountryId = eventItem.CountryId;
        eventEntity.Capacity = eventItem.Capacity;

        await eventsTrackingRepository.SaveAsync(cancellationToken);
        logger.LogInformation("{MethodName} succeeded: event with ID {EventId} updated.", nameof(UpdateEventItem), eventItem.EventId);
        return Result.Success();
    }

    /// <summary>
    /// Deletes an event from the database.
    /// </summary>
    public async Task<Result> DeleteEventItem(int eventId, CancellationToken cancellationToken)
    {
        var eventItem = await eventsTrackingRepository.FindEventById(eventId, cancellationToken);

        if (eventItem is null)
        {
            logger.LogError("{MethodName} failed: event with ID {EventId} not found.", nameof(DeleteEventItem), eventId);
            return Result.Failure(ErrorType.NotFound);
        }

        eventsTrackingRepository.Delete(eventItem);

        await eventsTrackingRepository.SaveAsync(cancellationToken);
        logger.LogInformation("{MethodName} succeeded: event with ID {EventId} deleted.", nameof(DeleteEventItem), eventId);
        return Result.Success();
    }

    /// <summary>
    /// Retrieves the details of an event.
    /// </summary>
    public async Task<Result<EventItemDTO>> GetEventItemByEventId(int eventId, CancellationToken cancellationToken)
    {
        var eventItem = await eventsNoTrackingRepository.FindEventItemById(eventId, cancellationToken);

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

        var eventItems = await eventsNoTrackingRepository.GetEventManagementItems(eventManagementFilter, cancellationToken);

        logger.LogInformation(
            "{MethodName} succeeded: retrieved {ItemCount} items for page {PageNumber} (items per page: {ItemsOnPage}).",
            nameof(GetEventManagementItems),
            eventItems.ItemCount,
            eventManagementFilter.PageNumber,
            eventManagementFilter.ItemsOnPage);

        return Result<PaginationWrapperDTO<EventManagementItemDTO>>.Success(eventItems);
    }

    /// <summary>
    /// Filters the countries from the database
    /// </summary>
    public async Task<Result<IReadOnlyCollection<EventCountryDTO>>> FilterCountries(SearchTextDTO searchText, CancellationToken cancellationToken)
    {
        if (searchText is null)
        {
            return Result<IReadOnlyCollection<EventCountryDTO>>.Failure(ErrorType.NotSet);
        }

        var result = await countriesNoTrackingRepository.GetEventCountries(searchText, cancellationToken);

        return Result<IReadOnlyCollection<EventCountryDTO>>.Success(result);
    }

    /// <summary>
    /// Filters the locations from the database
    /// </summary>
    public async Task<Result<IReadOnlyCollection<EventLocationDTO>>> FilterLocations(SearchTextDTO searchText, CancellationToken cancellationToken)
    {
        if (searchText is null)
        {
            return Result<IReadOnlyCollection<EventLocationDTO>>.Failure(ErrorType.NotSet);
        }

        var result = await locationsNoTrackingRepository.GetEventLocations(searchText, cancellationToken);

        return Result<IReadOnlyCollection<EventLocationDTO>>.Success(result);
    }
}