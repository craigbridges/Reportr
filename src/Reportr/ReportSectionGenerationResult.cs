namespace Reportr
{
    using System;

    /// <summary>
    /// Represents the result of a report section generation
    /// </summary>
    internal sealed class ReportSectionGenerationResult : ReportExecutionResult
    {
        /// <summary>
        /// Constructs a successful result with the report
        /// </summary>
        /// <param name="section">The report section generated</param>
        /// <param name="executionTime">The execution time</param>
        /// <param name="errorMessages">The error messages</param>
        internal ReportSectionGenerationResult
            (
                ReportSection section,
                long executionTime,
                params string[] errorMessages
            )

            : base(executionTime, errorMessages)
        {
            Validate.IsNotNull(section);

            this.Section = section;
        }

        /// <summary>
        /// Gets the report section that was generated
        /// </summary>
        public ReportSection Section { get; private set; }
    }
}
