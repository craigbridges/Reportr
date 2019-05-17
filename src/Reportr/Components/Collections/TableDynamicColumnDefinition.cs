namespace Reportr.Components.Collections
{
    using Reportr.Data;
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
        /// <param name="columnGroup">The dynamic column group</param>
        /// <param name="headerBinding">The column header binding</param>
        /// <param name="valueBinding">The row value binding</param>
        internal TableDynamicColumnDefinition
            (
                string name,
                TableDynamicColumnGroup columnGroup,
                DataBinding headerBinding,
                DataBinding valueBinding
            )
            : base(name, valueBinding)
        {
            Validate.IsNotNull(columnGroup);
            Validate.IsNotNull(headerBinding);

            this.ColumnGroup = columnGroup;
            this.HeaderBinding = headerBinding;
        }

        /// <summary>
        /// Gets the dynamic column group the definition belongs to
        /// </summary>
        public TableDynamicColumnGroup ColumnGroup { get; private set; }

        /// <summary>
        /// Gets the column header name data binding
        /// </summary>
        public DataBinding HeaderBinding { get; private set; }

        /// <summary>
        /// Gets the key value found for the column row used to generate the column definition
        /// </summary>
        internal object ColumnRowKeyValue { get; private set; }

        /// <summary>
        /// Sets the column row key value
        /// </summary>
        /// <param name="value">The value to set</param>
        internal void SetRowKeyValue
            (
                object value
            )
        {
            this.ColumnRowKeyValue = value;
        }
    }
}
