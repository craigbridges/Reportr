namespace Reportr
{
    using System.Collections.Generic;

    /// <summary>
    /// Represents the result of a report section generation
    /// </summary>
    internal sealed class ReportSectionGenerationResult : ReportExecutionResult
    {
        /// <summary>
        /// Constructs a successful result with the report
        /// </summary>
        /// <param name="executionTime">The execution time</param>
        /// <param name="section">The report section generated</param>
        internal ReportSectionGenerationResult
            (
                long executionTime,
                ReportSection section
            )

            : base(executionTime)
        {
            Validate.IsNotNull(section);

            this.Section = section;
        }

        /// <summary>
        /// Constructs an unsuccessful result with the errors
        /// </summary>
        /// <param name="executionTime">The execution time</param>
        /// <param name="errorMessages">The error messages</param>
        internal ReportSectionGenerationResult
            (
                long executionTime,
                Dictionary<string, string> errorMessages
            )

            : base(executionTime, errorMessages)
        { }

        /// <summary>
        /// Gets the report section that was generated
        /// </summary>
        public ReportSection Section { get; private set; }
    }
}
