using MXC.Domain.DataTransferObjects.Common;
using MXC.Domain.DataTransferObjects.EventManagement;
using MXC.Domain.Entities;
using MXC.Infrastructure.Repositories.RepositoryBase.NoTrackingRepositoryBase;

namespace MXC.Infrastructure.Repositories.NoTracking.EventsRepository;

public interface IEventsNoTrackingRepository : INoTrackingRepositoryBase<EventEntity>
{
    Task<EventItemDTO?> FindEventItemById(int eventId, CancellationToken cancellationToken);

    Task<PaginationWrapperDTO<EventManagementItemDTO>> GetEventManagementItems(EventManagementFilterDTO eventManagementFilter, CancellationToken cancellationToken);
}