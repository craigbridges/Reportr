namespace Reportr.Components
{
    using Reportr.Data.Querying;
    
    /// <summary>
    /// Represents a base class for the report component output
    /// </summary>
    public abstract class ReportComponentOutputBase : IReportComponentOutput
    {
        /// <summary>
        /// Constructs the component output with the details
        /// </summary>
        /// <param name="component">The component that generated the output</param>
        /// <param name="results">The results used to generate the output</param>
        protected ReportComponentOutputBase
            (
                IReportComponent component,
                QueryResults results
            )
        {
            Validate.IsNotNull(component);
            Validate.IsNotNull(results);

            this.Component = component;
            this.Results = results;
        }

        /// <summary>
        /// Gets the component that generated the output
        /// </summary>
        public IReportComponent Component { get; protected set; }

        /// <summary>
        /// Gets the query results used to generate the output
        /// </summary>
        public QueryResults Results { get; protected set; }
    }
}
