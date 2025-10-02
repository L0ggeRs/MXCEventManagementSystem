using MXC.Domain.Entities;
using MXC.Infrastructure.Context;
using MXC.Shared;
using System.Security.Cryptography;

namespace MXC.Infrastructure.Configuration.SeedData;

internal static class EventEntityConfiguration
{
    internal static void SeedData(ApplicationTrackingDbContext applicationTrackingDbContext)
    {
        Ensure.NotNull(applicationTrackingDbContext);

        IList<string> eventNames =
        [
            "Bebury Park",
            "Trughtcote Avenue",
            "Apathampton Manor.",
            "Wrecote Lake",
            "Bunthold",
            "Gradstan",
            "Pustan Way",
            "Blithampdale",
            "Plodon Park",
            "Flumore Tye"
        ];
        IList<int> capacities =
        [
            7720,
            19900,
            5474,
            8673,
            19043,
            1961,
            15839,
            908,
            19758,
            2246
        ];

        var count = 1000;
        var eventEntities = new List<EventEntity>(count);
        var locations = applicationTrackingDbContext.Locations.Local.ToList();
        var countries = applicationTrackingDbContext.Countries.Local.ToList();

        foreach (var i in Enumerable.Range(0, count))
        {
            var capacity = (uint)capacities[RandomNumberGenerator.GetInt32(0, capacities.Count)];

            eventEntities.Add(new EventEntity()
            {
                EventName = eventNames[RandomNumberGenerator.GetInt32(0, eventNames.Count)],
                Location = locations[RandomNumberGenerator.GetInt32(0, locations.Count)],
                Country = countries[RandomNumberGenerator.GetInt32(0, countries.Count)],
                Capacity = capacity % 2 == 0 ? capacity : null
            });
        }

        applicationTrackingDbContext.AddRange(eventEntities);
    }
}