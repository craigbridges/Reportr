namespace Reportr.Rendering
{
    using Reportr.Templating;

    /// <summary>
    /// Defines a contract for a service that renders reports
    /// </summary>
    public interface IReportRenderer
    {
        /// <summary>
        /// Renders a report using the template specified
        /// </summary>
        /// <param name="report">The report</param>
        /// <param name="template">The report template</param>
        /// <returns>The rendered report</returns>
        IRenderedReport Render
        (
            Report report,
            ReportTemplate template
        );
    }
}
