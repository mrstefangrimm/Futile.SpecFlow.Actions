using BoDi;
using Futile.SpecFlow.Actions.Selenium.Configuration;
using Futile.SpecFlow.Actions.Selenium.DriverInitialisers;
using Futile.SpecFlow.Actions.Selenium.Hoster;
using System;
using TechTalk.SpecFlow.Plugins;
using TechTalk.SpecFlow.UnitTestProvider;

[assembly: RuntimePlugin(typeof(Futile.SpecFlow.Actions.Selenium.RuntimePlugin))]

namespace Futile.SpecFlow.Actions.Selenium
{
    public class RuntimePlugin : IRuntimePlugin
    {
        public void Initialize(RuntimePluginEvents runtimePluginEvents, RuntimePluginParameters runtimePluginParameters,
            UnitTestProviderConfiguration unitTestProviderConfiguration)
        {
            runtimePluginEvents.CustomizeScenarioDependencies += RuntimePluginEvents_CustomizeScenarioDependencies;
        }

        private void RuntimePluginEvents_CustomizeScenarioDependencies(object? sender, CustomizeScenarioDependenciesEventArgs e)
        {
            if (!e.ObjectContainer.IsRegistered<ISeleniumConfiguration>())
            {
                e.ObjectContainer.RegisterTypeAs<SeleniumConfiguration, ISeleniumConfiguration>(); 
            }

            if (!e.ObjectContainer.IsRegistered<IDriverInitializer>())
            {
                RegisterInitialisers(e.ObjectContainer);
            }

            if (!e.ObjectContainer.IsRegistered<ICredentialProvider>())
            {
                e.ObjectContainer.RegisterTypeAs<NoCredentialsProvider, ICredentialProvider>();
            }

            e.ObjectContainer.RegisterTypeAs<BrowserInteractions, IBrowserInteractions>();
        }

        private void RegisterInitialisers(IObjectContainer objectContainer)
        {
            objectContainer.RegisterFactoryAs<IDriverInitializer>(container =>
            {
                var config = container.Resolve<ISeleniumConfiguration>();

                var credentialProvider = container.Resolve<ICredentialProvider>();
                return config.Browser switch
                {
                    Browser.Chrome => new ChromeDriverInitializer(config, credentialProvider),
                    Browser.Firefox => new FirefoxDriverInitializer(config, credentialProvider),
                    Browser.Edge => new EdgeDriverInitializer(config, credentialProvider),
                    Browser.Safari => new SafariDriverInitializer(config, credentialProvider),
                    _ => throw new ArgumentOutOfRangeException($"Browser {config.Browser} not implemented")
                };
            });
        }
    }
}