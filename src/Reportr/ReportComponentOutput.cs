namespace Reportr
{
    /// <summary>
    /// Represents a base class for the report component output
    /// </summary>
    public abstract class ReportComponentOutput : IReportComponentOutput
    {
        /// <summary>
        /// Constructs the result with the details
        /// </summary>
        /// <param name="component">The component that generated the output</param>
        /// <param name="executionTime">The execution time in milliseconds</param>
        /// <param name="success">True, if the query executed successfully</param>
        /// <param name="errorMessage">The error message, if there was one</param>
        protected ReportComponentOutput
            (
                IReportComponent component,
                int executionTime,
                bool success = true,
                string errorMessage = null
            )
        {
            Validate.IsNotNull(component);

            this.ComponentName = component.Name;
            this.ComponentType = component.ComponentType;
            this.ExecutionTime = executionTime;
            this.Success = success;
            this.ErrorMessage = errorMessage;
        }

        /// <summary>
        /// Gets the name of the component that generated the output
        /// </summary>
        public string ComponentName { get; private set; }

        /// <summary>
        /// Gets the type of the component that generated the output
        /// </summary>
        public ReportComponentType ComponentType { get; private set; }

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
