// Ignore Spelling: App Cli

using SpecFlow.Actions.Appium.Configuration.WindowsAppDriver;
using System;
using System.Diagnostics;

namespace SpecFlow.Actions.WindowsAppDriver
{
    public class AppDriverCli : IAppDriverCli
    {
        private readonly IWindowsAppDriverConfiguration _windowsAppDriverConfiguration;
        private bool _isDisposed;
        private Process? _appDriverProcess;

        public AppDriverCli(IWindowsAppDriverConfiguration windowsAppDriverConfiguration)
        {
            _windowsAppDriverConfiguration = windowsAppDriverConfiguration;
        }

        /// <summary>
        /// Starts the WindowsAppDriver.exe process
        /// </summary>
        public void Start()
        {
            var path = _windowsAppDriverConfiguration.WindowsAppDriverPath ??
                       Environment.GetEnvironmentVariable("WINDOWS_APP_DRIVER_EXECUTABLE_PATH") ?? null;

            if (path != null)
            {
                string arguments = _windowsAppDriverConfiguration.WindowsAppDriverPort != null && _windowsAppDriverConfiguration.WindowsAppDriverPort.HasValue ? _windowsAppDriverConfiguration.WindowsAppDriverPort.Value.ToString() : "";
                _appDriverProcess = Process.Start(path, arguments);
            }
        }

        /// <summary>
        /// Disposes the WindowsAppDriver.exe process
        /// </summary>
        public void Dispose()
        {
            if ( _isDisposed)
            {
                return;
            }
            _appDriverProcess?.Kill();

            _isDisposed = true;
        }
    }
}