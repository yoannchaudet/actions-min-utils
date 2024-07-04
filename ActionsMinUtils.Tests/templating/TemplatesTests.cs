using ActionsMinUtils.templating;

namespace ActionsMinUtils.Tests.templating;

public class TemplatesTests
{
    [Fact]
    public void TextTemplateTrimsAndReplacesVariables()
    {
        var template = Templates.RenderLiquidFromText(" Hello {{ name }} ", new { Name = "Yoann" });
        Assert.Equal("Hello Yoann", template);
    }

    [Fact]
    public void MarkerUtils()
    {
        var marker = new Marker("hello").AddArgument("foo", "bar");
        var template =
            Templates.RenderLiquidFromText(
                "{{ markers.new 'hello' | markers.add_argument 'foo' 'bar' | markers.to_string }}");
        Assert.Equal(new MarkersParser().ToText(marker), template);
    }

    [Fact]
    public void MarkerJoining()
    {
        var people = new[] { "1", "2" }.ToList();
        var separator = '-';
        var marker = new Marker("hello").AddArgument("foo", "bar")
            .AddArgument("people", string.Join(separator, people));
        var template =
            Templates.RenderLiquidFromText(
                "{{ markers.new 'hello' | markers.add_argument 'foo' 'bar' | markers.join_argument 'people', people, separator | markers.to_custom_string '# MARKER' '' }}",
                new
                {
                    People = people,
                    Separator = separator
                });
        Assert.Equal(new MarkersParser("# MARKER", "").ToText(marker), template);
    }
}