using MXC.Infrastructure.Context;
using MXC.Shared;

namespace MXC.Infrastructure.Configuration;

public static class SeedDataConfiguration
{
    public static void SeedData(ApplicationTrackingDbContext applicationTrackingDbContext)
    {
        Ensure.NotNull(applicationTrackingDbContext);

        new SeedData.EventEntityConfiguration().SeedData(applicationTrackingDbContext);
    }
}