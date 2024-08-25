using Futile.SpecFlow.Actions.Appium.Configuration.Android;
using Futile.SpecFlow.Actions.Appium.Configuration.WindowsAppDriver;
using Futile.SpecFlow.Actions.Appium.Server;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.Windows;
using System;

namespace Futile.SpecFlow.Actions.Appium.Driver;

internal class DriverFactory : IDriverFactory
{
    public AndroidDriver<AndroidElement> GetAndroidDriver(IAppiumServer appiumServer, IDriverOptions driverOptions, IAndroidConfiguration appiumConfiguration)
    {
        return appiumConfiguration.LocalAppiumServerRequired

            ? new AndroidDriver<AndroidElement>(appiumServer.Current, driverOptions.Current,
                TimeSpan.FromSeconds(appiumConfiguration.Timeout ?? 30))

            : new AndroidDriver<AndroidElement>(appiumConfiguration.ServerAddress, driverOptions.Current,
                TimeSpan.FromSeconds(appiumConfiguration.Timeout ?? 30));
    }

    public void GetXCUITestDriver()
    {
        throw new NotImplementedException("XCUITestDriver not yet implemented");
    }

    public void GetEspressoDriver()
    {
        throw new NotImplementedException("EspressoDriver not yet implemented");
    }

    public WindowsDriver<WindowsElement> GetWindowsAppDriver(IWindowsAppDriverConfiguration windowsAppDriverConfiguration, IDriverOptions driverOptions, Uri driverUrl)
    {
        return new WindowsDriver<WindowsElement>(driverUrl, driverOptions.Current);
    }

    public void GetMacDriver()
    {
        throw new NotImplementedException("MacDriver not yet implemented");
    }
}