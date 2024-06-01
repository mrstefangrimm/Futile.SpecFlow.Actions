using FlaUI.Core.AutomationElements;

namespace Futile.Specflow.Actions.FlaUI.IntegrationTests.CalculatorApp;

internal class CalculatorMainWindowElements
{
    private readonly FlaUIDriver _driver;

    public CalculatorMainWindowElements(FlaUIDriver driver)
    {
        _driver = driver;
    }

    public TextBox FirstNumberTextBox => _driver.Current.FindFirstDescendant("TextBoxFirst").AsTextBox();
    public TextBox SecondNumberTextBox => _driver.Current.FindFirstDescendant("TextBoxSecond").AsTextBox();
    public TextBox ResultTextBox => _driver.Current.FindFirstDescendant("TextBoxResult").AsTextBox();

    public Button AddButton => _driver.Current.FindFirstDescendant("ButtonAdd").AsButton();
    public Button SubtractButton => _driver.Current.FindFirstDescendant("ButtonSubtract").AsButton();
    public Button MultiplyButton => _driver.Current.FindFirstDescendant("ButtonMultiply").AsButton();
    public Button DivideButton => _driver.Current.FindFirstDescendant("ButtonDivide").AsButton();
}
