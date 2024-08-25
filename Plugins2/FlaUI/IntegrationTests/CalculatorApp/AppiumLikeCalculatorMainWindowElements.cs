using FlaUI.Core.AutomationElements;
using System.Collections.Generic;
using System.Linq;

namespace Futile.Specflow.Actions.FlaUI.IntegrationTests.CalculatorApp;

internal class AppiumLikeCalculatorMainWindowElements : ICalculatorMainWindowElements
{
    private readonly AppiumLikeInteractions _interactions;

    internal AppiumLikeCalculatorMainWindowElements(FlaUIDriver driver)
    {
        _interactions = new AppiumLikeInteractions(driver);
    }

    public AutomationElement WelcomeControl => _interactions.FindElementByClassName("LabelWithBorder");

    public TextBox FirstNumberTextBox => _interactions.FindElementByAccessibilityId("TextBoxFirst").AsTextBox();
    public TextBox SecondNumberTextBox => _interactions.FindElementsByAccessibilityId("TextBoxSecond")[0].AsTextBox();
    public TextBox ResultTextBox => _interactions.FindElementByAccessibilityId("TextBoxResult").AsTextBox();

    public IEnumerable<Button> AllButtons => _interactions.FindElementsByClassName("Button").Select(x => x.AsButton());
    public Button AddButton => _interactions.FindElementByName("Add").AsButton();
    public Button SubtractButton => _interactions.FindElementsByName("Subtract")[0].AsButton();
    public Button MultiplyButton => _interactions.FindElementByAccessibilityId("ButtonMultiply").AsButton();
    public Button DivideButton => _interactions.FindElementByAccessibilityId("ButtonDivide").AsButton();
}
