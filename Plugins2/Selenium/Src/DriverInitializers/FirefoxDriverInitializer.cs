using Futile.SpecFlow.Actions.Selenium.Configuration;
using Futile.SpecFlow.Actions.Selenium.Hoster;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using System;

namespace Futile.SpecFlow.Actions.Selenium.DriverInitialisers;

public class FirefoxDriverInitializer : DriverInitializer<FirefoxOptions>
{
    private static readonly Lazy<string?> FirefoxWebDriverFilePath =
        new Lazy<string?>(() => Environment.GetEnvironmentVariable("FIREFOX_WEBDRIVER_FILE_PATH"));

    public FirefoxDriverInitializer(ISeleniumConfiguration seleniumConfiguration,
        ICredentialProvider credentialProvider) : base(seleniumConfiguration, credentialProvider)
    {
    }

    protected override void AddDefaultCapabilities(FirefoxOptions options)
    {
        
    }

    protected override IWebDriver CreateWebDriver(FirefoxOptions options)
    {
        return string.IsNullOrWhiteSpace(FirefoxWebDriverFilePath.Value)
            ? new FirefoxDriver(FirefoxDriverService.CreateDefaultService(), options, TimeSpan.FromSeconds(120))
            : new FirefoxDriver(FirefoxDriverService.CreateDefaultService(FirefoxWebDriverFilePath.Value), options,
                TimeSpan.FromSeconds(120));
    }
}