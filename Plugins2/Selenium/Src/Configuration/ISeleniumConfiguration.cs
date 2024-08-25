using System.Collections.Generic;

namespace Futile.SpecFlow.Actions.Selenium.Configuration;

public interface ISeleniumConfiguration
{
    Browser Browser { get; }

    string[] Arguments { get; }

    Dictionary<string, string> Capabilities { get; }

    double? DefaultTimeout { get; }

    double? PollingInterval { get; }
}