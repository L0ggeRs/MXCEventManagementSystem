using MXC.Domain.Entities;
using MXC.Infrastructure.Repositories.RepositoryBase.TrackingRepositoryBase;

namespace MXC.Infrastructure.Repositories.Tracking.EventsRepository;

public interface IEventsTrackingRepository : ITrackingRepositoryBase<EventEntity>
{
    Task<EventEntity?> FindEventById(int eventId, CancellationToken cancellationToken);
}