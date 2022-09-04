namespace LS.Avatars.Api.Configurations;

public record ImageConfiguration
{
    public string Key { get; init; } = null!;

    public int Width { get; init; }

    public int Height { get; init; }
}