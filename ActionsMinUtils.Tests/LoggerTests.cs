namespace ActionsMinUtils.Tests;

public class LoggerTests
{
    // Logging tests

    [Fact]
    public void TestDebug()
    {
        var output = new StringWriter();
        Console.SetOut(output);
        Logger.Debug("hello");
        Assert.Equal($"::debug::hello{Environment.NewLine}", output.ToString());
    }

    [Fact]
    public void TestError()
    {
        var output = new StringWriter();
        Console.SetOut(output);
        Logger.Error("hello");
        Assert.Equal($"::error::hello{Environment.NewLine}", output.ToString());
    }

    [Fact]
    public void TestFatal()
    {
        var output = new StringWriter();
        Console.SetOut(output);
        var e = Assert.Throws<Exception>(() => Logger.Fatal("Oh no!"));
        Assert.Equal("Oh no!", e.Message);
        Assert.Equal($"::error::Oh no!{Environment.NewLine}", output.ToString());
    }

    [Fact]
    public void TestErrorWithAnnotation()
    {
        var output = new StringWriter();
        Console.SetOut(output);
        Logger.Error("hello",
            new Logger.AnnotationProperties
                { Title = "", File = "foo", StartLine = "1", EndLine = "2", StartColumn = "3", EndColumn = "4" });
        Assert.Equal($"::error title=,file=foo,line=1,endLine=2,col=3,endColumn=4::hello{Environment.NewLine}",
            output.ToString());
    }

    [Fact]
    public void TestWarning()
    {
        var output = new StringWriter();
        Console.SetOut(output);
        Logger.Warning("hello");
        Assert.Equal($"::warning::hello{Environment.NewLine}", output.ToString());
    }

    [Fact]
    public void TestWarningWithAnnotation()
    {
        var output = new StringWriter();
        Console.SetOut(output);
        Logger.Warning("hello",
            new Logger.AnnotationProperties
                { File = "foo", StartLine = "1", EndLine = "2", StartColumn = "3", EndColumn = "4" });
        Assert.Equal($"::warning title=,file=foo,line=1,endLine=2,col=3,endColumn=4::hello{Environment.NewLine}",
            output.ToString());
    }

    [Fact]
    public void TestNotice()
    {
        var output = new StringWriter();
        Console.SetOut(output);
        Logger.Notice("hello");
        Assert.Equal($"::notice::hello{Environment.NewLine}", output.ToString());
    }

    [Fact]
    public void TestNoticeWithAnnotation()
    {
        var output = new StringWriter();
        Console.SetOut(output);
        Logger.Notice("hello",
            new Logger.AnnotationProperties
                { File = "foo", StartLine = "1", EndLine = "2", StartColumn = "3", EndColumn = "4" });
        Assert.Equal($"::notice title=,file=foo,line=1,endLine=2,col=3,endColumn=4::hello{Environment.NewLine}",
            output.ToString());
    }

    [Fact]
    public void TestInfo()
    {
        var output = new StringWriter();
        Console.SetOut(output);
        Logger.Info("hello");
        Assert.Equal($"hello{Environment.NewLine}", output.ToString());
    }

    [Fact]
    public void TestGroup()
    {
        var output = new StringWriter();
        Console.SetOut(output);
        using (Logger.Group("hello"))
        {
            Logger.Info("world");
        }

        Assert.Equal(
            $"::group name=hello::{Environment.NewLine}world{Environment.NewLine}::endgroup::{Environment.NewLine}",
            output.ToString());
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