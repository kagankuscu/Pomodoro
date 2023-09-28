using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace PomodoroRacer.Backend.Common.Web;

public static class CommonWebConfiguration
{
    public static IServiceCollection AddCommonWeb(this IServiceCollection services, Assembly assembly)
    {
        return services;
    }
}