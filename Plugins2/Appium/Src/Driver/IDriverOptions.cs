using OpenQA.Selenium.Appium;

namespace Futile.SpecFlow.Actions.Appium.Driver;

public interface IDriverOptions
{
    AppiumOptions Current { get; }
}