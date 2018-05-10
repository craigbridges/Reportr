namespace Reportr.Components.Metrics
{
    using Reportr.Data.Querying;

    /// <summary>
    /// Represents the definition of a single report statistic
    /// </summary>
    public class StatisticDefinition : ReportComponentBase
    {
        /// <summary>
        /// Constructs the statistic definition with the details
        /// </summary>
        /// <param name="name">The name</param>
        /// <param name="title">The title</param>
        /// <param name="aggregator">The statistic aggregator</param>
        public StatisticDefinition
            (
                string name,
                string title,
                IQueryAggregator aggregator
            )
            : base(name, title)
        {
            Validate.IsNotNull(aggregator);
            
            this.Aggregator = aggregator;
        }

        /// <summary>
        /// Gets the statistic aggregator
        /// </summary>
        public IQueryAggregator Aggregator { get; protected set; }
        
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
    }
}
