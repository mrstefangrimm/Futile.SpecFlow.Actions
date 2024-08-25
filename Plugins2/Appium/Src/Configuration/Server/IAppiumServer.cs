using OpenQA.Selenium.Appium.Service;
using System;

namespace Futile.SpecFlow.Actions.Appium.Server;

public interface IAppiumServer : IDisposable
{
    AppiumLocalService Current { get; }
}