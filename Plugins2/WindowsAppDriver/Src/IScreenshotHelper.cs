using TechTalk.SpecFlow;

namespace Futile.SpecFlow.Actions.WindowsAppDriver;

public interface IScreenshotHelper
{
    void TakeScreenshot(AppDriver appDriver, FeatureContext featureContext, ScenarioContext scenarioContext);
}