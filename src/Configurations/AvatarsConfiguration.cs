using System.Collections.Generic;

namespace LS.Avatars.Api.Configurations;

public record AvatarsConfiguration
{
    public IList<ImageConfiguration> Dimensions { get; set; } = new List<ImageConfiguration>();
}