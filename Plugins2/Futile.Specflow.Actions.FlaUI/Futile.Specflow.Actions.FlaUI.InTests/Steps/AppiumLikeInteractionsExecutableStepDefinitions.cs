using FluentAssertions;
using Futile.Specflow.Actions.FlaUI.IntegrationTests.CalculatorApp;
using TechTalk.SpecFlow;

namespace Futile.Specflow.Actions.FlaUI.IntegrationTests.Steps;

[Binding]
internal sealed class AppiumLikeInteractionsExecutableStepDefinitions
{
    private readonly CalculatorProxy _proxy;

    public AppiumLikeInteractionsExecutableStepDefinitions(CalculatorProxy proxy)
    {
        _proxy = proxy;
    }

#pragma warning disable VSSpell001 // Spell Check

    [Given(@"Appium-like interactions are set")]
    public void GivenAppium_LikeInteractionsAreSet()
    {
        _proxy.UseAppiumLikeInterface();
    }

#pragma warning restore VSSpell001 // Spell Check
}