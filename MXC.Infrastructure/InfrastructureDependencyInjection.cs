using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MXC.Infrastructure.Configuration;
using MXC.Infrastructure.Context;
using MXC.Infrastructure.Repositories.NoTracking.EventsRepository;

namespace MXC.Infrastructure;

public static class InfrastructureDependencyInjection
{
    public static void AddInfrastructure(
        this IServiceCollection services,
        ConfigurationManager configurationManager)
    {
        addContext(services, configurationManager);
        addRepositories(services);
    }

    private static void addRepositories(IServiceCollection services)
    {
        services.Scan(scan => scan
            .FromAssemblies(typeof(IEventsNoTrackingRepository).Assembly)
            .AddClasses()
            .AsImplementedInterfaces()
            .WithScopedLifetime());
    }

    private static void addContext(IServiceCollection services, ConfigurationManager configurationManager)
    {
        services.AddDbContext<ApplicationTrackingDbContext>(options => options
            .UseSqlServer(configurationManager.GetConnectionString("MXC"))
            .UseAsyncSeeding(async (context, _, cancellationToken) =>
            {
                SeedDataConfiguration.SeedData((ApplicationTrackingDbContext)context);
                await context.SaveChangesAsync(cancellationToken);
            })
            .UseQueryTrackingBehavior(QueryTrackingBehavior.TrackAll)
            .ConfigureWarnings(warnings => warnings.Log(RelationalEventId.PendingModelChangesWarning)));

        services.AddScoped<ApplicationTrackingDbContext>();

        services.AddDbContext<ApplicationNoTrackingDbContext>(options => options
            .UseSqlServer(configurationManager.GetConnectionString("MXC"))
            .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking));

        services.AddScoped<ApplicationNoTrackingDbContext>();
    }
}