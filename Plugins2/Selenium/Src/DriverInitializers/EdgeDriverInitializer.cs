using Futile.SpecFlow.Actions.Selenium.Configuration;
using Futile.SpecFlow.Actions.Selenium.Hoster;
using OpenQA.Selenium;
using OpenQA.Selenium.Edge;
using System;

namespace Futile.SpecFlow.Actions.Selenium.DriverInitialisers;

public class EdgeDriverInitializer : DriverInitializer<EdgeOptions>
{
    private static readonly Lazy<string?> EdgeWebDriverFilePath =
        new Lazy<string?>(() => Environment.GetEnvironmentVariable("EDGE_WEBDRIVER_FILE_PATH"));

    public EdgeDriverInitializer(ISeleniumConfiguration seleniumConfiguration, ICredentialProvider credentialProvider) :
        base(seleniumConfiguration, credentialProvider)
    {
    }


    protected override void AddDefaultCapabilities(EdgeOptions options)
    {
        
    }

    protected override IWebDriver CreateWebDriver(EdgeOptions options)
    {
        return string.IsNullOrWhiteSpace(EdgeWebDriverFilePath.Value)
            ? new EdgeDriver(EdgeDriverService.CreateDefaultService(), options, TimeSpan.FromSeconds(120))
            : new EdgeDriver(EdgeDriverService.CreateDefaultService(EdgeWebDriverFilePath.Value), options,
                TimeSpan.FromSeconds(120));
    }
}