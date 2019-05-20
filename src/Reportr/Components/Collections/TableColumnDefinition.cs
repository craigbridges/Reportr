namespace Reportr.Components.Collections
{
    using Reportr.Data;
    using Reportr.Data.Querying.Functions;
    using System;

    /// <summary>
    /// Represents a column definition for a report table
    /// </summary>
    public class TableColumnDefinition
    {
        /// <summary>
        /// Constructs the column definition with the configuration
        /// </summary>
        /// <param name="name">The column name</param>
        /// <param name="valueBinding">The row value binding</param>
        /// <param name="totalAggregator">The total aggregator (optional)</param>
        /// <param name="totalFormat">The total format (optional)</param>
        public TableColumnDefinition
            (
                string name,
                DataBinding valueBinding,
                IAggregateFunction totalAggregator = null,
                string totalFormat = null
            )
        {
            Validate.IsNotEmpty(name);
            Validate.IsNotNull(valueBinding);

            this.ColumnId = Guid.NewGuid();
            this.Name = name;
            this.Title = name;
            this.ValueBinding = valueBinding;

            if (totalAggregator != null)
            {
                DefineTotal(totalAggregator, totalFormat);
            }
        }

        /// <summary>
        /// Gets the column ID
        /// </summary>
        public Guid ColumnId { get; protected set; }

        /// <summary>
        /// Gets the column name
        /// </summary>
        public string Name { get; protected set; }

        /// <summary>
        /// Gets the columns row value binding
        /// </summary>
        public DataBinding ValueBinding { get; protected set; }

        /// <summary>
        /// Gets a flag indicating if the column is dynamic
        /// </summary>
        public virtual bool IsDynamic
        {
            get
            {
                return false;
            }
        }

        /// <summary>
        /// Defines a total function for the column
        /// </summary>
        /// <param name="totalAggregator">The total aggregate function</param>
        /// <param name="totalFormat">The total format (optional)</param>
        public virtual void DefineTotal
            (
                IAggregateFunction totalAggregator,
                string totalFormat = null
            )
        {
            Validate.IsNotNull(totalAggregator);

            this.TotalAggregator = totalAggregator;
            this.TotalFormat = totalFormat;
            this.HasTotal = true;
        }

        /// <summary>
        /// Removes the total function from the column
        /// </summary>
        public virtual void RemoveTotal()
        {
            this.TotalAggregator = null;
            this.TotalFormat = null;
            this.HasTotal = false;
        }

        /// <summary>
        /// Gets the total aggregator function
        /// </summary>
        public IAggregateFunction TotalAggregator { get; protected set; }

        /// <summary>
        /// Gets the total display format
        /// </summary>
        public string TotalFormat { get; protected set; }

        /// <summary>
        /// Gets a flag indicating if the column has a total aggregator
        /// </summary>
        public bool HasTotal { get; protected set; }

        /// <summary>
        /// Gets the value formatting type override
        /// </summary>
        public DataValueFormattingType? FormattingTypeOverride
        {
            get;
            protected internal set;
        }

        /// <summary>
        /// Defines the style details for the column
        /// </summary>
        /// <param name="title">The title</param>
        /// <param name="alignment">The text alignment</param>
        /// <param name="importance">The importance</param>
        /// <param name="noWrap">True, if the cell text shouldn't word wrap</param>
        public virtual void DefineStyle
            (
                string title,
                ColumnAlignment alignment,
                DataImportance importance = default,
                bool noWrap = false
            )
        {
            this.Title = title;
            this.Alignment = alignment;
            this.Importance = importance;
            this.NoWrap = noWrap;
            this.HasStyling = true;
        }

        /// <summary>
        /// Gets the column title
        /// </summary>
        public string Title { get; protected set; }

        /// <summary>
        /// Gets the column alignment
        /// </summary>
        public ColumnAlignment Alignment { get; protected set; }

        /// <summary>
        /// Gets the importance of the column
        /// </summary>
        public DataImportance Importance { get; private set; }

        /// <summary>
        /// Gets a flag indicating if the cell text should be word wrapped
        /// </summary>
        public bool NoWrap { get; private set; }

        /// <summary>
        /// Gets a flag indicating if the column has styling
        /// </summary>
        public bool HasStyling { get; protected set; }

        /// <summary>
        /// Defines the cell action for the column
        /// </summary>
        /// <param name="action">The report action</param>
        public virtual void DefineAction
            (
                ReportActionDefinition action
            )
        {
            Validate.IsNotNull(action);

            this.CellAction = action;
            this.HasCellAction = true;
        }

        /// <summary>
        /// Gets the action associated with each cell in the column
        /// </summary>
        /// <remarks>
        /// The cell action can be used to make an entire table cell
        /// click-able instead of just text.
        /// 
        /// The cell action can be null and only works when the row
        /// action has not been set.
        /// </remarks>
        public ReportActionDefinition CellAction { get; protected set; }

        /// <summary>
        /// Gets a flag indicating if the column has a cell action
        /// </summary>
        public bool HasCellAction { get; protected set; }

        /// <summary>
        /// Provides a custom string description
        /// </summary>
        /// <returns>The column name</returns>
        public override string ToString()
        {
            return this.Name;
        }
    }
}
