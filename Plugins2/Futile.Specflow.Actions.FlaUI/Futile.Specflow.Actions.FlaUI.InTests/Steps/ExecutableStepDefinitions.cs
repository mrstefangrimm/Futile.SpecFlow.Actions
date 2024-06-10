using FluentAssertions;
using Futile.Specflow.Actions.FlaUI.IntegrationTests.CalculatorApp;
using TechTalk.SpecFlow;

namespace Futile.Specflow.Actions.FlaUI.IntegrationTests.Steps;

[Binding]
internal sealed class ExecutableStepDefinitions
{
    private readonly CalculatorProxy _proxy;

    public ExecutableStepDefinitions(CalculatorProxy proxy)
    {
        _proxy = proxy;
    }

#pragma warning disable VSSpell001 // Spell Check

    [Given(@"the Calculator\.exe with the argument Hello FlaUI from the profile")]
    public void GivenTheCalculator_ExeWithTheArgumentHelloFlaUIFromTheProfile()
    {
#if !DEBUG
        _proxy.SwitchProfile("Futile Calculator Release with Hello FlaUI");
#endif

        _proxy.SwitchProfile("Futile Calculator with Hello FlaUI");
    }

    [Given(@"the Calculator\.exe with the argument ""([^""]*)""")]
    public void GivenTheCalculator_ExeWithTheArgument(string arguments)
    {
#if !DEBUG
        _proxy.SwitchProfile("Futile Calculator Release", arguments);
#endif

        _proxy.SwitchProfile("Futile Calculator", arguments);
    }

    [Given("the first number is (.*)")]
    public void GivenTheFirstNumberIs(int number)
    {
#if !DEBUG
        _proxy.SwitchProfile("Futile Calculator Release");
#endif

        _proxy.EnterFirstNumber(number.ToString());
    }

    [Given("the second number is (.*)")]
    public void GivenTheSecondNumberIs(int number)
    {
        _proxy.EnterSecondNumber(number.ToString());
    }

    [When("the two numbers are added")]
    public void WhenTheTwoNumbersAreAdded()
    {
        _proxy.ClickAdd();
    }

    [When(@"the two numbers are subtracted")]
    public void WhenTheTwoNumbersAreSubtracted()
    {
        _proxy.ClickSubtract();
    }

    [When(@"the two numbers are multiplied")]
    public void WhenTheTwoNumbersAreMultiplied()
    {
        _proxy.ClickMultiply();
    }

    [When(@"the two numbers are divided")]
    public void WhenTheTwoNumbersAreDivided()
    {
        _proxy.ClickDivide();
    }

    [Then("the result should be (.*)")]
    public void ThenTheResultShouldBe(int result)
    {
        double actualResult = double.Parse(_proxy.GetResult());

        result.Should().Be((int)actualResult);
    }

    [Then(@"""([^""]*)"" should be displayed")]
    public void ThenShouldBeDisplayed(string expected)
    {
        string actual = _proxy.GetCommandLine();

        expected.Should().Be(actual);
    }

    [Then(@"number of buttons is (.*)")]
    public void ThenNumberOfButtonsIs(int numberOfButtons)
    {
        int actualCount = _proxy.GetNumberOfButtons();

        numberOfButtons.Should().Be(actualCount);
    }

#pragma warning restore VSSpell001 // Spell Check
}