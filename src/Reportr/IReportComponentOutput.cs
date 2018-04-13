namespace Reportr
{
    using System.Collections.Generic;

    /// <summary>
    /// Defines a contract for a report component output
    /// </summary>
    public interface IReportComponentOutput
    {
        /// <summary>
        /// Gets the name of the component that generated the output
        /// </summary>
        string ComponentName { get; }

        /// <summary>
        /// Gets the type of the component that generated the output
        /// </summary>
        ReportComponentType ComponentType { get; }

        /// <summary>
        /// Gets a flag indicating if the component ran successfully
        /// </summary>
        bool Success { get; }

        /// <summary>
        /// Gets the components execution time
        /// </summary>
        int ExecutionTime { get; }

        /// <summary>
        /// Gets the error message that was generated
        /// </summary>
        string ErrorMessage { get; }

        /// <summary>
        /// Gets a dictionary of report component fields
        /// </summary
        /// <remarks>
        /// The component fields are a collection of name-values, 
        /// where the value can be of any type.
        /// </remarks>
        IDictionary<string, object> Fields { get; }
    }
}
