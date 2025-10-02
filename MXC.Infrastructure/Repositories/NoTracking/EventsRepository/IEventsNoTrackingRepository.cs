using MXC.Domain.DataTransferObjects.EventManagement;
using MXC.Domain.Entities;
using MXC.Infrastructure.Models;
using MXC.Infrastructure.Repositories.RepositoryBase.NoTrackingRepositoryBase;

namespace MXC.Infrastructure.Repositories.NoTracking.EventsRepository;

public interface IEventsNoTrackingRepository : INoTrackingRepositoryBase<EventEntity>
{
    Task<EventItemDTO?> FindEventItemById(int eventId, CancellationToken cancellationToken);

    IQueryable<EventManagementModel> GetEventForEventManagements();

    Task<ICollection<EventManagementItemDTO>> GetEventManagementItems(IQueryable<EventManagementModel> events, int pageNumber, int itemsOnPage, CancellationToken cancellationToken);
}