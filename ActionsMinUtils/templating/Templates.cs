using System.Text;
using Scriban;
using Scriban.Runtime;

namespace ActionsMinUtils.templating;

/// <summary>
///     Templates utilities.
/// </summary>
public static class Templates
{
  /// <summary>
  ///     Render a liquid template from a string.
  /// </summary>
  /// <param name="text">Template to render.</param>
  /// <param name="context">Optional context to pass to the template.</param>
  /// <returns></returns>
  public static string RenderLiquidFromText(string text, object? context = null)
    {
        // Render
        var template = Template.ParseLiquid(text);
        return RenderTemplate(template, context);
    }

  /// <summary>
  ///     Render a Liquid template from a file.
  /// </summary>
  /// <param name="path">Path to the file.</param>
  /// <param name="context">Optional context to pass to the template.</param>
  /// <returns></returns>
  public static string RenderLiquidFromFile(string path, object? context = null)
    {
        // Validate the file exists
        if (!File.Exists(path))
            Logger.Fatal($"File {path} does not exist, unable to parse Liquid template", new Logger.AnnotationProperties
            {
                File = path
            });

        // Render
        var template = Template.ParseLiquid(File.ReadAllText(path, Encoding.UTF8), path);
        return RenderTemplate(template, context);
    }

    private static string RenderTemplate(Template template, object? context)
    {
        // Don't render the template if it has errors
        if (template.HasErrors)
            Logger.Fatal($"Error parsing Liquid template{Environment.NewLine}{template.Messages}",
                new Logger.AnnotationProperties
                {
                    File = template.SourceFilePath
                });

        // Enrich the passed context
        var enrichedContext = new ScriptObject();
        if (context != null)
            enrichedContext.Import(context);
        enrichedContext.Add("markers", new MarkersScriptObject());

        // Return the rendered template
        return template.Render(enrichedContext).Trim();
    }
}