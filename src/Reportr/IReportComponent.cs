namespace Reportr
{
    /// <summary>
    /// Defines a contract for a report component
    /// </summary>
    public interface IReportComponent
    {
        /// <summary>
        /// Gets the components execution time
        /// </summary>
        int ExecutionTime { get; }

        /// <summary>
        /// Gets a flag indicating if the component ran successfully
        /// </summary>
        bool Success { get; }

        /// <summary>
        /// Gets the error message that was generated
        /// </summary>
        string ErrorMessage { get; }
    }
}
