using System;

namespace LS.Avatars.Api.Exceptions.Configurations;

public class ConfigurationException : ApplicationException
{
    public ConfigurationException(string message) : base(message)
    { }
}