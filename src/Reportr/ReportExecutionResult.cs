namespace Reportr
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;

    /// <summary>
    /// Represents the result of a report execution
    /// </summary>
    public class ReportExecutionResult
    {
        /// <summary>
        /// Constructs a successful result with the report
        /// </summary>
        /// <param name="executionTime">The execution time</param>
        internal ReportExecutionResult
            (
                long executionTime
            )
        {
            this.Success = true;
            this.ExecutionTime = executionTime;
        }

        /// <summary>
        /// Constructs an unsuccessful result with the errors
        /// </summary>
        /// <param name="executionTime">The execution time</param>
        /// <param name="errorMessages">The error messages</param>
        internal ReportExecutionResult
            (
                long executionTime,
                Dictionary<string, string> errorMessages
            )
        {
            Validate.IsNotNull(errorMessages);

            this.Success = false;
            this.ExecutionTime = executionTime;

            this.ErrorMessages = new ReadOnlyDictionary<string, string>
            (
                errorMessages
            );
        }

        /// <summary>
        /// Gets a flag indicating if everything ran successfully
        /// </summary>
        public bool Success { get; private set; }

        /// <summary>
        /// Gets the components execution time (in milliseconds)
        /// </summary>
        public long ExecutionTime { get; private set; }

        /// <summary>
        /// Gets any error messages that were generated
        /// </summary>
        /// <remarks>
        /// The error messages are grouped by report component name.
        /// </remarks>
        public ReadOnlyDictionary<string, string> ErrorMessages
        {
            get;
            private set;
        }
    }
}
