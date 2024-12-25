
using CommonCore.Services;
using System.Reflection;

namespace Microsoft.Extensions.DependencyInjection;

public static class ServiceExtensions
{
    public static IServiceCollection AddMapping(this IServiceCollection services,params Assembly[] assemblies)
    {
        if (assemblies is null || assemblies.Length == 0)
            assemblies = new[] { Assembly.GetCallingAssembly() };

        MapService.LoadMappers(assemblies);

        services.AddSingleton(typeof(MapService<>));

        return services;
    }
}
