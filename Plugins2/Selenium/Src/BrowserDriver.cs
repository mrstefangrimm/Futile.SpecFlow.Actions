using System;
using Futile.SpecFlow.Actions.Selenium.DriverInitialisers;
using OpenQA.Selenium;

namespace Futile.SpecFlow.Actions.Selenium;

/// <summary>
/// Manages a browser instance using Selenium
/// </summary>
public class BrowserDriver : IDisposable
{
    private readonly IDriverInitializer _driverInitializer;

    protected readonly Lazy<IWebDriver> CurrentWebDriverLazy;
    protected bool IsDisposed;

    public BrowserDriver(IDriverInitializer driverInitializer)
    {
        _driverInitializer = driverInitializer;
        CurrentWebDriverLazy = new Lazy<IWebDriver>(CreateWebDriver);
    }

    /// <summary>
    /// The current Selenium IWebDriver instance
    /// </summary>
    public IWebDriver Current => CurrentWebDriverLazy.Value;

    /// <summary>
    /// Creates the Selenium web driver (opens a browser)
    /// </summary>
    private IWebDriver CreateWebDriver()
    {
        return _driverInitializer.Initialize();
    }

    /// <summary>
    /// Disposes the Selenium web driver (closing the browser)
    /// </summary>
    public void Dispose()
    {
        if (IsDisposed)
        {
            return;
        }

        if (CurrentWebDriverLazy.IsValueCreated)
        {
            Current.Quit();
        }

        IsDisposed = true;
    }
}