using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using PomodoroRacer.Backend.Common.Infrastructure;

namespace PomodoroRacer.Backend.Infrastructure;

public static class InfrastructureConfiguration
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services)
    {
        services.AddCommonInfrastructure(Assembly.GetExecutingAssembly());

        return services;
    }
}