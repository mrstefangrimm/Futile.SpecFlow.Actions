using Futile.SpecFlow.Actions.Configuration;
using System;
using System.Collections.Generic;

namespace Futile.SpecFlow.Actions.Selenium.Configuration;

public class SeleniumConfiguration : ISeleniumConfiguration
{
    private readonly ISpecFlowActionsConfiguration _specFlowActionsConfiguration;

    public SeleniumConfiguration(ISpecFlowActionsConfiguration specFlowActionsConfiguration)
    {
        _specFlowActionsConfiguration = specFlowActionsConfiguration;
    }

    public Browser Browser => (Browser)Enum.Parse(typeof(Browser), _specFlowActionsConfiguration.Get("selenium:browser", "None"), true);
    public string[] Arguments => _specFlowActionsConfiguration.GetArray("selenium:arguments") ?? [];
    public Dictionary<string, string> Capabilities => _specFlowActionsConfiguration.GetDictionary("selenium:capabilities");
    public double? DefaultTimeout => _specFlowActionsConfiguration.GetDouble("selenium:defaulttimeout");
    public double? PollingInterval => _specFlowActionsConfiguration.GetDouble("selenium:pollinginterval");
}