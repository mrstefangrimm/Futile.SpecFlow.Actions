using OpenQA.Selenium;

namespace Futile.SpecFlow.Actions.Selenium.DriverInitialisers;

public interface IDriverInitializer
{
    IWebDriver Initialize();
}