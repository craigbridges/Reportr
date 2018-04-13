namespace Reportr.Rendering
{
    using Reportr.Templates;

    /// <summary>
    /// Defines a contract for a rendered report
    /// </summary>
    public interface IRenderedReport : IReportResult
    {
        /// <summary>
        /// Gets the report output that was generated
        /// </summary>
        IReportOutput ReportOutput { get; }

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
