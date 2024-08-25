using Futile.SpecFlow.Actions.Appium.Configuration.Android;
using Futile.SpecFlow.Actions.Appium.Configuration.WindowsAppDriver;
using Futile.SpecFlow.Actions.Appium.Server;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.Windows;
using System;

namespace Futile.SpecFlow.Actions.Appium.Driver;

public interface IDriverFactory
{
    AndroidDriver<AndroidElement> GetAndroidDriver(IAppiumServer appiumServer, IDriverOptions driverOptions, IAndroidConfiguration appiumConfiguration);

    void GetXCUITestDriver();

    void GetEspressoDriver();

    WindowsDriver<WindowsElement> GetWindowsAppDriver(IWindowsAppDriverConfiguration windowsAppDriverConfiguration, IDriverOptions driverOptions, Uri driverUrl);

    void GetMacDriver();
}