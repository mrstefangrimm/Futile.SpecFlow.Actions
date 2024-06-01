using Futile.Specflow.Actions.FlaUI;
using TechTalk.SpecFlow.Plugins;
using TechTalk.SpecFlow.UnitTestProvider;

[assembly: RuntimePlugin(typeof(RuntimePlugin))]

namespace Futile.Specflow.Actions.FlaUI;

public class RuntimePlugin : IRuntimePlugin
{
    public void Initialize(
        RuntimePluginEvents runtimePluginEvents,
        RuntimePluginParameters runtimePluginParameters,
        UnitTestProviderConfiguration unitTestProviderConfiguration)
    {
        runtimePluginEvents.CustomizeScenarioDependencies += RuntimePluginEvents_CustomizeScenarioDependencies;
    }

    private void RuntimePluginEvents_CustomizeScenarioDependencies(object? sender, CustomizeScenarioDependenciesEventArgs e)
    {
        e.ObjectContainer.RegisterTypeAs<Configuration, IConfiguration>();
    }
}