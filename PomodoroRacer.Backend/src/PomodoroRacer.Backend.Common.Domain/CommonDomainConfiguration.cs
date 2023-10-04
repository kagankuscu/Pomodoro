using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using PomodoroRacer.Backend.Common.Domain.Contracts;

namespace PomodoroRacer.Backend.Common.Domain;

public static class CommonDomainConfiguration
{
    public static IServiceCollection AddCommonDomain(
        this IServiceCollection services,
        Assembly assembly)
    {
        services
            .Scan(scan => scan
                .FromAssemblies(assembly)
                .AddClasses(classes => classes
                    .AssignableTo(typeof(IFactory<>)))
                .AsMatchingInterface()
                .WithTransientLifetime())
            .Scan(scan => scan
                .FromAssemblies(assembly)
                .AddClasses(classes => classes
                    .AssignableTo(typeof(IInitialData)))
                .AsImplementedInterfaces()
                .WithTransientLifetime())
            .Scan(scan => scan
                .FromAssemblies(assembly)
                .AddClasses(classes => classes
                    .AssignableTo(typeof(IDomainService)))
                .AsImplementedInterfaces()
                .WithTransientLifetime());

        return services;
    }
}