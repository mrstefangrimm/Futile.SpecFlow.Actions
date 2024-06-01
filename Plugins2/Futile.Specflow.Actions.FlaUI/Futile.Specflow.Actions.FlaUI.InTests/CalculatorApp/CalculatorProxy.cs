using FlaUI.Core.AutomationElements;
using System.Text.RegularExpressions;

namespace Futile.Specflow.Actions.FlaUI.IntegrationTests.CalculatorApp;

internal class CalculatorProxy
{
    private readonly FlaUIDriver _driver;
    private readonly CalculatorMainWindowElements _exeElements;
    private readonly WindowsCalculatorElements _storeElements;

    public CalculatorProxy(FlaUIDriver driver)
    {
        _driver = driver;
        _exeElements = new CalculatorMainWindowElements(driver);
        _storeElements = new WindowsCalculatorElements(driver);
    }

    public void SelectProfile(string profileName)
    {
        _driver.SwitchProfile(profileName);
    }

    public void EnterFirstNumber(string number)
    {
        _exeElements.FirstNumberTextBox.Text += number;
    }

    public void EnterSecondNumber(string number)
    {
        _exeElements.SecondNumberTextBox.Text += number;
    }

    public void ClickAdd()
    {
        _exeElements.AddButton.Click();
    }

    public void ClickSubtract()
    {
        _exeElements.SubtractButton.Click();
    }

    public void ClickMultiply()
    {
        _exeElements.MultiplyButton.Click();
    }

    public void ClickDivide()
    {
        _exeElements.DivideButton.Click();
    }

    public string GetResult()
    {
        return _exeElements.ResultTextBox.Text;
    }

    public string GetCommandLine()
    {
        return _driver.Current.FindFirstDescendant("LabelCommandlineArgs").AsLabel().Text;
    }


    public void WinCalculatorEnter(char number)
    {
        _storeElements.Numbers[number].Click();
    }

    public void WinCalculatorClickAdd()
    {
        _storeElements.ButtonAdd.Click();
    }

    public void WinCalculatorClickEqual()
    {
        _storeElements.ButtonEquals.Click();
    }

    public string WinCalculatorGetResult()
    {
        var resultElement = _storeElements.Results;
        var value = resultElement.Properties.Name;
        return Regex.Replace(value, "[^0-9]", string.Empty);
    }
}