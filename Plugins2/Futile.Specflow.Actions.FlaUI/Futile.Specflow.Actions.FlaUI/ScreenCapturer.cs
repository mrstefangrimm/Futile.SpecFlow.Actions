using FlaUI.Core.Capturing;
using System;
using System.IO;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.TestFramework;
using Path = System.IO.Path;

namespace Futile.Specflow.Actions.FlaUI;

internal class ScreenCapturer : IScreenCapturer
{
    private readonly bool _takeScreenShots;
    private readonly bool _takeRecording;
    private readonly string? _outputPath;
    private readonly string? _ffmpegExecutable;
    private readonly string? _ffmpegTempFile;
    private readonly string CurrentDateTime = DateTime.Now.ToString("yyyy-MM-dd_Hmmss");

    private bool _isDisposed;
    private bool _recording;
    private VideoRecorder? _videoRecorder;

    internal ScreenCapturer(IConfiguration configuration, ITestRunContext testRunContext)
    {
        if (configuration.Settings.Capturing == null) return;

        _takeScreenShots = configuration.Settings.Capturing!.Type == CapturingType.Screenshot;
        _takeRecording = configuration.Settings.Capturing!.Type == CapturingType.Recording;

        _outputPath = configuration.Settings.Capturing!.OutputPath != null
            ? configuration.Settings.Capturing!.OutputPath
            : Path.Combine(testRunContext.GetTestDirectory(), "Capture");

        _ffmpegExecutable = _takeRecording ? configuration.Settings.Capturing!.FFMPEG! : string.Empty;
        _ffmpegTempFile = Path.GetTempFileName();
    }

    public void Dispose()
    {
        if (!_isDisposed)
        {
            _isDisposed = true;
            _videoRecorder?.Dispose();
        }
    }

    public void StartRecording()
    {
        if (_takeRecording)
        {
            _recording = true;
            _videoRecorder?.Dispose();
            _videoRecorder = new VideoRecorder(
                new VideoRecorderSettings
                {
                    VideoQuality = 26,
                    ffmpegPath = _ffmpegExecutable,
                    TargetVideoPath = _ffmpegTempFile
                },
                r =>
                {
                    var img = Capture.Screen();
                    img.ApplyOverlays(new InfoOverlay(img) { RecordTimeSpan = r.RecordTimeSpan, OverlayStringFormat = @"{rt:hh\:mm\:ss\.fff} / {name} / CPU: {cpu} / RAM: {mem.p.used}/{mem.p.tot} ({mem.p.used.perc})" }, new MouseOverlay(img));
                    return img;
                });
        }
    }

    public void StopRecording(FlaUIDriver driver, FeatureContext featureContext, ScenarioContext scenarioContext)
    {
        if (_recording)
        {
            _videoRecorder?.Stop();
            _videoRecorder?.Dispose();
            _videoRecorder = null;
            _recording = false;

            if (scenarioContext.ScenarioExecutionStatus == ScenarioExecutionStatus.TestError)
            {
                Directory.CreateDirectory(_outputPath!);
                string filename = $"{CurrentDateTime} {featureContext.FeatureInfo.Title} {scenarioContext.ScenarioInfo.Title} {scenarioContext.ScenarioExecutionStatus}.mpg";
                File.Move(_ffmpegTempFile!, Path.Combine(_outputPath!, filename));
            }
            else
            {
                File.Delete(_ffmpegTempFile!);
            }
        }
    }

    public void TakeScreenshot(FlaUIDriver driver, FeatureContext featureContext, ScenarioContext scenarioContext)
    {
        if (_takeScreenShots)
        {
            if (scenarioContext.ScenarioExecutionStatus == ScenarioExecutionStatus.TestError)
            {
                var img = Capture.Screen();
                img.ApplyOverlays(new MouseOverlay(img));

                Directory.CreateDirectory(_outputPath!);
                string filename = $"{CurrentDateTime} {featureContext.FeatureInfo.Title} {scenarioContext.ScenarioInfo.Title} {scenarioContext.ScenarioExecutionStatus}.png";
                img.ToFile(Path.Combine(_outputPath!, filename));
            }
        }
    }
}
