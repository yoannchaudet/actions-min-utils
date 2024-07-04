using ActionsMinUtils.templating;

namespace ActionsMinUtils.Tests.templating;

public class MarkersParserTests
{
    [Fact]
    public void ToTextSimple()
    {
        var marker = new Marker("test");

        // default prefix/suffix
        Assert.Equal("<!-- test -->", new MarkersParser().ToText(marker));

        // yaml friendly prefix
        Assert.Equal("# MARKER test", new MarkersParser("# MARKER", "").ToText(marker));
    }

    [Fact]
    public void ToTextArguments()
    {
        var parser = new MarkersParser();
        var marker = new Marker("test").AddArgument("foo", "bar");
        Assert.Equal("<!-- test foo=bar -->", parser.ToText(marker));
        marker = new Marker("test").AddArgument("foo", "bar").AddArgument("unsafe&", "?");
        Assert.Equal("<!-- test foo=bar&unsafe&=%3f -->", parser.ToText(marker));
    }

    [Fact]
    public void FromTextSimple()
    {
        var parser = new MarkersParser();
        var text = "<!-- hello -->";

        var markers = parser.FromText(text).ToList();
        Assert.Single(markers);
        Assert.Equal("hello", markers.First().Name);
        Assert.Equal(0, markers.First().Start);
        Assert.Equal(1, markers.First().Line);
        Assert.Equal(text.Length, markers.First().Length);
        Assert.Equal(text.Length, markers.First().End);
    }

    [Fact]
    public void FromTextComplex()
    {
        var text = @"
# Hello

<!-- marker1 date=2020-05-16 -->

- item 1
- item 2 <!-- marker2 -->

<!-- /marker3 -->
@";
        var parser = new MarkersParser();
        var markers = parser.FromText(text).ToList();
        Assert.Equal(3, markers.Count());
        var marker1 = markers.First(m => m.Name == "marker1");
        var marker2 = markers.First(m => m.Name == "marker2");
        var marker3 = markers.First(m => m.Name == "/marker3");

        // marker1
        Assert.Equal("2020-05-16", marker1.Arguments.First(a => a.Key == "date").Value);
        Assert.Equal(10, marker1.Start);
        Assert.Equal(4, marker1.Line);
        Assert.Equal("<!-- marker1 date=2020-05-16 -->".Length, marker1.Length);

        // marker2
        Assert.Empty(marker2.Arguments);
        Assert.Equal(62, marker2.Start);
        Assert.Equal(7, marker2.Line);

        // marker3
        Assert.Empty(marker3.Arguments);
        Assert.Equal(80, marker3.Start);
        Assert.Equal(9, marker3.Line);
    }
}