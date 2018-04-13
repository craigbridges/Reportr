namespace Reportr
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
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

            this.ReportName = report.Name;
            this.ReportTitle = report.Title;
            this.ReportDescription = report.Description;
            this.Culture = report.CurrentCulture;
            this.SectionOutputs = sectionOutputs;

            SetResults(sectionOutputs);
        }

        /// <summary>
        /// Gets the reports name
        /// </summary>
        public string ReportName { get; private set; }

        /// <summary>
        /// Gets the reports title
        /// </summary>
        public string ReportTitle { get; private set; }

        /// <summary>
        /// Gets the reports description
        /// </summary>
        public string ReportDescription { get; private set; }

        /// <summary>
        /// Gets the culture used by the report
        /// </summary>
        public CultureInfo Culture { get; private set; }

        /// <summary>
        /// Gets the filter that was used to generate the report
        /// </summary>
        public IReportFilter FilterUsed { get; private set; }

        /// <summary>
        /// Adds the filter used to the report output
        /// </summary>
        /// <param name="filter">The filter that was used</param>
        /// <returns>The updated report output</returns>
        public ReportOutput WithFilter
            (
                IReportFilter filter
            )
        {
            Validate.IsNotNull(filter);

            this.FilterUsed = filter;

            return this;
        }

        /// <summary>
        /// Gets an array of the reports section outputs
        /// </summary>
        public IReportComponentOutput[] SectionOutputs { get; private set; }
        
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

            // Aggregate the error messages from all component outputs
            foreach (var output in sectionOutputs)
            {
                if (output.ErrorMessages != null)
                {
                    foreach (var error in output.ErrorMessages)
                    {
                        errorMessages.Add
                        (
                            error.Key,
                            error.Value
                        );
                    }
                }
            }

            this.Success = success;
            this.ExecutionTime = executionTime;

            this.ErrorMessages = new ReadOnlyDictionary<string, string>
            (
                errorMessages
            );
        }
    }
}
