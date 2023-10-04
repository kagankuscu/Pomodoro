using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using PomodoroRacer.Backend.Common.Domain;

namespace PomodoroRacer.Backend.Domain;

public static class DomainConfiguration
{
    public static IServiceCollection AddDomain(
        this IServiceCollection services)
    {
        services.AddCommonDomain(Assembly.GetExecutingAssembly());

        return services;
    }
}