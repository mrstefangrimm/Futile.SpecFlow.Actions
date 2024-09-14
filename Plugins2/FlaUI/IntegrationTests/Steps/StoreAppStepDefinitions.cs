using FluentAssertions;
using Futile.Specflow.Actions.FlaUI.IntegrationTests.CalculatorApp;
using TechTalk.SpecFlow;

namespace Futile.Specflow.Actions.FlaUI.IntegrationTests.Steps;

[Binding]
internal sealed class StoreAppStepDefinitions
{
    private readonly CalculatorProxy _proxy;

    public StoreAppStepDefinitions(CalculatorProxy proxy)
    {
        _proxy = proxy;
    }

#pragma warning disable VSSpell001 // Spell Check

    [Given(@"the Windows calculator")]
    public void GivenTheWindowsCalculator()
    {
        _proxy.SwitchProfile("Windows 11 Calculator");
    }

    [Given(@"the number ""([^""]*)"" is entered")]
    public void GivenTheNumberIsEntered(string number)
    {
        foreach (char ch in number)
        {
            _proxy.WinCalculatorEnter(ch);
        }
    }

    [Given(@"Plus is pressed")]
    public void GivenPlusIsPressed()
    {
        _proxy.WinCalculatorClickAdd();
    }

    [When(@"Equal is pressed")]
    public void WhenEqualIsPressed()
    {
        _proxy.WinCalculatorClickEqual();
    }

    [Then(@"the result is (.*)")]
    public void ThenTheResultIs(int result)
    {
        double actualResult = double.Parse(_proxy.WinCalculatorGetResult());

        result.Should().Be((int)actualResult);
    }

#pragma warning restore VSSpell001 // Spell Check
}