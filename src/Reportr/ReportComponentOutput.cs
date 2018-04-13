namespace Reportr
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;

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

        /// <summary>
        /// Adds the fields to the report component output
        /// </summary>
        /// <param name="fields">The fields to add</param>
        /// <returns>The updated component output</returns>
        public ReportComponentOutput WithFields
            (
                IDictionary<string, object> fields
            )
        {
            Validate.IsNotNull(fields);

            this.Fields = new ReadOnlyDictionary<string, object>
            (
                fields
            );

            return this;
        }

        /// <summary>
        /// Gets a dictionary of report component fields
        /// </summary
        /// <remarks>
        /// The component fields are a collection of name-values, 
        /// where the value can be of any type.
        /// </remarks>
        public IDictionary<string, object> Fields { get; private set; }
    }
}
