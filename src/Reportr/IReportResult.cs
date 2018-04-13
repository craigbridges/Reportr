namespace Reportr
{
    using System.Collections.Generic;

    /// <summary>
    /// Defines a contract for a report result
    /// </summary>
    public interface IReportResult
    {
        /// <summary>
        /// Gets a flag indicating if everything ran successfully
        /// </summary>
        bool Success { get; }

        /// <summary>
        /// Gets the components execution time
        /// </summary>
        int ExecutionTime { get; }

        /// <summary>
        /// Gets any error messages that were generated
        /// </summary>
        /// <remarks>
        /// The error messages are grouped by error code.
        /// </remarks>
        IDictionary<string, string> ErrorMessages { get; }
    }
}
