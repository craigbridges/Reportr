namespace Reportr
{
    using System.Linq;

    /// <summary>
    /// Represents the result of a report execution
    /// </summary>
    public class ReportExecutionResult
    {
        /// <summary>
        /// Constructs an unsuccessful result with the errors
        /// </summary>
        /// <param name="executionTime">The execution time</param>
        /// <param name="errorMessages">The error messages</param>
        internal ReportExecutionResult
            (
                long executionTime,
                params string[] errorMessages
            )
        {
            Validate.IsNotNull(errorMessages);
            
            this.ExecutionTime = executionTime;
            this.ErrorMessages = errorMessages;

            if (errorMessages.Any())
            {
                this.Success = false;
            }
            else
            {
                this.Success = true;
            }
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
        public string[] ErrorMessages { get; private set; }
    }
}
