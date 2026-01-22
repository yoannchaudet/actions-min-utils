using ActionsMinUtils.Tests.utils;
using Xunit.Abstractions;

namespace ActionsMinUtils.Tests;

public class LoggerTests(ITestOutputHelper testOutputHelper) : ConsoleOutputTests(testOutputHelper)
{
    // Logging tests

    [Fact]
    public void TestDebug()
    {
        Logger.Debug("hello");
        Assert.Equal($"::debug::hello{Environment.NewLine}", ConsoleOutput.Output());
    }

    [Fact]
    public void TestError()
    {
        Logger.Error("hello");
        Assert.Equal($"::error::hello{Environment.NewLine}", ConsoleOutput.Output());
    }

    [Fact]
    public void TestFatal()
    {
        var e = Assert.Throws<Exception>(() => Logger.Fatal("Oh no!"));
        Assert.Equal("Oh no!", e.Message);
        Assert.Equal($"::error::Oh no!{Environment.NewLine}", ConsoleOutput.Output());
    }

    [Fact]
    public void TestErrorWithAnnotation()
    {
        Logger.Error("hello",
            new Logger.AnnotationProperties
            { Title = "", File = "foo", StartLine = "1", EndLine = "2", StartColumn = "3", EndColumn = "4" });
        Assert.Equal($"::error title=,file=foo,line=1,endLine=2,col=3,endColumn=4::hello{Environment.NewLine}",
            ConsoleOutput.Output());
    }

    [Fact]
    public void TestWarning()
    {
        Logger.Warning("hello");
        Assert.Equal($"::warning::hello{Environment.NewLine}", ConsoleOutput.Output());
    }

    [Fact]
    public void TestWarningWithAnnotation()
    {
        Logger.Warning("hello",
            new Logger.AnnotationProperties
            { File = "foo", StartLine = "1", EndLine = "2", StartColumn = "3", EndColumn = "4" });
        Assert.Equal($"::warning title=,file=foo,line=1,endLine=2,col=3,endColumn=4::hello{Environment.NewLine}",
            ConsoleOutput.Output());
    }

    [Fact]
    public void TestNotice()
    {
        Logger.Notice("hello");
        Assert.Equal($"::notice::hello{Environment.NewLine}", ConsoleOutput.Output());
    }

    [Fact]
    public void TestNoticeWithAnnotation()
    {
        Logger.Notice("hello",
            new Logger.AnnotationProperties
            { File = "foo", StartLine = "1", EndLine = "2", StartColumn = "3", EndColumn = "4" });
        Assert.Equal($"::notice title=,file=foo,line=1,endLine=2,col=3,endColumn=4::hello{Environment.NewLine}",
            ConsoleOutput.Output());
    }

    [Fact]
    public void TestInfo()
    {
        Logger.Info("hello");
        Assert.Equal($"hello{Environment.NewLine}", ConsoleOutput.Output());
    }

    [Fact]
    public void TestGroup()
    {
        using (Logger.Group("hello"))
        {
            Logger.Info("world");
        }

        Assert.Equal(
            $"::group::hello{Environment.NewLine}world{Environment.NewLine}::endgroup::{Environment.NewLine}",
            ConsoleOutput.Output());
    }

    // CommandEnvelope tests

    [Fact]
    public void CommandEnvelopeToString()
    {
        var envelope = new Logger.CommandEnvelope("test", new Dictionary<string, string> { { "foo", "bar" } }, "hello");
        Assert.Equal("::test foo=bar::hello", envelope.ToString());
    }

    [Fact]
    public void CommandEnvelopeNoParametersToString()
    {
        var envelope = new Logger.CommandEnvelope("test", null, "hello");
        Assert.Equal("::test::hello", envelope.ToString());
    }

    [Fact]
    public void CommandEnvelopeNoParametersNoMessageToString()
    {
        // probably invalid but we'll allow it
        var envelope = new Logger.CommandEnvelope("test", null, "");
        Assert.Equal("::test::", envelope.ToString());
    }

    [Fact]
    public void CommandEnvelopeParametersEscapingToString()
    {
        var envelope = new Logger.CommandEnvelope("test",
            new Dictionary<string, string> { { "a", "b:" }, { "c", "d," } },
            "\r\n%hello%");
        Assert.Equal("::test a=b%3A,c=d%2C::%0D%0A%25hello%25", envelope.ToString());
    }
}