namespace Reportr.Components.Metrics
{
    using Reportr.Data.Querying;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;

    /// <summary>
    /// Represents the definition of a single report statistic
    /// </summary>
    public class StatisticDefinition : ReportComponentDefinitionBase
    {
        /// <summary>
        /// Constructs the statistic definition with the details
        /// </summary>
        /// <param name="name">The name</param>
        /// <param name="title">The title</param>
        /// <param name="aggregator">The statistic aggregator</param>
        /// <param name="action">The action (optional)</param>
        public StatisticDefinition
            (
                string name,
                string title,
                IQueryAggregator aggregator,
                ReportAction action = null
            )
            : base(name, title)
        {
            Validate.IsNotNull(aggregator);
            
            this.Aggregator = aggregator;
            this.DefaultParameterValues = new Collection<ParameterValue>();
            this.Action = action;

            var defaultValues = aggregator.Query.CompileDefaultParameters();

            foreach (var value in defaultValues)
            {
                this.DefaultParameterValues.Add(value);
            }
        }

        /// <summary>
        /// Gets the statistic aggregator
        /// </summary>
        public IQueryAggregator Aggregator { get; protected set; }
        
        /// <summary>
        /// Gets the default parameter values for the aggregator
        /// </summary>
        public ICollection<ParameterValue> DefaultParameterValues
        {
            get;
            protected set;
        }

        /// <summary>
        /// Gets a collection of all queries being used by the component
        /// </summary>
        /// <returns>A collection of queries</returns>
        public override IEnumerable<IQuery> GetQueriesUsed()
        {
            return new IQuery[]
            {
                this.Aggregator.Query
            };
        }

        /// <summary>
        /// Gets the statistic action
        /// </summary>
        public ReportAction Action { get; protected set; }

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
