using MXC.Domain.Entities;
using MXC.Infrastructure.Context;
using MXC.Shared;

namespace MXC.Infrastructure.Configuration.SeedData;

internal class EventEntityConfiguration
{
    internal void SeedData(ApplicationTrackingDbContext applicationTrackingDbContext)
    {
        Ensure.NotNull(applicationTrackingDbContext);

        applicationTrackingDbContext.AddRange(new List<EventEntity>()
        {
            new EventEntity()
            {
                EventName = "Bebury Park",
                EventLocation = "Alexandria",
                Country = "Egypt",
                Capacity = 7720
            },
            new EventEntity()
            {
                EventName = "Trughtcote Avenue",
                EventLocation = "Caloocan",
                Country = "Philippines",
                Capacity = 19900
            },
            new EventEntity()
            {
                EventName = "Bunthold",
                EventLocation = "Mashhad",
                Country = "Iran",
                Capacity = 19043
            },
            new EventEntity()
            {
                EventName = "Plodon Park",
                EventLocation = "Chennai",
                Capacity = 19758
            },
        });
    }
}