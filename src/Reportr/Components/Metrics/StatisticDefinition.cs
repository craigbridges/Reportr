namespace Reportr.Components.Metrics
{
    using Reportr.Data;
    using Reportr.Data.Querying;
    using System;

    /// <summary>
    /// Represents the definition of a single statistic
    /// </summary>
    public class StatisticDefinition : ReportComponentBase
    {
        /// <summary>
        /// Constructs the statistic with the details
        /// </summary>
        /// <param name="name">The name</param>
        /// <param name="title">The title</param>
        public StatisticDefinition
            (
                string name,
                string title
            )
            : base(name, title)
        { }

        /// <summary>
        /// Gets the data binding for the statistic
        /// </summary>
        public DataBinding Binding { get; private set; }

        /// <summary>
        /// Sets the statistics data binding
        /// </summary>
        /// <param name="binding">The data binding</param>
        public void SetBinding
            (
                DataBinding binding
            )
        {
            Validate.IsNotNull(binding);

            this.Binding = binding;
        }

        /// <summary>
        /// Gets the component type
        /// </summary>
        public override ReportComponentType ComponentType
        {
            get
            {
                return ReportComponentType.Statistic;
            }
        }

        /// <summary>
        /// Generates the component output from the results of a query
        /// </summary>
        /// <param name="results">The query results</param>
        /// <returns>The output generated</returns>
        public override IReportComponentOutput GenerateOutput
            (
                QueryResults results
            )
        {
            // TODO: apply the binding to the query results

            throw new NotImplementedException();
        }
    }
}
