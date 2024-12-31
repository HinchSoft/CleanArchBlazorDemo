
using CommonCore.Services;
using Microsoft.AspNetCore.Builder;
using System.Reflection;

#pragma warning disable IDE0130 // Namespace does not match folder structure
namespace Microsoft.Extensions.DependencyInjection;
#pragma warning restore IDE0130 // Namespace does not match folder structure

public static class ServiceExtensions
{
    public static IServiceCollection AddApiPagination(this IServiceCollection services)
    {
        services.AddScoped<IPageInfoProvider, PageInfoProvider>();
        services.AddScoped(typeof(PaginationService<>));

        return services;
    }

    public static WebApplicationBuilder AddApiPagination(this WebApplicationBuilder builder,string configName="Pagination")
    {
        builder.Services.Configure<PaginationOptions>(builder.Configuration.GetSection(configName));
        builder.Services.AddApiPagination();
        return builder;
    }
}
