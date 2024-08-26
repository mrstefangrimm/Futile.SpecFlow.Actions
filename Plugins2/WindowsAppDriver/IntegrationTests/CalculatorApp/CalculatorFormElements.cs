using OpenQA.Selenium.Appium.Windows;

namespace Futile.SpecFlow.Actions.WindowsAppDriver.IntegrationTests.CalculatorApp;

public class CalculatorFormElements
{
    private readonly AppDriver _appDriver;

    public CalculatorFormElements(AppDriver appDriver)
    {
        _appDriver = appDriver;
    }

    public WindowsElement FirstNumberTextBox => _appDriver.Current.FindElementByAccessibilityId("TextBoxFirst");
    public WindowsElement SecondNumberTextBox => _appDriver.Current.FindElementByAccessibilityId("TextBoxSecond");
    public WindowsElement ResultTextBox => _appDriver.Current.FindElementByAccessibilityId("TextBoxResult");


    public WindowsElement AddButton => _appDriver.Current.FindElementByAccessibilityId("ButtonAdd");
    public WindowsElement SubtractButton => _appDriver.Current.FindElementByAccessibilityId("ButtonSubtract");
    public WindowsElement MultiplyButton => _appDriver.Current.FindElementByAccessibilityId("ButtonMultiply");
    public WindowsElement DivideButton => _appDriver.Current.FindElementByAccessibilityId("ButtonDivide");
}