# Futile.SpecFlow.Actions.FlaUI

[![Nuget](https://img.shields.io/nuget/v/Futile.Specflow.Actions.FlaUI)](https://www.nuget.org/packages/Futile.Specflow.Actions.FlaUI/)

This SpecFlow.Action will help you use [FlaUI](https://github.com/FlaUI/FlaUI/) together with SpecFlow.

## Futile?

Work on [Specflow](https://specflow.org/) has been [discontinued](https://github.com/SpecFlowOSS/SpecFlow/issues/2719) and the successor is [reqnroll](https://reqnroll.net/) (status May 2024). This nuget package is an addition to a [fork](https://github.com/mrstefangrimm/Futile.SpecFlow.Actions).

### Included Features

- Lifetime handling of the application being tested
- Configuration via `specflow.actions.json`
- Launch of executables and Windows Store Apps (e.g. Calculator)

## Configuration

You can configure this plugin via the  `specflow.actions.json`.

``` json
{
  "flaui": {
    "settings": {
      "uia": "UIA2"
    },
    "profiles": {
      "Calculator.exe with Hello FlaUI": {
        "app": "[path]\Calculator.exe",
        "arguments": "Hello FlaUI",
        "launch": "Exe"
      },
      "Windows 11 Calculator": {
        "app": "Microsoft.WindowsCalculator_8wekyb3d8bbwe!App",
        "launch": "StoreApp"
      }
    }
  }
}
```

### uia
Supported values:
- `UIA2`
- `UIA3`

### app

filename of the executable or the name of the Windows App. The Windows App names can be found in can be found as folder in `%userprofile%\AppData\Local\Packages`.

### arguments

Optional list of arguments.

### launch

Supported values:

- `Exe`
- `StoreApp`

## How to use it

The application is started automatically when you try to use it for the first time. It is closed after the scenario ends. You can use constructor and/or parameter injection to inject dependencies into your classes, this way the class instances are managed automatically.

### Elements object

Wraps the FlaUI access to the application.

```csharp
public class CalculatorMainWindowElements
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
}
```

### Proxy object
Represents the application under test.
```csharp
public class CalculatorProxy
{
  private readonly FlaUIDriver _driver;
  private readonly CalculatorMainWindowElements _elements;

  public CalculatorProxy(FlaUIDriver driver)
  {
    _driver = driver;
    _elements = new CalculatorMainWindowElements(driver);        
  }

  public void EnterFirstNumber(string number)
  {
    _elements.FirstNumberTextBox.Text += number;
  }

  public void EnterSecondNumber(string number)
  {
    _elements.SecondNumberTextBox.Text += number;
  }

  public void ClickAdd()
  {
    _elements.AddButton.Click();
  }

  public string GetResult()
  {
    return _elements.ResultTextBox.Text;
  }
}
```
### Step definitions
```csharp
[Binding]
public sealed class CalculatorStepDefinitions
{
  private readonly CalculatorProxy _proxy;

  public CalculatorStepDefinitions(CalculatorProxy proxy)
  {
    _proxy = proxy;
  }

  [Given("the first number is (.*)")]
  public void GivenTheFirstNumberIs(int number)
  {
    _proxy.EnterFirstNumber(number.ToString());
  }
}
```
## How to get it

Add the latest version of the `Futile.SpecFlow.Actions.FlaUI` NuGet Package to your project.

Latest version: [![Nuget](https://img.shields.io/nuget/v/Futile.SpecFlow.Actions.FlaUI)](https://www.nuget.org/packages/Futile.SpecFlow.Actions.FlaUI/)
