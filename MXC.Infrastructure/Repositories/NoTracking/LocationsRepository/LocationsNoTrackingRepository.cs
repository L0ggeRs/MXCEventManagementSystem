using Microsoft.EntityFrameworkCore;
using MXC.Domain.DataTransferObjects.EventManagement;
using MXC.Domain.Entities;
using MXC.Infrastructure.Context;
using MXC.Infrastructure.Repositories.RepositoryBase.NoTrackingRepositoryBase;

namespace MXC.Infrastructure.Repositories.NoTracking.LocationsRepository;

public class LocationsNoTrackingRepository(ApplicationNoTrackingDbContext context) : NoTrackingRepositoryBase<LocationEntity>(context), ILocationsNoTrackingRepository
{
    public IQueryable<LocationEntity> GetAllLocation()
    {
        return FindAll()
            .AsQueryable();
    }

    public async Task<IReadOnlyCollection<EventLocationDTO>> GetEventLocations(IQueryable<LocationEntity> searchQuery, CancellationToken cancellationToken)
    {
        return await searchQuery
            .Select(x => new EventLocationDTO()
            {
                LocationId = x.Id,
                LocationName = x.LocationName
            })
            .OrderBy(x => x.LocationName)
            .ToListAsync(cancellationToken);
    }
}