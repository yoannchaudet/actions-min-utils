using ActionsMinUtils.Tests.utils;
using NSubstitute;
using Xunit.Abstractions;

namespace ActionsMinUtils.Tests;

public class ActionContextTests(ITestOutputHelper testOutputHelper) : ConsoleOutputTests(testOutputHelper)
{
    [Theory]
    [InlineData("test", "value")]
    [InlineData("test", "")]
    [InlineData("GITHUB_TOKEN", null)]
    public void CanReadEnvironmentVariable(string variable, string? value)
    {
        // Mock system env variable
        var ctx = Substitute.ForPartsOf<ActionContext>();
        ctx.GetSystemEnvironmentVariable(variable).Returns(value);

        Assert.Equal(value, ctx.GetEnvironmentVariable(variable));
    }

    [Fact]
    public void ValidatesEnvironmentVariable()
    {
        // Mock system env variable
        var ctx = Substitute.ForPartsOf<ActionContext>();
        ctx.GetSystemEnvironmentVariable(Arg.Any<string>()).Returns((string?)null);

        var e = Assert.Throws<Exception>(() => ctx.GetEnvironmentVariable("test", true));
        Assert.Equal("Unable to read environment variable 'test' marked as required", e.Message);
    }

    [Theory]
    [InlineData("test", "INPUT_TEST", "test value")]
    [InlineData("greetings-from-hell", "INPUT_GREETINGS-FROM-HELL", "hello, world")]
    [InlineData("X", "INPUT_X", "x")]
    public void CanReadInput(string input, string variable, string? value)
    {
        // Mock system env variable
        var ctx = Substitute.ForPartsOf<ActionContext>();
        ctx.GetSystemEnvironmentVariable(variable).Returns(value);

        Assert.Equal(value, ctx.GetInput(input));
    }


    [Fact]
    public void ValidatesInput()
    {
        // Mock system env variable
        var ctx = Substitute.ForPartsOf<ActionContext>();
        ctx.GetSystemEnvironmentVariable(Arg.Any<string>()).Returns((string?)null);

        var e = Assert.Throws<Exception>(() => ctx.GetInput("test", true));
        Assert.Equal("Unable to read input 'test' marked as required", e.Message);
    }

    [Fact]
    public void TryCreate1()
    {
        Assert.NotNull(ActionContext.TryCreate<Ctx1>());
    }

    [Fact]
    public void TryCreate2()
    {
        var ctx = ActionContext.TryCreate<Ctx2>();
        Assert.Throws<Exception>(() =>
        {
            var s = ctx!.Something;
        });
    }

    private class Ctx1 : ActionContext
    {
    }

    private class Ctx2 : ActionContext
    {
        public string Something => GetInput("something", true)!;
    }
}