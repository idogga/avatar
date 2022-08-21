using System;
using System.Linq;
using LS.Avatars.Api.Attributes;
using Microsoft.Extensions.DependencyInjection;

namespace LS.Avatars.Api.Services;
public static class IServiceCollectionExtensions
{
    public static IServiceCollection AddDIServices(this IServiceCollection services)
    {
        foreach (var type in AppDomain.CurrentDomain.GetAssemblies().SelectMany(a => a.GetTypes()))
        {
            if (type.GetCustomAttributes(typeof(DependencyAttribute), true).Length <= 0)
            {
                continue;
            }

            var attribute = type.GetCustomAttributes(typeof(DependencyAttribute), true)
                .Cast<DependencyAttribute>()
                .First();
            var baseType = attribute.Interface ?? type;
            var descriptor = new ServiceDescriptor(baseType, type, attribute.Lifetime);
            services.Add(descriptor);
        }

        return services;
    }
}
