namespace LS.Avatars.Api.Exceptions;

public class BadConfigurationException : BaseException
{
    public override ErrorTypes ErrorType => ErrorTypes.BadConfiguration;

    public override string Message { get; }

    public BadConfigurationException(string config, string description)
    {
        Message = $"Smth wrong with config {config}: {description}";
    }
}