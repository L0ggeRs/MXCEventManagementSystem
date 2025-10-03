using Microsoft.EntityFrameworkCore;
using MXC.Domain.DataTransferObjects.Common;
using MXC.Domain.DataTransferObjects.EventManagement;
using MXC.Domain.Entities;
using MXC.Domain.Enums;
using MXC.Infrastructure.Context;
using MXC.Infrastructure.Models;
using MXC.Infrastructure.Repositories.RepositoryBase.NoTrackingRepositoryBase;
using MXC.Shared;

namespace MXC.Infrastructure.Repositories.NoTracking.EventsRepository;

public class EventsNoTrackingRepository(ApplicationNoTrackingDbContext context) : NoTrackingRepositoryBase<EventEntity>(context), IEventsNoTrackingRepository
{
    public async Task<EventItemDTO?> FindEventItemById(int eventId, CancellationToken cancellationToken)
    {
        return await FindByCondition(e => e.Id == eventId)
            .Select(e => new EventItemDTO()
            {
                EventId = e.Id,
                EventName = e.EventName,
                Location = new EventLocationDTO()
                {
                    LocationId = e.LocationId,
                    LocationName = e.Location.LocationName
                },
                Country = e.CountryId.HasValue
                    ? new EventCountryDTO()
                    {
                        CountryId = e.CountryId.Value,
                        CountryName = e.Country!.CountryName
                    }
                    : null,
                Capacity = e.Capacity
            })
            .SingleOrDefaultAsync(cancellationToken);
    }

    public async Task<PaginationWrapperDTO<EventManagementItemDTO>> GetEventManagementItems(EventManagementFilterDTO eventManagementFilter, CancellationToken cancellationToken)
    {
        Ensure.NotNull(eventManagementFilter);

        var isAscending = eventManagementFilter.OrderDirection == OrderDirection.Asc;
        var searchQuery = FindAll()
            .Select(e => new EventManagementModel()
            {
                EventId = e.Id,
                EventName = e.EventName,
                LocationId = e.LocationId,
                Capacity = e.Capacity,
                LocationName = e.Location.LocationName,
                CountryName = e.Country != null ? e.Country.CountryName : string.Empty
            })
            .AsQueryable();

        searchQuery = eventManagementFilter.EventManagementOrderBy switch
        {
            EventManagementOrderBy.EventName => isAscending
                ? searchQuery.OrderBy(e => e.EventName)
                : searchQuery.OrderByDescending(e => e.EventName),
            EventManagementOrderBy.EventLocation => isAscending
                ? searchQuery.OrderBy(e => e.LocationId)
                : searchQuery.OrderByDescending(e => e.LocationId),
            EventManagementOrderBy.Capacity => isAscending
                ? searchQuery.OrderBy(e => e.Capacity)
                : searchQuery.OrderByDescending(e => e.Capacity),
            _ => isAscending ? searchQuery.OrderBy(e => e.EventName) : searchQuery.OrderByDescending(e => e.EventName)
        };

        var searchResultCount = await searchQuery.CountAsync(cancellationToken);
        var items = await searchQuery
            .Select(x => new EventManagementItemDTO()
            {
                EventId = x.EventId,
                EventName = x.EventName,
                Place = $"{x.LocationName}, {x.CountryName}",
                Capacity = x.Capacity
            })
            .Skip(eventManagementFilter.PageNumber * eventManagementFilter.ItemsOnPage)
            .Take(eventManagementFilter.ItemsOnPage)
            .ToListAsync(cancellationToken);

        return new PaginationWrapperDTO<EventManagementItemDTO>()
        {
            ItemCount = searchResultCount,
            Items = items,
            PageNumber = eventManagementFilter.PageNumber,
            ItemsOnPage = eventManagementFilter.ItemsOnPage
        };
    }
}