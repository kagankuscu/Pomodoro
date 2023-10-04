using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PomodoroRacer.Backend.Common.Application;

namespace PomodoroRacer.Backend.Application;

public static class ApplicationConfiguration
{
    public static IServiceCollection AddApplication(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddCommonApplication(configuration, Assembly.GetExecutingAssembly());
        return services;
    }
}