using Microsoft.EntityFrameworkCore;
using MXC.Domain.DataTransferObjects.EventManagement;
using MXC.Domain.Entities;
using MXC.Infrastructure.Context;
using MXC.Infrastructure.Models;
using MXC.Infrastructure.Repositories.RepositoryBase.NoTrackingRepositoryBase;

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

    public IQueryable<EventManagementModel> GetEventForEventManagements()
    {
        return FindAll()
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
    }

    public async Task<ICollection<EventManagementItemDTO>> GetEventManagementItems(IQueryable<EventManagementModel> events, int pageNumber, int itemsOnPage, CancellationToken cancellationToken)
    {
        return await events
            .Select(sq => new EventManagementItemDTO()
            {
                EventId = sq.EventId,
                EventName = sq.EventName,
                Place = $"{sq.LocationName}, {sq.CountryName}",
                Capacity = sq.Capacity
            })
            .Skip(pageNumber * itemsOnPage)
            .Take(itemsOnPage)
            .ToListAsync(cancellationToken);
    }
}