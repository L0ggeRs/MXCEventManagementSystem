using MXC.Domain.DataTransferObjects.Common;
using MXC.Domain.DataTransferObjects.EventManagement;
using MXC.Domain.DataTransferObjects.Location;
using MXC.Domain.Entities;
using MXC.Infrastructure.Repositories.RepositoryBase.NoTrackingRepositoryBase;

namespace MXC.Infrastructure.Repositories.NoTracking.LocationsRepository;

public interface ILocationsNoTrackingRepository : INoTrackingRepositoryBase<LocationEntity>
{
    Task<PaginationWrapperDTO<LocationManagementItemDTO>> FindLocationsForManagement(LocationItemFilterDTO locationItemFilterDTO, CancellationToken cancellationToken);

    Task<IReadOnlyCollection<EventLocationDTO>> GetEventLocations(SearchTextDTO searchText, CancellationToken cancellationToken);
}