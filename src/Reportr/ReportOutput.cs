namespace Reportr
{
    using Reportr.Templates;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;

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
            this.SectionOutputs = sectionOutputs;

            SetResults(sectionOutputs);
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
        /// Gets an array of the reports section outputs
        /// </summary>
        public IReportComponentOutput[] SectionOutputs { get; private set; }

        /// <summary>
        /// Gets the name of the template used to render the report
        /// </summary>
        public string TemplateName { get; private set; }

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
        /// <param name="templateName">The name of the template used</param>
        /// <param name="renderedContent">The rendered content</param>
        /// <param name="renderedContentType">The content type</param>
        /// <returns>The updated report output</returns>
        public ReportOutput WithContent
            (
                string templateName,
                string renderedContent,
                TemplateOutputType renderedContentType
            )
        {
            Validate.IsNotEmpty(templateName);

            this.TemplateName = templateName;
            this.RenderedContent = renderedContent;
            this.RenderedContentType = renderedContentType;
            this.HasRenderedContent = true;

            return this;
        }

        /// <summary>
        /// Gets a flag indicating if the report ran successfully
        /// </summary>
        public bool Success { get; private set; }

        /// <summary>
        /// Gets the reports execution time
        /// </summary>
        public int ExecutionTime { get; private set; }

        /// <summary>
        /// Gets any error messages that were generated
        /// </summary>
        /// <remarks>
        /// The error messages are grouped by component name.
        /// </remarks>
        public Dictionary<string, string> ErrorMessages
        {
            get;
            private set;
        }

        /// <summary>
        /// Sets the report results from the section outputs
        /// </summary>
        /// <param name="sectionOutputs">The section outputs</param>
        private void SetResults
            (
                params IReportComponentOutput[] sectionOutputs
            )
        {
            Validate.IsNotNull(sectionOutputs);
            
            var success = sectionOutputs.All
            (
                output => output.Success
            );

            var executionTime = sectionOutputs.Sum
            (
                output => output.ExecutionTime
            );

            var errorMessages = new Dictionary<string, string>();

            foreach (var output in sectionOutputs)
            {
                errorMessages.Add
                (
                    output.ComponentName,
                    output.ErrorMessage
                );
            }

            this.Success = success;
            this.ExecutionTime = executionTime;
            this.ErrorMessages = errorMessages;
        }
    }
}
