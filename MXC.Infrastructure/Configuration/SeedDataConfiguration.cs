using MXC.Infrastructure.Configuration.SeedData;
using MXC.Infrastructure.Context;
using MXC.Shared;

namespace MXC.Infrastructure.Configuration;

public static class SeedDataConfiguration
{
    public static void SeedData(ApplicationTrackingDbContext applicationTrackingDbContext)
    {
        Ensure.NotNull(applicationTrackingDbContext);

        EventEntityConfiguration.SeedData(applicationTrackingDbContext);
    }
}