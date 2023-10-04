using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using PomodoroRacer.Backend.Common.Web;

namespace PomodoroRacer.Backend.Web;

public static class WebConfiguration
{
    public static IServiceCollection AddWeb(this IServiceCollection services)
    {
        services.AddCommonWeb(Assembly.GetExecutingAssembly());

        return services;
    }
}