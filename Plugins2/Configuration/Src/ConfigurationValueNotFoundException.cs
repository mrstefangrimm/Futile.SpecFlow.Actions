using System;

namespace Futile.SpecFlow.Actions.Configuration;

public class ConfigurationValueNotFoundException : Exception
{
    public ConfigurationValueNotFoundException(string path) : base($"No configuration value found for path {path}")
    {
    }
}