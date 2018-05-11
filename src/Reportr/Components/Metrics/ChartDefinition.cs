namespace Reportr.Components.Metrics
{
    using System.Collections.Generic;

    /// <summary>
    /// Represents the definition of a single chart
    /// </summary>
    public class ChartDefinition : ReportComponentDefinitionBase
    {
        /// <summary>
        /// Constructs the chart definition with the details
        /// </summary>
        /// <param name="name">The name</param>
        /// <param name="title">The title</param>
        /// <param name="allowDecimals">True, to allow decimal places</param>
        public ChartDefinition
            (
                string name,
                string title,
                bool allowDecimals = true
            )
            : base(name, title)
        {
            this.AllowDecimals = allowDecimals;
        }

        /// <summary>
        /// Gets a flag indicating if decimal places should be allowed
        /// </summary>
        public bool AllowDecimals { get; protected set; }

        /// <summary>
        /// Gets a collection of chart data set definitions
        /// </summary>
        public ICollection<ChartDataSetDefinition> DataSets
        {
            get;
            protected set;
        }
        
        /// <summary>
        /// Gets the component type
        /// </summary>
        public override ReportComponentType ComponentType
        {
            get
            {
                return ReportComponentType.Chart;
            }
        }
    }
}
