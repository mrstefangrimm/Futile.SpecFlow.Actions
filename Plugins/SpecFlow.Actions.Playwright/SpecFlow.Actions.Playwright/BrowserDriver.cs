using Microsoft.Playwright;
using System;
using System.Threading.Tasks;

namespace SpecFlow.Actions.Playwright;

/// <summary>
/// Manages a browser instance using Playwright
/// </summary>
public class BrowserDriver : IDisposable
{
    private readonly IPlaywrightConfiguration _playwrightConfiguration;
    private readonly IDriverInitialiser _driverInitializer;
    protected readonly AsyncLazy<IBrowser> _currentBrowserLazy;
    protected bool _isDisposed;

    public BrowserDriver(IPlaywrightConfiguration playwrightConfiguration, IDriverInitialiser driverInitializer)
    {
        _playwrightConfiguration = playwrightConfiguration;
        _driverInitializer = driverInitializer;
        _currentBrowserLazy = new AsyncLazy<IBrowser>(CreatePlaywrightAsync);
    }

    /// <summary>
    /// The current Playwright instance
    /// </summary>
    public Task<IBrowser> Current => _currentBrowserLazy.Value;

    /// <summary>
    /// Creates a new instance of Playwright (opens a browser)
    /// </summary>
    /// <returns></returns>
    private async Task<IBrowser> CreatePlaywrightAsync()
    {
        return _playwrightConfiguration.Browser switch
        {
            Browser.Chrome => await _driverInitializer.GetChromeDriverAsync(_playwrightConfiguration.Arguments, _playwrightConfiguration.DefaultTimeout, _playwrightConfiguration.Headless, _playwrightConfiguration.SlowMo, _playwrightConfiguration.TraceDir),
            Browser.Firefox => await _driverInitializer.GetFirefoxDriverAsync(_playwrightConfiguration.Arguments, _playwrightConfiguration.DefaultTimeout, _playwrightConfiguration.Headless, _playwrightConfiguration.SlowMo, _playwrightConfiguration.TraceDir),
            Browser.Edge => await _driverInitializer.GetEdgeDriverAsync(_playwrightConfiguration.Arguments, _playwrightConfiguration.DefaultTimeout, _playwrightConfiguration.Headless, _playwrightConfiguration.SlowMo, _playwrightConfiguration.TraceDir),
            Browser.Chromium => await _driverInitializer.GetChromiumDriverAsync(_playwrightConfiguration.Arguments, _playwrightConfiguration.DefaultTimeout, _playwrightConfiguration.Headless, _playwrightConfiguration.SlowMo, _playwrightConfiguration.TraceDir),
            Browser.Webkit => await _driverInitializer.GetWebKitDriverAsync(_playwrightConfiguration.Arguments, _playwrightConfiguration.DefaultTimeout, _playwrightConfiguration.Headless, _playwrightConfiguration.SlowMo, _playwrightConfiguration.TraceDir),
            _ => throw new NotImplementedException($"Support for browser {_playwrightConfiguration.Browser} is not implemented yet"),
        };
    }

    /// <summary>
    /// Disposes the Playwright instance (closing the browser)
    /// </summary>
    public void Dispose()
    {
        if (_isDisposed)
        {
            return;
        }

        if (_currentBrowserLazy.IsValueCreated)
        {
            Task.Run(async delegate
            {
                await (await Current).CloseAsync();
                await (await Current).DisposeAsync();
            });
        }

        _isDisposed = true;
    }
}
