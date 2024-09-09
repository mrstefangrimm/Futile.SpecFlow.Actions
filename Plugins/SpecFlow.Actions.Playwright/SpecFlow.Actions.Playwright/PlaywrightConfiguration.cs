using SpecFlow.Actions.Configuration;
using System;

namespace SpecFlow.Actions.Playwright;

public interface IPlaywrightConfiguration
{
    Browser Browser { get; }

    string[]? Arguments { get; }

    float? DefaultTimeout { get; }

    bool? Headless { get; }

    float? SlowMo { get; }

    string? TraceDir { get; }
}

public class PlaywrightConfiguration : IPlaywrightConfiguration
{
    private readonly ISpecFlowActionsConfiguration _specFlowActionsConfiguration;

    public PlaywrightConfiguration(ISpecFlowActionsConfiguration specFlowActionsConfiguration)
    {
        _specFlowActionsConfiguration = specFlowActionsConfiguration;
    }

    public Browser Browser => (Browser)Enum.Parse(typeof(Browser), _specFlowActionsConfiguration.Get("playwright:browser"), true);
    public string[]? Arguments => _specFlowActionsConfiguration.GetArray("selenium:arguments") ?? [];
    public float? DefaultTimeout => (float?)_specFlowActionsConfiguration.GetDouble("playwright:defaultTimeout");
    public bool? Headless => bool.Parse(_specFlowActionsConfiguration.Get("playwright:headless"));
    public float? SlowMo => (float?)_specFlowActionsConfiguration.GetDouble("playwright:slowmo");
    public string? TraceDir => _specFlowActionsConfiguration.Get("playwright:traceDir");
}