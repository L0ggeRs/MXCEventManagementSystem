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
        IList<string> eventLocations =
        [
            "Alexandria",
            "Caloocan",
            "Guadalajara",
            "Vijayawada",
            "Mashhad",
            "Osaka",
            "Algiers",
            "Prague",
            "Chennai",
            "Dubai"
        ];
        IList<string> countries =
        [
            "Egypt",
            "Philippines",
            "Mexico",
            "India",
            "Iran",
            "Japan",
            "Algeria",
            "Czech Republic",
            "India",
            "United Arab Emirates"
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

        foreach (var i in Enumerable.Range(0, count))
        {
            var capacity = (uint)capacities[RandomNumberGenerator.GetInt32(0, capacities.Count)];

            eventEntities.Add(new EventEntity()
            {
                EventName = eventNames[RandomNumberGenerator.GetInt32(0, eventNames.Count)],
                EventLocation = eventLocations[RandomNumberGenerator.GetInt32(0, eventLocations.Count)],
                Country = countries[RandomNumberGenerator.GetInt32(0, countries.Count)],
                Capacity = capacity % 2 == 0 ? capacity : null
            });
        }

        applicationTrackingDbContext.AddRange(eventEntities);
    }
}