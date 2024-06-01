using FlaUI.Core;
using FlaUI.Core.AutomationElements;
using FlaUI.Core.Tools;
using FlaUI.UIA2;
using FlaUI.UIA3;
using System;
using System.Linq;

namespace Futile.Specflow.Actions.FlaUI;

public class FlaUIDriver : IDisposable
{
    private readonly IConfiguration _configuration;
    private AutomationBase? _automation;
    private Application? _application;
    private string? _currentProfileName;

    private readonly Lazy<AutomationElement> _currentLazy;
    private bool _disposed;

    internal FlaUIDriver(IConfiguration configuration)
    {
        _configuration = configuration;
        _currentLazy = new Lazy<AutomationElement>(LaunchProfile);
    }

    public void SwitchProfile(string name)
    {
        _currentProfileName = name;
    }

    public AutomationElement Current => _currentLazy.Value;

    private AutomationElement LaunchProfile()
    {
        _automation = _configuration.Settings.UIA switch
        {
            FlaUIA.UIA2 => new UIA2Automation(),
            FlaUIA.UIA3 => new UIA3Automation(),
            _ => throw new InvalidOperationException($"Invalid FlaUI Automation {_configuration.Settings.UIA}."),
        };

        var profiles = _configuration.Profiles;
        if (profiles == null || !profiles.Any()) { throw new InvalidOperationException("No FlaUI profile defined"); }

        if (_currentProfileName == null)
        {
            _currentProfileName = profiles.First().Key;
            if (_currentProfileName == null || string.IsNullOrEmpty(_currentProfileName)) { throw new InvalidOperationException($"Invalid FlaUI profile name {_currentProfileName}."); }
        }

        var profile = profiles[_currentProfileName];
        if (profile == null) { throw new InvalidOperationException($"Invalid profile with name {_currentProfileName}."); }

        if (profile.Launch == LaunchCommand.Exe)
        {
            _application = Application.Launch(profile.App, profile.Arguments);
        }
        else if (profile.Launch == LaunchCommand.StoreApp)
        {
            _application = Application.LaunchStoreApp(profile.App, profile.Arguments);
        }
        else
        {
            throw new InvalidOperationException();
        }

        return _application.GetMainWindow(_automation);
    }

    public void Dispose()
    {
        if (_disposed)
        {
            return;
        }

        if (_application != null)
        {
            _application.Close();
            Retry.WhileFalse(() => _application.HasExited, TimeSpan.FromSeconds(2), ignoreException: true);
            _application.Dispose();
            _application = null;

        }
        if (_automation != null)
        {
            _automation.Dispose();
            _automation = null;
        }

        _disposed = true;
    }
}
