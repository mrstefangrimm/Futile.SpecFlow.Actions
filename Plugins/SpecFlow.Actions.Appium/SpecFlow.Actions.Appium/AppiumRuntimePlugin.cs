﻿// Ignore Spelling: Plugin Appium

using SpecFlow.Actions.Appium;
using SpecFlow.Actions.Appium.Driver;
using TechTalk.SpecFlow.Plugins;
using TechTalk.SpecFlow.UnitTestProvider;

[assembly: RuntimePlugin(typeof(AppiumRuntimePlugin))]

namespace SpecFlow.Actions.Appium
{
    public class AppiumRuntimePlugin : IRuntimePlugin
    {
        public void Initialize(RuntimePluginEvents runtimePluginEvents, RuntimePluginParameters runtimePluginParameters, UnitTestProviderConfiguration unitTestProviderConfiguration)
        {
            runtimePluginEvents.CustomizeScenarioDependencies += RuntimePluginEvents_CustomizeScenarioDependencies;
        }

        private void RuntimePluginEvents_CustomizeScenarioDependencies(object? sender, CustomizeScenarioDependenciesEventArgs e)
        {
            e.ObjectContainer.RegisterTypeAs<DriverFactory, IDriverFactory>();
        }
    }
}