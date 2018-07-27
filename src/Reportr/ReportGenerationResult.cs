namespace Reportr
{
    /// <summary>
    /// Represents the result of a report generation
    /// </summary>
    public sealed class ReportGenerationResult : ReportExecutionResult
    {
        /// <summary>
        /// Constructs a successful result with the report
        /// </summary>
        /// <param name="report">The report generated</param>
        /// <param name="executionTime">The execution time</param>
        /// <param name="errorMessages">The error messages</param>
        public ReportGenerationResult
            (
                Report report,
                long executionTime,
                params string[] errorMessages
            )

            : base(executionTime, errorMessages)
        {
            this.Report = report;
        }
        
        /// <summary>
        /// Gets the report that was generated
        /// </summary>
        public Report Report { get; private set; }
    }
}
