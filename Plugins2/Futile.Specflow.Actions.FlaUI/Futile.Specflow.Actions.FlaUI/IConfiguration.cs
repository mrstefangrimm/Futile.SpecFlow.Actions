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

internal enum FlaUIA
{
    UIA2,
    UIA3,
}

public enum LaunchCommand
{
    Exe,
    StoreApp,
}
