using Microsoft.EntityFrameworkCore;
using MXC.Domain.DataTransferObjects.Common;
using MXC.Domain.DataTransferObjects.EventManagement;
using MXC.Domain.DataTransferObjects.Location;
using MXC.Domain.Entities;
using MXC.Domain.Enums;
using MXC.Infrastructure.Context;
using MXC.Infrastructure.Repositories.RepositoryBase.NoTrackingRepositoryBase;
using MXC.Shared;

namespace MXC.Infrastructure.Repositories.NoTracking.LocationsRepository;

public class LocationsNoTrackingRepository(ApplicationNoTrackingDbContext context) : NoTrackingRepositoryBase<LocationEntity>(context), ILocationsNoTrackingRepository
{
    public async Task<IReadOnlyCollection<EventLocationDTO>> GetEventLocations(SearchTextDTO searchText, CancellationToken cancellationToken)
    {
        Ensure.NotNull(searchText);

        var searchQuery = FindAll()
            .AsQueryable();

        if (!string.IsNullOrEmpty(searchText.SearchText))
        {
            searchQuery = searchQuery.Where(x => x.LocationName.Contains(searchText.SearchText));
        }

        return await searchQuery
            .Select(x => new EventLocationDTO()
            {
                LocationId = x.Id,
                LocationName = x.LocationName
            })
            .OrderBy(x => x.LocationName)
            .ToListAsync(cancellationToken);
    }

    public async Task<PaginationWrapperDTO<LocationManagementItemDTO>> FindLocationsForManagement(LocationItemFilterDTO locationItemFilterDTO, CancellationToken cancellationToken)
    {
        Ensure.NotNull(locationItemFilterDTO);

        var isAscending = locationItemFilterDTO.OrderDirection == OrderDirection.Asc;
        var searchQuery = FindByCondition(c => c.LocationName.Contains(locationItemFilterDTO.SearchText))
            .Select(c => new LocationManagementItemDTO()
            {
                LocationId = c.Id,
                LocationName = c.LocationName
            })
            .AsQueryable();

        searchQuery = isAscending ? searchQuery.OrderBy(x => x.LocationName) : searchQuery.OrderByDescending(x => x.LocationName);

        var searchResultCount = await searchQuery.CountAsync(cancellationToken);
        var items = await searchQuery
            .Skip(locationItemFilterDTO.PageNumber * locationItemFilterDTO.ItemsOnPage)
            .Take(locationItemFilterDTO.ItemsOnPage)
            .ToListAsync(cancellationToken);

        return new PaginationWrapperDTO<LocationManagementItemDTO>()
        {
            ItemCount = searchResultCount,
            Items = items,
            PageNumber = locationItemFilterDTO.PageNumber,
            ItemsOnPage = locationItemFilterDTO.ItemsOnPage
        };
    }
}