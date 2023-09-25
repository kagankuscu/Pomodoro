using PomodoroRacer.Backend.Common.Application.Services;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System.Reflection;

namespace PomodoroRacer.Backend.Common.Application;

public static class ApplicationBaseConfiguration
{
    public static IServiceCollection AddApplicationBase(
        this IServiceCollection services,
        IConfiguration configuration,
        Assembly assembly)
        => services
            .AddAutoMapper(Assembly.GetExecutingAssembly(), assembly)
            .Configure<ApplicationSettings>(configuration.GetSection("ApplicationSettings"),
                options => options.BindNonPublicProperties = true)
            .AddMediatR(cfg =>
            {
            });

    public static IServiceCollection AddConventionalServices(
        this IServiceCollection services,
        Assembly assembly)
    {
        var serviceInterfaceType = typeof(ITransientService);
        var singletonServiceInterfaceType = typeof(ISingletonService);
        var scopedServiceInterfaceType = typeof(IScopedService);

        var types = assembly
            .GetExportedTypes()
            .Where(t => t is {IsClass: true, IsAbstract: false})
            .Select(t => new
            {
                Service = t.GetInterface($"I{t.Name.Replace("Service", string.Empty)}"),
                Implementation = t
            })
            .Where(t => t.Service != null);

        foreach (var type in types)
        {
            if (serviceInterfaceType.IsAssignableFrom(type.Service))
            {
                services.AddTransient(type.Service, type.Implementation);
            }
            else if (singletonServiceInterfaceType.IsAssignableFrom(type.Service))
            {
                services.AddSingleton(type.Service, type.Implementation);
            }
            else if (scopedServiceInterfaceType.IsAssignableFrom(type.Service))
            {
                services.AddScoped(type.Service, type.Implementation);
            }
        }

        return services;
    }
}