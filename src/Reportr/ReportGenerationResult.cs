namespace Reportr
{
    using System.Collections.Generic;

    /// <summary>
    /// Represents the result of a report generation
    /// </summary>
    public sealed class ReportGenerationResult
    {
        /// <summary>
        /// Constructs a successful result with the report
        /// </summary>
        /// <param name="executionTime">The execution time</param>
        /// <param name="report">The report generated</param>
        internal ReportGenerationResult
            (
                int executionTime,
                Report report
            )
        {
            Validate.IsNotNull(report);

            this.Success = true;
            this.ExecutionTime = executionTime;
            this.Report = report;
        }

        /// <summary>
        /// Constructs an unsuccessful result with the errors
        /// </summary>
        /// <param name="executionTime">The execution time</param>
        /// <param name="errorMessages">The error messages</param>
        internal ReportGenerationResult
            (
                int executionTime,
                Dictionary<string, string> errorMessages
            )
        {
            Validate.IsNotNull(errorMessages);

            this.Success = false;
            this.ExecutionTime = executionTime;
            this.ErrorMessages = errorMessages;
        }

        /// <summary>
        /// Gets a flag indicating if everything ran successfully
        /// </summary>
        public bool Success { get; private set; }

        /// <summary>
        /// Gets the components execution time
        /// </summary>
        public int ExecutionTime { get; private set; }

        /// <summary>
        /// Gets any error messages that were generated
        /// </summary>
        /// <remarks>
        /// The error messages are grouped by error code.
        /// </remarks>
        public Dictionary<string, string> ErrorMessages
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the report that was generated
        /// </summary>
        public Report Report { get; private set; }
    }
}
