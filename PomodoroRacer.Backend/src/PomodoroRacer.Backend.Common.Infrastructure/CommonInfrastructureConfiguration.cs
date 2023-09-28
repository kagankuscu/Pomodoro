using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace PomodoroRacer.Backend.Common.Infrastructure;

public static class CommonInfrastructureConfiguration
{
    public static IServiceCollection AddCommonInfrastructure(
        this IServiceCollection services,
        Assembly assembly)
    {
        return services;
    }
}