namespace Reportr.Rendering
{
    using Reportr.Templating;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;

    /// <summary>
    /// Represents the default implementation for a rendered report
    /// </summary>
    public class RenderedReport : IRenderedReport
    {
        /// <summary>
        /// Constructs the rendered report with the details
        /// </summary>
        /// <param name="report">The report</param>
        /// <param name="templateName">The name of the template used</param>
        /// <param name="renderedContent">The rendered content</param>
        /// <param name="renderedContentType">The content type</param>
        public RenderedReport
            (
                Report report,
                string templateName,
                string renderedContent,
                TemplateOutputType renderedContentType
            )
        {
            Validate.IsNotNull(report);
            Validate.IsNotEmpty(templateName);

            this.Report = report;
            this.TemplateName = templateName;
            this.RenderedContent = renderedContent;
            this.RenderedContentType = renderedContentType;
        }

        /// <summary>
        /// Gets the report that was generated
        /// </summary>
        public Report Report { get; private set; }

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
        /// Adds the render results to the rendered report
        /// </summary>
        /// <param name="success">True, if the rendering was successful</param>
        /// <param name="executionTime">The execution time, in milliseconds</param>
        /// <param name="errorMessages">Any error messages generated</param>
        /// <returns>The updated rendered report</returns>
        public RenderedReport WithResults
            (
                bool success,
                int executionTime,
                IDictionary<string, string> errorMessages = null
            )
        {
            this.Success = success;
            this.ExecutionTime = executionTime;

            if (errorMessages != null)
            {
                this.ErrorMessages = new ReadOnlyDictionary<string, string>
                (
                    errorMessages
                );
            }

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
        /// The error messages are grouped by error code.
        /// </remarks>
        public IDictionary<string, string> ErrorMessages
        {
            get;
            private set;
        }
    }
}
