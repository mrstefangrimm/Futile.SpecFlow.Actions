using FlaUI.Core.AutomationElements;
using System.Collections.Generic;
using System.Linq;

namespace Futile.Specflow.Actions.FlaUI.IntegrationTests.CalculatorApp;

internal class CalculatorMainWindowElements : ICalculatorMainWindowElements
{
    private readonly FlaUIDriver _driver;

    internal CalculatorMainWindowElements(FlaUIDriver driver)
    {
        _driver = driver;
    }

    public AutomationElement WelcomeControl => _driver.Current.FindFirstDescendant("WelcomeLabel");

    public TextBox FirstNumberTextBox => _driver.Current.FindFirstDescendant("TextBoxFirst").AsTextBox();
    public TextBox SecondNumberTextBox => _driver.Current.FindFirstDescendant("TextBoxSecond").AsTextBox();
    public TextBox ResultTextBox => _driver.Current.FindFirstDescendant("TextBoxResult").AsTextBox();

    public IEnumerable<Button> AllButtons => _driver.Current.FindAllChildren(_driver.Get.ByClassName("Button")).Select(x => x.AsButton());
    public Button AddButton => _driver.Current.FindFirstDescendant("ButtonAdd").AsButton();
    public Button SubtractButton => _driver.Current.FindFirstDescendant("ButtonSubtract").AsButton();
    public Button MultiplyButton => _driver.Current.FindFirstDescendant("ButtonMultiply").AsButton();
    public Button DivideButton => _driver.Current.FindFirstDescendant("ButtonDivide").AsButton();
}
