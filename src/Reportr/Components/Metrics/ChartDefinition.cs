namespace Reportr.Components.Metrics
{
    using Reportr.Data.Querying;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;

    /// <summary>
    /// Represents the definition of a single chart
    /// </summary>
    public class ChartDefinition : ReportComponentDefinitionBase
    {
        private ReportComponentType _chartType;

        /// <summary>
        /// Constructs the chart definition with the details
        /// </summary>
        /// <param name="name">The name</param>
        /// <param name="title">The title</param>
        /// <param name="type">The chart type</param>
        public ChartDefinition
            (
                string name,
                string title,
                ReportComponentType type
            )
            : base(name, title)
        {
            if (type != ReportComponentType.Chart)
            {
                throw new ArgumentException
                (
                    "The chart component type is invalid."
                );
            }

            this.DataSets = new Collection<ChartDataSetDefinition>();

            _chartType = type;
        }
        
        /// <summary>
        /// Gets a collection of chart data set definitions
        /// </summary>
        public ICollection<ChartDataSetDefinition> DataSets
        {
            get;
            protected set;
        }

        /// <summary>
        /// Gets an array of x-axis label template
        /// </summary>
        public ChartAxisLabel XAxisLabelTemplate { get; protected set; }

        /// <summary>
        /// Gets the step interval for the y-axis labels
        /// </summary>
        public double? YAxisInterval { get; protected set; }

        /// <summary>
        /// Adds the label templates to the chart definition
        /// </summary>
        /// <param name="xAxisLabelTemplate">The x-axis label template</param>
        /// <param name="yAxisInterval">The y-axis interval</param>
        /// <returns>The updated chart definition</returns>
        public ChartDefinition WithLabelTemplates
            (
                ChartAxisLabel xAxisLabelTemplate,
                double? yAxisInterval = null
            )
        {
            Validate.IsNotNull(xAxisLabelTemplate);

            if (yAxisInterval < 1)
            {
                throw new ArgumentException
                (
                    "The y-axis interval must be greater than zero."
                );
            }

            this.XAxisLabelTemplate = xAxisLabelTemplate;
            this.YAxisInterval = yAxisInterval;

            return this;
        }

        /// <summary>
        /// Gets a collection of all queries being used by the component
        /// </summary>
        /// <returns>A collection of queries</returns>
        public override IEnumerable<IQuery> GetQueriesUsed()
        {
            foreach (var set in this.DataSets)
            {
                yield return set.Query;
            }
        }

        /// <summary>
        /// Gets the component type
        /// </summary>
        public override ReportComponentType ComponentType
        {
            get
            {
                return _chartType;
            }
        }
    }
}
