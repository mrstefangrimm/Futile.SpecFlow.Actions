using TechTalk.SpecFlow.Plugins;
using TechTalk.SpecFlow.UnitTestProvider;

[assembly:RuntimePlugin(typeof(SpecFlow.Actions.Playwright.RuntimePlugin))]

namespace SpecFlow.Actions.Playwright;

public class RuntimePlugin : IRuntimePlugin
{
    public void Initialize(RuntimePluginEvents runtimePluginEvents, RuntimePluginParameters runtimePluginParameters,
        UnitTestProviderConfiguration unitTestProviderConfiguration)
    {
        runtimePluginEvents.CustomizeScenarioDependencies += RuntimePluginEvents_CustomizeScenarioDependencies;
    }

    private void RuntimePluginEvents_CustomizeScenarioDependencies(object? sender, CustomizeScenarioDependenciesEventArgs e)
    {
        e.ObjectContainer.RegisterTypeAs<PlaywrightConfiguration, IPlaywrightConfiguration>();
        e.ObjectContainer.RegisterTypeAs<DriverInitialiser, IDriverInitialiser>();
    }
}