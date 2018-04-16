namespace Reportr.Components
{
    using System.Collections.Generic;

    /// <summary>
    /// Defines a contract for a report component output
    /// </summary>
    public interface IReportComponentOutput : IReportExecutionResult
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
        /// Gets a dictionary of report component fields
        /// </summary
        /// <remarks>
        /// The component fields are a collection of name-values, 
        /// where the value can be of any type.
        /// </remarks>
        IDictionary<string, object> Fields { get; }
    }
}
