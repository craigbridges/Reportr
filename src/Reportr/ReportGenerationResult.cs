namespace Reportr
{
    using System.Collections.Generic;

    /// <summary>
    /// Represents the result of a report generation
    /// </summary>
    public sealed class ReportGenerationResult : ReportExecutionResult
    {
        /// <summary>
        /// Constructs a successful result with the report
        /// </summary>
        /// <param name="executionTime">The execution time</param>
        /// <param name="report">The report generated</param>
        public ReportGenerationResult
            (
                long executionTime,
                Report report
            )

            : base(executionTime)
        {
            Validate.IsNotNull(report);
            
            this.Report = report;
        }

        /// <summary>
        /// Constructs an unsuccessful result with the errors
        /// </summary>
        /// <param name="executionTime">The execution time</param>
        /// <param name="errorMessages">The error messages</param>
        public ReportGenerationResult
            (
                long executionTime,
                Dictionary<string, string> errorMessages
            )

            : base(executionTime, errorMessages)
        { }

        /// <summary>
        /// Constructs an unsuccessful result with a single error
        /// </summary>
        /// <param name="executionTime">The execution time</param>
        /// <param name="areaName">The error area name</param>
        /// <param name="errorMessage">The error message</param>
        public ReportGenerationResult
            (
                long executionTime,
                string areaName,
                string errorMessage
            )

            : base(executionTime, areaName,  errorMessage)
        { }

        /// <summary>
        /// Gets the report that was generated
        /// </summary>
        public Report Report { get; private set; }
    }
}
