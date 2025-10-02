using MXC.Domain.Entities;
using MXC.Infrastructure.Context;
using MXC.Shared;

namespace MXC.Infrastructure.Configuration.SeedData;

internal static class CountryEntityConfiguration
{
    internal static void SeedData(ApplicationTrackingDbContext applicationTrackingDbContext)
    {
        Ensure.NotNull(applicationTrackingDbContext);

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

        applicationTrackingDbContext
            .AddRange(countries
                .Select(el => new CountryEntity()
                {
                    CountryName = el
                }));
    }
}