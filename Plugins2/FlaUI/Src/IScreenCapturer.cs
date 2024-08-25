using System;
using TechTalk.SpecFlow;

namespace Futile.Specflow.Actions.FlaUI;

internal interface IScreenCapturer : IDisposable
{
    public void TakeScreenshot(FlaUIDriver driver, FeatureContext featureContext, ScenarioContext scenarioContext);

    public void StartRecording();
    public void StopRecording(FlaUIDriver driver, FeatureContext featureContext, ScenarioContext scenarioContext);
}
