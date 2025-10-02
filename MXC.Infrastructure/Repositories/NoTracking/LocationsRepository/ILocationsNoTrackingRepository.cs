using MXC.Domain.DataTransferObjects.EventManagement;
using MXC.Domain.Entities;
using MXC.Infrastructure.Repositories.RepositoryBase.NoTrackingRepositoryBase;

namespace MXC.Infrastructure.Repositories.NoTracking.LocationsRepository;

public interface ILocationsNoTrackingRepository : INoTrackingRepositoryBase<LocationEntity>
{
    IQueryable<LocationEntity> GetAllLocation();

    Task<IReadOnlyCollection<EventLocationDTO>> GetEventLocations(IQueryable<LocationEntity> searchQuery, CancellationToken cancellationToken);
}