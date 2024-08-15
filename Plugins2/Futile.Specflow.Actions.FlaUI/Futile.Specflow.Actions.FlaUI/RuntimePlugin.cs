using Futile.Specflow.Actions.FlaUI;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Plugins;
using TechTalk.SpecFlow.UnitTestProvider;

[assembly: RuntimePlugin(typeof(RuntimePlugin))]

namespace Futile.Specflow.Actions.FlaUI;

public class RuntimePlugin : IRuntimePlugin
{
    private IScreenCapturer? _screenCapturer;

    public void Initialize(
        RuntimePluginEvents runtimePluginEvents,
        RuntimePluginParameters runtimePluginParameters,
        UnitTestProviderConfiguration unitTestProviderConfiguration)
    {
        runtimePluginEvents.RegisterGlobalDependencies += RuntimePluginEvents_RegisterGlobalDependencies;
    }

    private void RuntimePluginEvents_RegisterGlobalDependencies(object? sender, RegisterGlobalDependenciesEventArgs e)
    {
        var runtimePluginTestExecutionLifecycleEventEmitter = e.ObjectContainer.Resolve<RuntimePluginTestExecutionLifecycleEvents>();
        runtimePluginTestExecutionLifecycleEventEmitter.BeforeTestRun += RuntimePluginTestExecutionLifecycleEventEmitter_BeforeTestRun;
        runtimePluginTestExecutionLifecycleEventEmitter.AfterTestRun += RuntimePluginTestExecutionLifecycleEventEmitter_AfterTestRun;
        runtimePluginTestExecutionLifecycleEventEmitter.AfterStep += RuntimePluginTestExecutionLifecycleEventEmitter_AfterStep;
        runtimePluginTestExecutionLifecycleEventEmitter.AfterScenario += RuntimePluginTestExecutionLifecycleEventEmitter_AfterScenario; ;

        e.ObjectContainer.RegisterTypeAs<Configuration, IConfiguration>();
        e.ObjectContainer.RegisterTypeAs<ScreenCapturer, IScreenCapturer>();
    }

    private void RuntimePluginTestExecutionLifecycleEventEmitter_BeforeTestRun(object? sender, RuntimePluginBeforeTestRunEventArgs e)
    {
        _screenCapturer = e.ObjectContainer.Resolve<IScreenCapturer>();

        _screenCapturer?.StartRecording();
    }

    private void RuntimePluginTestExecutionLifecycleEventEmitter_AfterStep(object? sender, RuntimePluginAfterStepEventArgs e)
    {
        var driver = e.ObjectContainer.Resolve<FlaUIDriver>();
        var featureContext = e.ObjectContainer.Resolve<FeatureContext>();
        var scenarioContext = e.ObjectContainer.Resolve<ScenarioContext>();

        _screenCapturer?.TakeScreenshot(driver, featureContext, scenarioContext);
    }

    private void RuntimePluginTestExecutionLifecycleEventEmitter_AfterScenario(object? sender, RuntimePluginAfterScenarioEventArgs e)
    {
        var driver = e.ObjectContainer.Resolve<FlaUIDriver>();
        var featureContext = e.ObjectContainer.Resolve<FeatureContext>();
        var scenarioContext = e.ObjectContainer.Resolve<ScenarioContext>();

        _screenCapturer?.StopRecording(driver, featureContext, scenarioContext);
    }

    private void RuntimePluginTestExecutionLifecycleEventEmitter_AfterTestRun(object? sender, RuntimePluginAfterTestRunEventArgs e)
    {
        _screenCapturer?.Dispose();
    }
}