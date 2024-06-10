namespace Futile.Specflow.Actions.FlaUI.IntegrationTests.CalculatorApp;

public abstract class AppProxyBase
{
    protected readonly FlaUIDriver _driver;
    protected readonly AppiumLikeInteractions _interactions;

    protected AppProxyBase(FlaUIDriver driver)
    {
        _driver = driver;
        _interactions = new AppiumLikeInteractions(driver);
    }

    public void SwitchProfile(string profileName, string? arguments = null)
    {
        _driver.SwitchProfile(profileName, arguments);
    }
}
