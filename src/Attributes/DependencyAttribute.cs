using System;
using Microsoft.Extensions.DependencyInjection;

namespace LS.Avatars.Api.Attributes;

[AttributeUsage(AttributeTargets.Class)]
public class DependencyAttribute : Attribute
{
    public DependencyAttribute(ServiceLifetime lifetime = ServiceLifetime.Scoped, Type? baseType = null)
    {
        Lifetime = lifetime;
        Interface = baseType;
    }

    public ServiceLifetime Lifetime { get; }

    public Type? Interface { get; }
}
