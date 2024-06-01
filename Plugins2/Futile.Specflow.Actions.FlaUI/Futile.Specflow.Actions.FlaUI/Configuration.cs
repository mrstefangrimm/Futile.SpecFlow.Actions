using SpecFlow.Actions.Configuration;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Futile.Specflow.Actions.FlaUI;

internal class Configuration : IConfiguration {
  private readonly ISpecFlowActionJsonLoader _specFlowActionJsonLoader;
  private readonly Lazy<SpecFlowActionJson> _jsonObjectLazy;

  internal Configuration(ISpecFlowActionJsonLoader specFlowActionJsonLoader) {
    _specFlowActionJsonLoader = specFlowActionJsonLoader;
    _jsonObjectLazy = new Lazy<SpecFlowActionJson>(LoadSpecFlowJson);
  }

  public FlaUISettings Settings => _jsonObjectLazy.Value.FlaUI!.Settings!.Value;
  public Dictionary<string, FlaUIProfile> Profiles => _jsonObjectLazy.Value.FlaUI!.Profiles!;

  private SpecFlowActionJson LoadSpecFlowJson() {
    var json = _specFlowActionJsonLoader.Load();
    if (json == null) { throw new InvalidOperationException("Failed to load specflow.actions.json"); }

    var jsonSerializerOptions = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
    jsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());

    var specflowActionConfig = JsonSerializer.Deserialize<SpecFlowActionJson>(json, jsonSerializerOptions);
    if (specflowActionConfig == null) { throw new InvalidOperationException("Failed deserialize specflow.actions.json"); }

    return specflowActionConfig;
  }

  private class SpecFlowActionJson {
    [JsonInclude]
    public FlaUIObject? FlaUI { get; private set; }
  }

  private class FlaUIObject {
    [JsonInclude]
    public FlaUISettings? Settings { get; private set; }

    [JsonInclude]
    public Dictionary<string, FlaUIProfile>? Profiles { get; private set; }
  }
}
