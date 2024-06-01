using FlaUI.Core.AutomationElements;
using System;
using System.Collections.Generic;

namespace Futile.Specflow.Actions.FlaUI.IntegrationTests.CalculatorApp;

internal class WindowsCalculatorElements
{
    private readonly FlaUIDriver _driver;
    private readonly Lazy<IDictionary<char, Button>> _numbersLazy;

    public WindowsCalculatorElements(FlaUIDriver driver)
    {
        _driver = driver;

        _numbersLazy = new Lazy<IDictionary<char, Button>>(LoadNumbers);
    }

    private IDictionary<char, Button> LoadNumbers()
    {
        Dictionary<char, Button> buttons = new();
        buttons['1'] = _driver.Current.FindFirstDescendant("num1Button").AsButton();
        buttons['2'] = _driver.Current.FindFirstDescendant("num2Button").AsButton();
        buttons['3'] = _driver.Current.FindFirstDescendant("num3Button").AsButton();
        buttons['4'] = _driver.Current.FindFirstDescendant("num4Button").AsButton();
        buttons['5'] = _driver.Current.FindFirstDescendant("num5Button").AsButton();
        buttons['6'] = _driver.Current.FindFirstDescendant("num6Button").AsButton();
        buttons['7'] = _driver.Current.FindFirstDescendant("num7Button").AsButton();
        buttons['8'] = _driver.Current.FindFirstDescendant("num8Button").AsButton();
        buttons['9'] = _driver.Current.FindFirstDescendant("num9Button").AsButton();
        buttons['0'] = _driver.Current.FindFirstDescendant("num0Button").AsButton();

        return buttons;
    }

    public IDictionary<char, Button> Numbers => _numbersLazy.Value;

    public Button ButtonAdd => _driver.Current.FindFirstDescendant("plusButton").AsButton();
    public Button ButtonEquals => _driver.Current.FindFirstDescendant("equalButton").AsButton();
    public AutomationElement Results => _driver.Current.FindFirstDescendant("CalculatorResults");
}