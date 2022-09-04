using System.Linq;
using LS.Avatars.Api.Configurations;
using LS.Avatars.Api.Exceptions.Configurations;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace LS.Avatars.Api.Services;

public static class IHostExtensions
{
    public static IHost CheckConfiguration(this IHost host)
    {
        using var scope = host.Services.CreateScope();
        var avatarsConfigurationOptions = scope.ServiceProvider.GetRequiredService<IOptions<AvatarsConfiguration>>();
        var avatarsConfiguration = avatarsConfigurationOptions.Value;
        if (!avatarsConfiguration.Dimensions.Any())
        {
            throw new ConfigurationException("Not found dimensions. Please set at least one");
        }

        var keyDuplicates = avatarsConfiguration.Dimensions.GroupBy(x => x.Key)
                      .Where(g => g.Count() > 1)
                      .Select(y => y.Key)
                      .ToList();
        if (keyDuplicates.Any())
        {
            var keys = string.Join(", ", keyDuplicates);
            throw new ConfigurationException($"Found duplicates : {keys}. Please change it.");
        }
        return host;
    }
}