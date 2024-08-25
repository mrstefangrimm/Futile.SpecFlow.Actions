using System;

namespace Futile.SpecFlow.Actions.WindowsAppDriver;

public interface IAppDriverCli : IDisposable
{
    void Start();
}