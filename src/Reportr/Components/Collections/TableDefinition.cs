namespace Reportr.Components.Collections
{
    using Newtonsoft.Json;
    using Reportr.Data;
    using Reportr.Data.Querying;
    using Reportr.Data.Querying.Functions;
    using Reportr.Filtering;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;

    /// <summary>
    /// Represents a single report table definition
    /// </summary>
    public class TableDefinition : ReportComponentDefinitionBase, ISortableComponent
    {
        /// <summary>
        /// Constructs the table definition with the details
        /// </summary>
        /// <param name="name">The name</param>
        /// <param name="title">The title</param>
        /// <param name="query">The query</param>
        /// <param name="rowAction">The row action (optional)</param>
        public TableDefinition
            (
                string name,
                string title,
                IQuery query,
                ReportActionDefinition rowAction = null
            )
            : base(name, title)
        {
            Validate.IsNotNull(query);

            this.Query = query;
            this.DefaultParameterValues = new Collection<ParameterValue>();
            this.StaticColumns = new Collection<TableColumnDefinition>();
            this.DynamicColumnGroups = new Collection<TableDynamicColumnGroup>();
            this.RowImportanceRules = new Collection<TableRowImportanceRule>();
            this.RowAction = rowAction;

            var defaultValues = query.CompileDefaultParameters();

            foreach (var value in defaultValues)
            {
                this.DefaultParameterValues.Add(value);
            }
        }

        /// <summary>
        /// Gets the query that will supply the tables data
        /// </summary>
        [JsonIgnore]
        public IQuery Query { get; protected set; }

        /// <summary>
        /// Gets the default parameter values for the query
        /// </summary>
        [JsonIgnore]
        public ICollection<ParameterValue> DefaultParameterValues
        {
            get;
            protected set;
        }

        /// <summary>
        /// Gets the static columns defined by the table
        /// </summary>
        [JsonIgnore]
        public ICollection<TableColumnDefinition> StaticColumns
        {
            get;
            protected set;
        }

        /// <summary>
        /// Gets a single column definition from the table definition
        /// </summary>
        /// <param name="name">The column name</param>
        /// <returns>The matching column definition</returns>
        internal TableColumnDefinition GetColumn
            (
                string name
            )
        {
            Validate.IsNotNull(name);

            var column = this.StaticColumns.FirstOrDefault
            (
                definition => definition.Name.Replace
                (
                    " ",
                    String.Empty
                )
                .Equals
                (
                    name,
                    StringComparison.OrdinalIgnoreCase
                )
            );

            if (column == null)
            {
                throw new InvalidOperationException
                (
                    $"The name '{name}' did not match any columns."
                );
            }

            return column;
        }

        /// <summary>
        /// Applies style configuration to a table column definition
        /// </summary>
        /// <param name="columnName">The column name</param>
        /// <param name="title">The title</param>
        /// <param name="alignment">The text alignment</param>
        /// <param name="importance">The importance</param>
        /// <param name="noWrap">True, if the cell text shouldn't word wrap</param>
        public void StyleColumn
            (
                string columnName,
                string title,
                ColumnAlignment alignment,
                DataImportance importance = default,
                bool noWrap = false
            )
        {
            var column = GetColumn(columnName);

            column.DefineStyle
            (
                title,
                alignment,
                importance,
                noWrap
            );
        }

        /// <summary>
        /// Sets a formatting type override against one or more columns
        /// </summary>
        /// <param name="formattingType">The value formatting type</param>
        /// <param name="columnNames">The column names</param>
        public void SetFormattingTypeOverrides
            (
                DataValueFormattingType formattingType,
                params string[] columnNames
            )
        {
            Validate.IsNotNull(columnNames);

            foreach (var name in columnNames)
            {
                var column = GetColumn(name);

                column.FormattingTypeOverride = formattingType;
            }
        }

        /// <summary>
        /// Defines total aggregator functions for the columns specified
        /// </summary>
        /// <param name="totalAggregator">The total aggregator function</param>
        /// <param name="columnNames">The column names</param>
        public void DefineTotals
            (
                IAggregateFunction totalAggregator,
                params string[] columnNames
            )
        {
            DefineTotals
            (
                totalAggregator,
                null,
                columnNames
            );
        }

        /// <summary>
        /// Defines total aggregator functions for the columns specified
        /// </summary>
        /// <param name="totalAggregator">The total aggregator function</param>
        /// <param name="totalFormat">The total format pattern</param>
        /// <param name="columnNames">The column names</param>
        public void DefineTotals
            (
                IAggregateFunction totalAggregator,
                string totalFormat,
                params string[] columnNames
            )
        {
            Validate.IsNotNull(totalAggregator);
            Validate.IsNotNull(columnNames);

            foreach (var name in columnNames)
            {
                var column = GetColumn(name);

                column.DefineTotal
                (
                    totalAggregator,
                    totalFormat
                );
            }
        }

        /// <summary>
        /// Removes total aggregator functions from the columns specified
        /// </summary>
        /// <param name="columnNames">The column names</param>
        public void RemoveTotals
            (
                params string[] columnNames
            )
        {
            Validate.IsNotNull(columnNames);

            foreach (var name in columnNames)
            {
                var column = GetColumn(name);

                column.RemoveTotal();
            }
        }

        /// <summary>
        /// Removes totals defined against all columns
        /// </summary>
        public void RemoveAllTotals()
        {
            foreach (var column in this.StaticColumns.ToList())
            {
                column.RemoveTotal();
            }
        }

        /// <summary>
        /// Gets a flag indicating if the table definition has any totals
        /// </summary>
        public bool HasTotals
        {
            get
            {
                var staticTotals = this.StaticColumns.Any
                (
                    column => column.HasTotal
                );

                if (staticTotals)
                {
                    return true;
                }
                else if (this.DynamicColumnGroups.Any())
                {
                    var dynamicColumns = this.DynamicColumnGroups.SelectMany
                    (
                        g => g.Columns
                    );

                    var dynamicTotals = dynamicColumns.Any
                    (
                        c => c.HasTotal
                    );

                    return dynamicTotals;
                }
                else
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// Gets the dynamic column groups defined by the table
        /// </summary>
        [JsonIgnore]
        public ICollection<TableDynamicColumnGroup> DynamicColumnGroups
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
                this.Query
            };
        }

        /// <summary>
        /// Gets the action associated with each table row
        /// </summary>
        /// <remarks>
        /// The row action can be used to make an entire table row
        /// a click-able action, instead of just a single table cell.
        /// 
        /// The row action can be null, but when set, it overrides
        /// any column actions that have been set.
        /// </remarks>
        [JsonIgnore]
        public ReportActionDefinition RowAction { get; protected set; }

        /// <summary>
        /// Gets a collection of row importance rules for the defined table
        /// </summary>
        [JsonIgnore]
        public ICollection<TableRowImportanceRule> RowImportanceRules
        {
            get;
            protected set;
        }

        /// <summary>
        /// Adds a row importance rule to the table definition
        /// </summary>
        /// <param name="columnName">The column name</param>
        /// <param name="matchValue">The data match value</param>
        /// <param name="compareOperator">The match compare operator</param>
        /// <param name="importanceOnMatch">The importance flag for matches</param>
        public void AddRowImportanceRule
            (
                string columnName,
                object matchValue,
                DataCompareOperator compareOperator,
                DataImportance importanceOnMatch
            )
        {
            var ruleFound = this.RowImportanceRules.Any
            (
                r => r.ColumnName.Equals(columnName, StringComparison.OrdinalIgnoreCase)
                    && r.MatchValue == matchValue
                    && r.CompareOperator == compareOperator
            );

            if (ruleFound)
            {
                throw new ArgumentException
                (
                    "A rule with matching column, value and comparer has been defined."
                );
            }

            var rule = new TableRowImportanceRule
            (
                columnName,
                matchValue,
                compareOperator,
                importanceOnMatch
            );

            this.RowImportanceRules.Add(rule);
        }

        /// <summary>
        /// Gets or sets a flag indicating if column sorting is disabled
        /// </summary>
        public bool DisableSorting { get; set; }

        /// <summary>
        /// Gets an array of sortable columns
        /// </summary>
        public string[] SortableColumns
        {
            get
            {
                return this.StaticColumns.Select(c => c.Name).ToArray();
            }
        }

        /// <summary>
        /// Gets the report component type
        /// </summary>
        public override ReportComponentType ComponentType
        {
            get
            {
                return ReportComponentType.Table;
            }
        }
    }
}
