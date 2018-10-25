namespace Reportr.Rendering
{
    using Reportr.Templating;

    /// <summary>
    /// Defines a contract for a rendered report
    /// </summary>
    public interface IRenderedReport
    {
        /// <summary>
        /// Gets the report that was generated
        /// </summary>
        Report Report { get; }

        /// <summary>
        /// Gets the name of the template used to render the report
        /// </summary>
        string TemplateName { get; }

        /// <summary>
        /// Gets the reports rendered content
        /// </summary>
        string RenderedContent { get; }

        /// <summary>
        /// Gets the reports rendered content type
        /// </summary>
        TemplateOutputType? RenderedContentType { get; }
    }
}
