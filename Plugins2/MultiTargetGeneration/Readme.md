# Futile.SpecFlow.Actions.TestTargetsGeneration
[![Nuget](https://img.shields.io/nuget/v/Futile.SpecFlow.Actions.TestTargetsGeneration)](https://www.nuget.org/packages/SpecFlow.Actions.TestTargetsGeneration/)

This plugin supports targeting of multiple configurations at runtime. For each configuration you provide, a class will be generated in your feature's code behind file when you build the project. This means that for any given test, the test will be executed against each target.



## Futile?

Work on [Specflow](https://specflow.org/) has been [discontinued](https://github.com/SpecFlowOSS/SpecFlow/issues/2719) and the successor is [reqnroll](https://reqnroll.net/) (status May 2024). This nuget package comes from a [fork](https://github.com/mrstefangrimm/Futile.SpecFlow.Actions) that:

  - Moved out of `Futile.SpecFlow.Actions.Configuration` into separate package
  - Uses `net48` and `net8.0`



## How to use it

```specflow.actions.json```

``` json
{
  "your_configuration": "value of the property",
  "another_configuration": "another value"
}
```

```specflow.actions.TARGET_1.json```

``` json
{
  "your_configuration": "TARGET 1 overrides this value"
}
```

```specflow.actions.TARGET_2.json```

``` json
{
  "your_configuration": "TARGET 2 overrides this value"
}
```

``` csharp
[Binding]
public class A_Binding_Class
{
    private readonly ISpecFlowActionsConfiguration _specFlowActionsConfiguration;

    public A_Binding_Class(ISpecFlowActionsConfiguration specFlowActionsConfiguration)
    {
        _specFlowActionsConfiguration = specFlowActionsConfiguration;
    }

    public string GetConfigurationValue()
    {
        return _specFlowActionsConfiguration.Get("your_configuration", string.Empty);
    }
}
```

`.csproj`

```xml
 <ItemGroup>
     <PackageReference Include="Futile.SpecFlow.Actions.TestTargetsGeneration" Version="0.1.350.6" />
 </ItemGroup>

<ItemGroup>
    <None Update="specflow.actions.*.json">
      <CopyToOutputDirectory>Always</CopyToOutputDirectory>
    </None>
    <None Update="specflow.actions.json">
      <CopyToOutputDirectory>Always</CopyToOutputDirectory>
    </None>
  </ItemGroup>

```



## How to get it

Add the latest version of the `Futile.SpecFlow.Actions.TestTargetsGeneration` NuGet Package to your project.

Latest version:[![Nuget](https://img.shields.io/nuget/v/Futile.SpecFlow.Actions.TestTargetsGeneration)](https://www.nuget.org/packages/SpecFlow.Actions.TestTargetsGeneration/)