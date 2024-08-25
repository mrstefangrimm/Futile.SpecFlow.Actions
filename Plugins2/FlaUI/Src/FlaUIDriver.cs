using FlaUI.Core;
using FlaUI.Core.AutomationElements;
using FlaUI.Core.Conditions;
using FlaUI.Core.Tools;
using FlaUI.UIA2;
using FlaUI.UIA3;
using System;
using System.Linq;

namespace Futile.Specflow.Actions.FlaUI;

public class FlaUIDriver : IDisposable
{
    private readonly IConfiguration _configuration;
    private Application? _application;
    private string? _launchProfileName;
    private string? _launchProfileArguments;

    private readonly Lazy<Window> _currentLazy;
    private bool _disposed;

    internal FlaUIDriver(IConfiguration configuration)
    {
        _configuration = configuration;
        _currentLazy = new Lazy<Window>(LaunchProfile);
    }

    /// <summary>
    /// Select a profile in "specflow.actions.json". By default, the first profile is used.
    /// </summary>
    /// <param name="name">The profile name </param>
    /// <param name="launchProfileArguments">Command line arguments. They overwrite the arguments defined in the profile.</param>
    /// <exception cref="InvalidOperationException">thrown when this method is called after the application launch.</exception>
    public void SwitchProfile(string name, string? launchProfileArguments = null)
    {
        if (_currentLazy.IsValueCreated)
        {
            throw new InvalidOperationException("switch profile on launched application is not possible.");
        }

        _launchProfileName = name;
        _launchProfileArguments = launchProfileArguments;
    }

    /// <summary>
    /// Returns <see cref="Application.GetMainWindow(AutomationBase, TimeSpan?)"/> of the launched application.
    /// </summary>
    public Window Current => _currentLazy.Value;

    /// <summary>
    /// Returns <see cref="AutomationBase.ConditionFactory"/> of the used automation.
    /// </summary>
    public ConditionFactory Get => _currentLazy.Value.Automation.ConditionFactory;

    private Window LaunchProfile()
    {
        AutomationBase automation = _configuration.Settings.UIA switch
        {
            FlaUIA.UIA2 => new UIA2Automation(),
            FlaUIA.UIA3 => new UIA3Automation(),
            _ => throw new InvalidOperationException($"Invalid FlaUI Automation {_configuration.Settings.UIA}."),
        };

        var profiles = _configuration.Profiles;
        if (profiles == null || !profiles.Any()) { throw new InvalidOperationException("No FlaUI profile defined"); }

        if (_launchProfileName == null)
        {
            _launchProfileName = profiles.First().Key;
            if (_launchProfileName == null || string.IsNullOrEmpty(_launchProfileName)) { throw new InvalidOperationException($"Invalid FlaUI profile name {_launchProfileName}."); }
        }

        var profile = profiles[_launchProfileName];
        if (profile == null) { throw new InvalidOperationException($"Invalid profile with name {_launchProfileName}."); }

        if (profile.Launch == LaunchCommand.Exe)
        {
            _application = Application.Launch(profile.App, _launchProfileArguments ?? profile.Arguments);
        }
        else if (profile.Launch == LaunchCommand.StoreApp)
        {
            _application = Application.LaunchStoreApp(profile.App, _launchProfileArguments ?? profile.Arguments);
        }
        else
        {
            throw new InvalidOperationException();
        }

        return _application.GetMainWindow(automation);
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
            var application = _application;
            Retry.WhileFalse(() => application.HasExited, TimeSpan.FromSeconds(2), ignoreException: true);
            _application.Dispose();
            _application = null;
        }

        _disposed = true;
    }
}
