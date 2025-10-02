using Microsoft.EntityFrameworkCore;
using MXC.Domain.Entities;
using MXC.Infrastructure.Context;
using MXC.Infrastructure.Repositories.RepositoryBase.TrackingRepositoryBase;

namespace MXC.Infrastructure.Repositories.Tracking.EventsRepository;

public class EventsTrackingRepository(ApplicationTrackingDbContext context) : TrackingRepositoryBase<EventEntity>(context), IEventsTrackingRepository
{
    public async Task<EventEntity?> FindEventById(int eventId, CancellationToken cancellationToken)
    {
        return await FindByCondition(e => e.Id == eventId)
            .SingleOrDefaultAsync(cancellationToken);
    }
}