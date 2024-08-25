using FluentAssertions;
using Xunit;

namespace Futile.SpecFlow.Actions.Configuration.UnitTests;

public class TargetNameExtractorTests
{
    [Theory]
    [InlineData("specflow.actions.edge.json", "edge")]
    [InlineData("specflow.actions.edge.windows.json", "edge.windows")]
    public void Extract(string actual, string expected)
    {
        var targetNameExtractor = new TargetNameExtractor();
        var extractedTargetName = targetNameExtractor.Extract(actual);
        extractedTargetName.Should().Be(expected);
    }
}