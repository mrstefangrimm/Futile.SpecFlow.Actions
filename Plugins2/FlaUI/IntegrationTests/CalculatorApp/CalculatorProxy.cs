using FlaUI.Core.AutomationElements;
using System.Linq;
using System.Text.RegularExpressions;

namespace Futile.Specflow.Actions.FlaUI.IntegrationTests.CalculatorApp;

internal class CalculatorProxy : AppProxyBase
{
    private ICalculatorMainWindowElements _exeElements;
    private readonly WindowsCalculatorElements _storeElements;

    public CalculatorProxy(FlaUIDriver driver) : base(driver)
    {
        _exeElements = new CalculatorMainWindowElements(driver);
        _storeElements = new WindowsCalculatorElements(driver);
    }

#pragma warning disable VSSpell001 // Spell Check

    #region Calculator.exe

    public void UseAppiumLikeInterface()
    {
        _exeElements = new AppiumLikeCalculatorMainWindowElements(_driver);
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

    public int GetNumberOfButtons()
    {
        return _exeElements.AllButtons.Count();
    }

    public string GetCommandLine()
    {
        var userControl = _exeElements.WelcomeControl;
        return userControl.FindFirstDescendant().AsLabel().Text;
    }

    #endregion Calculator.exe


    #region Windows Calculator

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

    #endregion Windows Calculator

#pragma warning restore VSSpell001 // Spell Check

}