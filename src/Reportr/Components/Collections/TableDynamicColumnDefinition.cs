namespace Reportr.Components.Collections
{
    using Reportr.Data;
    using Reportr.Data.Querying.Functions;
    using System;
    
    /// <summary>
    /// Represents a dynamic column definition for a report table
    /// </summary>
    public class TableDynamicColumnDefinition : TableColumnDefinition
    {
        /// <summary>
        /// Constructs the column definition with the configuration
        /// </summary>
        /// <param name="name">The column name</param>
        /// <param name="headerBinding">The column header binding</param>
        /// <param name="valueBinding">The row value binding</param>
        /// <param name="totalAggregator">The total aggregator (optional)</param>
        /// <param name="totalFormat">The total format (optional)</param>
        public TableDynamicColumnDefinition
            (
                string name,
                DataBinding headerBinding,
                DataBinding valueBinding,
                IAggregateFunction totalAggregator = null,
                string totalFormat = null
            )
            : base(name, valueBinding, totalAggregator, totalFormat)
        {
            Validate.IsNotNull(headerBinding);

            this.HeaderBinding = headerBinding;
        }

        /// <summary>
        /// Gets the column header name data binding
        /// </summary>
        public DataBinding HeaderBinding { get; private set; }
    }
}
