using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace PomodoroRacer.Backend.Common.Startup;

public static class CommonStartupConfiguration
{
    public static IServiceCollection AddCommonStartup(
        this IServiceCollection services,
        Assembly assembly)
    {
        return services;
    }
}