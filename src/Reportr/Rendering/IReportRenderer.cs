namespace Reportr.Rendering
{
    using Reportr.Templates;

    /// <summary>
    /// Defines a contract for a service that renders reports
    /// </summary>
    public interface IReportRenderer
    {
        /// <summary>
        /// Renders the report output using a template
        /// </summary>
        /// <param name="output">The report output</param>
        /// <param name="template">The report template</param>
        /// <returns>The rendered report</returns>
        IRenderedReport Render
        (
            IReportOutput output,
            ReportTemplate template
        );
    }
}
