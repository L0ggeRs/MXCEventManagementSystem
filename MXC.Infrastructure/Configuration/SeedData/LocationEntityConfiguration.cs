using MXC.Domain.Entities;
using MXC.Infrastructure.Context;
using MXC.Shared;

namespace MXC.Infrastructure.Configuration.SeedData;

internal static class LocationEntityConfiguration
{
    internal static void SeedData(ApplicationTrackingDbContext applicationTrackingDbContext)
    {
        Ensure.NotNull(applicationTrackingDbContext);

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

        applicationTrackingDbContext
            .AddRange(eventLocations
                .Select(el => new LocationEntity()
                {
                    LocationName = el
                }));
    }
}