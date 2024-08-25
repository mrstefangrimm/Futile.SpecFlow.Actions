using System.Text.Json.Serialization;

namespace Futile.SpecFlow.Actions.WindowsAppDriver.Configuration;

public class SpecFlowActionJson
{
    [JsonInclude]
    public WindowsAppDriverJsonPart WindowsAppDriver { get; private set; } = new WindowsAppDriverJsonPart();
}
