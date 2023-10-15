using SpecFlow.Actions.Configuration;
using TechTalk.SpecFlow.Plugins;
using TechTalk.SpecFlow.UnitTestProvider;

[assembly:RuntimePlugin(typeof(ConfigurationRuntimePlugin))]

namespace SpecFlow.Actions.Configuration
{
    public class ConfigurationRuntimePlugin : IRuntimePlugin
    {
        public void Initialize(RuntimePluginEvents runtimePluginEvents, RuntimePluginParameters runtimePluginParameters,
            UnitTestProviderConfiguration unitTestProviderConfiguration)
        {
            runtimePluginEvents.CustomizeScenarioDependencies += RuntimePluginEvents_CustomizeScenarioDependencies;
            runtimePluginEvents.RegisterGlobalDependencies += RuntimePluginEvents_RegisterGlobalDependencies;
        }

        private void RuntimePluginEvents_RegisterGlobalDependencies(object? sender, RegisterGlobalDependenciesEventArgs e)
        {
            e.ObjectContainer.RegisterTypeAs<CurrentTargetIdentifier, CurrentTargetIdentifier>();
            // Registered here for WindowsAppDriver
            e.ObjectContainer.RegisterTypeAs<SpecFlowActionJsonLocator, ISpecFlowActionJsonLocator>();
            e.ObjectContainer.RegisterTypeAs<SpecFlowActionJsonLoader, ISpecFlowActionJsonLoader>();
        }

        private void RuntimePluginEvents_CustomizeScenarioDependencies(object? sender, CustomizeScenarioDependenciesEventArgs e)
        {
            e.ObjectContainer.RegisterTypeAs<CurrentTargetIdentifier, CurrentTargetIdentifier>();
            e.ObjectContainer.RegisterTypeAs<SpecFlowActionsConfiguration, ISpecFlowActionsConfiguration>();
            // Registered here for Selenium
            e.ObjectContainer.RegisterTypeAs<SpecFlowActionJsonLocator, ISpecFlowActionJsonLocator>();
            e.ObjectContainer.RegisterTypeAs<SpecFlowActionJsonLoader, ISpecFlowActionJsonLoader>();
        }
    }
}