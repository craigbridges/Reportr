namespace Reportr.Components.Metrics
{
    using Reportr.Data;
    using Reportr.Data.Querying;
    using System;

    /// <summary>
    /// Represents the definition of a single chart axis
    /// </summary>
    public class ChartAxisDefinition
    {
        /// <summary>
        /// Gets the label for the axis
        /// </summary>
        public ChartAxisLabel Label { get; protected set; }


        // TODO: need a specific query type that can take filters but only returns a single number


        /// <summary>
        /// Gets the query associated with the axis
        /// </summary>
        public IQuery Query { get; protected set; }

        /// <summary>
        /// Gets the binding for the axis
        /// </summary>
        /// <remarks>
        /// The binding maps the query results to the axis.
        /// </remarks>
        public DataBinding Binding { get; protected set; }
    }
}
