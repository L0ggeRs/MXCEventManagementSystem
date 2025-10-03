using Microsoft.Extensions.DependencyInjection;
using MXC.Application.Services.EventManagementService;
using MXC.Application.Validators.EventManagement;
using Scrutor;

namespace MXC.Application;

public static class InfrastructureDependencyInjection
{
    public static void AddApplication(this IServiceCollection services)
    {
        addServices(services);
        addValidators(services);
    }

    private static void addServices(IServiceCollection services)
    {
        services.Scan(scan => scan
            .FromAssemblyOf<IEventManagementService>()
            .AddClasses(classes => classes.InNamespaces("MXC.Application.Services"))
            .UsingRegistrationStrategy(RegistrationStrategy.Skip)
            .AsImplementedInterfaces()
            .WithScopedLifetime());
    }

    private static void addValidators(IServiceCollection services)
    {
        services.Scan(scan => scan
            .FromAssemblyOf<IEventManagementValidators>()
            .AddClasses(classes => classes.InNamespaces("MXC.Application.Validators"))
            .UsingRegistrationStrategy(RegistrationStrategy.Skip)
            .AsImplementedInterfaces()
            .WithScopedLifetime());
    }
}