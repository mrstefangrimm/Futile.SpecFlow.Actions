using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Futile.Specflow.Actions.FlaUI;

internal interface IConfiguration
{
    FlaUISettings Settings { get; }
    Dictionary<string, FlaUIProfile> Profiles { get; }
}

internal struct FlaUISettings
{
    [JsonInclude]
    public FlaUIA? UIA { get; private set; }

    [JsonInclude]
    public ErrorCapturing? Capturing { get; private set; }
}

internal class FlaUIProfile
{
    [JsonInclude]
    public string? App { get; private set; }

    [JsonInclude]
    public string? Arguments { get; private set; }

    [JsonInclude]
    public LaunchCommand? Launch { get; private set; }
}

internal class ErrorCapturing
{
    [JsonInclude]
    public CapturingType? Type { get; private set; }

    [JsonInclude]
    public string? OutputPath { get; private set; }

    [JsonInclude]
    public string? FFMPEG { get; private set; }
}

internal enum FlaUIA
{
    UIA2,
    UIA3,
}

internal enum CapturingType
{
    Screenshot,
    Recording,
}

public enum LaunchCommand
{
    Exe,
    StoreApp,
}
