namespace Reportr.Components.Metrics
{
    using Reportr.Data.Querying;
    using System;

    /// <summary>
    /// Represents the definition of a single chart axis
    /// </summary>
    public class ChartAxisDefinition
    {
        public ChartAxisDefinition
            (
                ChartAxisLabel label,
                IQueryAggregator aggregator
            )
        {
            Validate.IsNotNull(label);
            Validate.IsNotNull(aggregator);

            this.Label = label;
            this.Aggregator = aggregator;
        }

        /// <summary>
        /// Gets the label for the axis
        /// </summary>
        public ChartAxisLabel Label { get; protected set; }
        
        /// <summary>
        /// Gets the query aggregator associated with the axis
        /// </summary>
        public IQueryAggregator Aggregator { get; protected set; }
    }
}
