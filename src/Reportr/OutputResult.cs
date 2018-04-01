namespace Reportr
{
    /// <summary>
    /// Represents a base class for the output result of an execution
    /// </summary>
    public abstract class OutputResult : IReportComponent
    {
        /// <summary>
        /// Constructs the result with the details
        /// </summary>
        /// <param name="executionTime">The execution time in milliseconds</param>
        /// <param name="success">True, if the query executed successfully</param>
        /// <param name="errorMessage">The error message, if there was one</param>
        protected OutputResult
            (
                int executionTime,
                bool success = true,
                string errorMessage = null
            )
        {
            this.ExecutionTime = executionTime;
            this.Success = success;
            this.ErrorMessage = errorMessage;
        }

        /// <summary>
        /// Gets the components execution time
        /// </summary>
        public int ExecutionTime { get; private set; }

        /// <summary>
        /// Gets a flag indicating if the component ran successfully
        /// </summary>
        public bool Success { get; private set; }

        /// <summary>
        /// Gets the error message that was generated
        /// </summary>
        public string ErrorMessage { get; private set; }
    }
}
