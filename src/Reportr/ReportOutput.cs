namespace Reportr
{
    using Reportr.Templates;
    using System.Globalization;
    
    /// <summary>
    /// Represents the default implementation for a report output
    /// </summary>
    public class ReportOutput : IReportOutput
    {
        /// <summary>
        /// Constructs the report output with the details
        /// </summary>
        /// <param name="report">The report</param>
        /// <param name="sectionOutputs">The section outputs</param>
        public ReportOutput
            (
                IReport report,
                params IReportComponentOutput[] sectionOutputs
            )
        {
            Validate.IsNotNull(report);
            Validate.IsNotNull(sectionOutputs);

            this.Name = report.Name;
            this.Title = report.Title;
            this.Description = report.Description;
            this.Culture = report.CurrentCulture;
            this.ColumnCount = report.ColumnCount;
            this.SectionOutputs = sectionOutputs;
        }

        /// <summary>
        /// Gets the reports name
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Gets the reports title
        /// </summary>
        public string Title { get; private set; }

        /// <summary>
        /// Gets the reports description
        /// </summary>
        public string Description { get; private set; }

        /// <summary>
        /// Gets the culture used by the report
        /// </summary>
        public CultureInfo Culture { get; private set; }

        /// <summary>
        /// Gets the number of columns in the report
        /// </summary>
        /// <remarks>
        /// The column count is used to determine how the report
        /// is laid out. A column count of 1 indicates that each
        /// section will appear on a separate row.
        /// 
        /// Reports with column counts greater than 1 will 
        /// automatically push sections onto new rows once the
        /// column count has been reached.
        /// </remarks>
        public int? ColumnCount { get; private set; }

        /// <summary>
        /// Gets an array of the reports section outputs
        /// </summary>
        public IReportComponentOutput[] SectionOutputs { get; private set; }

        /// <summary>
        /// Gets the reports rendered content
        /// </summary>
        public string RenderedContent { get; private set; }

        /// <summary>
        /// Gets the reports rendered content type
        /// </summary>
        public TemplateOutputType? RenderedContentType { get; private set; }
        
        /// <summary>
        /// Gets a flag indicating if then rendered content has been set
        /// </summary>
        public bool HasRenderedContent { get; private set; }

        /// <summary>
        /// Adds the rendered content to the report output
        /// </summary>
        /// <param name="renderedContent">The rendered content</param>
        /// <param name="renderedContentType">The content type</param>
        /// <returns>The updated report output</returns>
        public ReportOutput WithContent
            (
                string renderedContent,
                TemplateOutputType renderedContentType
            )
        {
            this.RenderedContent = renderedContent;
            this.RenderedContentType = renderedContentType;
            this.HasRenderedContent = true;

            return this;
        }
    }
}
