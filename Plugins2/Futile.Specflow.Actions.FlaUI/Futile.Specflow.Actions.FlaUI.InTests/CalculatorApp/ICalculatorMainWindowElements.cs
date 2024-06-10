using FlaUI.Core.AutomationElements;
using System.Collections.Generic;

namespace Futile.Specflow.Actions.FlaUI.IntegrationTests.CalculatorApp;

internal interface ICalculatorMainWindowElements
{
    AutomationElement WelcomeControl { get; }

    TextBox FirstNumberTextBox { get; }
    TextBox SecondNumberTextBox { get; }
    TextBox ResultTextBox { get; }

    IEnumerable<Button> AllButtons { get; }
    Button AddButton { get; }
    Button SubtractButton { get; }
    Button MultiplyButton { get; }
    Button DivideButton { get; }
}