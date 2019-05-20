namespace Reportr.Components.Collections
{
    using Nito.AsyncEx.Synchronous;
    using Reportr.Data;
    using Reportr.Data.Querying;
    using Reportr.Filtering;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    /// <summary>
    /// Represents the default report table generator implementation
    /// </summary>
    public sealed class TableGenerator : ReportComponentGeneratorBase
    {
        private readonly Dictionary<Guid, IEnumerable<TableColumnDefinition>> _dynamicColumnCache;
        private readonly Dictionary<string, List<object>> _columnValueCache;

        /// <summary>
        /// Constructs the table generator with a column cache
        /// </summary>
        public TableGenerator()
        {
            _dynamicColumnCache = new Dictionary<Guid, IEnumerable<TableColumnDefinition>>();
            _columnValueCache = new Dictionary<string, List<object>>();
        }

        /// <summary>
        /// Asynchronously generates a component from a report definition and filter
        /// </summary>
        /// <param name="definition">The component definition</param>
        /// <param name="sectionType">The report section type</param>
        /// <param name="filter">The report filter</param>
        /// <returns>The report component generated</returns>
        public override async Task<IReportComponent> GenerateAsync
            (
                IReportComponentDefinition definition,
                ReportSectionType sectionType,
                ReportFilter filter
            )
        {
            Validate.IsNotNull(definition);
            Validate.IsNotNull(filter);

            var table = default(Table);
            var tableDefinition = definition.As<TableDefinition>();
            var query = tableDefinition.Query;
            var defaultParameters = tableDefinition.DefaultParameterValues;

            var queryTask = query.ExecuteAsync
            (
                filter,
                defaultParameters.ToArray()
            );

            var results = await queryTask.ConfigureAwait
            (
                false
            );

            var columnsTask = GetColumnsDynamicallyAsync
            (
                tableDefinition,
                filter
            );

            var columnDefinitions = await columnsTask.ConfigureAwait
            (
                false
            );

            if (results.HasMultipleGroupings)
            {
                var tableGroups = new List<TableGrouping>();

                foreach (var queryGroup in results.Groupings)
                {
                    var groupRows = BuildTableRows
                    (
                        tableDefinition,
                        sectionType,
                        filter,
                        queryGroup.Rows
                    );
                    
                    var tableGroup = new TableGrouping
                    (
                        queryGroup.GroupingValues,
                        groupRows.ToArray()
                    );

                    if (groupRows.Any() && tableDefinition.HasTotals)
                    {
                        var totals = GenerateTotals
                        (
                            tableDefinition,
                            filter,
                            queryGroup.Rows
                        );

                        tableGroup.SetTotals(totals);
                    }

                    tableGroups.Add(tableGroup);
                }

                table = new Table
                (
                    tableDefinition,
                    columnDefinitions,
                    tableGroups
                );
            }
            else
            {
                var tableRows = BuildTableRows
                (
                    tableDefinition,
                    sectionType,
                    filter,
                    results.AllRows
                );

                table = new Table
                (
                    tableDefinition,
                    columnDefinitions,
                    tableRows
                );
            }

            if (table.AllRows.Any() && tableDefinition.HasTotals)
            {
                var totals = GenerateTotals
                (
                    tableDefinition,
                    filter,
                    results.AllRows
                );

                table.SetTotals(totals);
            }

            _columnValueCache.Clear();

            return table;
        }

        /// <summary>
        /// Gets columns dynamically for a table definition
        /// </summary>
        /// <param name="definition">The table definition</param>
        /// <param name="filter">The report filter</param>
        /// <returns>A collection of column definitions</returns>
        private IEnumerable<TableColumnDefinition> GetColumnsDynamically
            (
                TableDefinition definition,
                ReportFilter filter
            )
        {
            var task = GetColumnsDynamicallyAsync
            (
                definition,
                filter
            );

            return task.WaitAndUnwrapException();
        }

        /// <summary>
        /// Asynchronously gets columns dynamically for a table definition
        /// </summary>
        /// <param name="definition">The table definition</param>
        /// <param name="filter">The report filter</param>
        /// <returns>A collection of column definitions</returns>
        private async Task<IEnumerable<TableColumnDefinition>> GetColumnsDynamicallyAsync
            (
                TableDefinition definition,
                ReportFilter filter
            )
        {
            var tableId = definition.ComponentId;

            if (_dynamicColumnCache.ContainsKey(tableId))
            {
                return _dynamicColumnCache[tableId];
            }
            else
            {
                var buildTask = definition.BuildColumnsDynamicallyAsync
                (
                    filter
                );

                var columns = await buildTask.ConfigureAwait
                (
                    false
                );

                _dynamicColumnCache.Add(tableId, columns);

                return columns;
            }
        }

        /// <summary>
        /// Builds a collection of table rows from a collection of query rows
        /// </summary>
        /// <param name="tableDefinition">The table definition</param>
        /// <param name="sectionType">The report section type</param>
        /// <param name="filter">The report filter</param>
        /// <param name="queryRows">The query rows</param>
        /// <returns>A collection of table rows</returns>
        private IEnumerable<TableRow> BuildTableRows
            (
                TableDefinition tableDefinition,
                ReportSectionType sectionType,
                ReportFilter filter,
                IEnumerable<QueryRow> queryRows
            )
        {
            var tableRows = new List<TableRow>();
            var actionDefinition = tableDefinition.RowAction;

            var columnDefinitions = GetColumnsDynamically
            (
                tableDefinition,
                filter
            );

            foreach (var queryRow in queryRows)
            {
                var tableCells = new List<TableCell>();

                foreach (var columnDefinition in columnDefinitions)
                {
                    var value = ResolveColumnValue
                    (
                        columnDefinition,
                        queryRow
                    );

                    var action = default(ReportAction);

                    if (columnDefinition.CellAction != null)
                    {
                        action = columnDefinition.CellAction.Resolve
                        (
                            queryRow
                        );
                    }

                    var cell = CreateTableCell
                    (
                        columnDefinition,
                        value,
                        action
                    );

                    tableCells.Add(cell);
                }

                var importance = DataImportance.Default;

                if (tableDefinition.RowImportanceRules.Any())
                {
                    foreach (var rule in tableDefinition.RowImportanceRules)
                    {
                        var columnName = rule.ColumnName;
                        var candidateValue = queryRow[columnName].Value;

                        if (rule.Matches(candidateValue))
                        {
                            importance = rule.ImportanceOnMatch;
                        }
                    }
                }

                var tableRow = new TableRow
                (
                    importance,
                    tableCells.ToArray()
                );

                if (tableDefinition.RowAction != null)
                {
                    var action = actionDefinition.Resolve
                    (
                        queryRow
                    );

                    tableRow = tableRow.WithAction
                    (
                        action
                    );
                }

                tableRows.Add(tableRow);
            }

            if (false == tableDefinition.DisableSorting)
            {
                var sortingRules = filter.GetSortingRules
                (
                    sectionType,
                    tableDefinition.Name
                );

                tableRows = SortRows(tableRows, sortingRules).ToList();
            }

            return tableRows;
        }

        /// <summary>
        /// Resolves a column value for a specific query row
        /// </summary>
        /// <param name="column">The column definition</param>
        /// <param name="row">The query row</param>
        /// <returns>The value that was resolved</returns>
        private object ResolveColumnValue
            (
                TableColumnDefinition column,
                QueryRow row
            )
        {
            var value = default(object);

            if (false == column.IsDynamic)
            {
                value = column.ValueBinding.Resolve
                (
                    row
                );
            }
            else
            {
                var dynamicColumn = (TableDynamicColumnDefinition)column;
                var group = dynamicColumn.ColumnGroup;
                var columnRowKeyValue = dynamicColumn.ColumnRowKeyValue;
                var valueResults = group.ValueResults;

                var masterKeyValue = row.FindCellValue
                (
                    group.MasterToValueQueryKeyMap.FromColumnName
                );

                // NOTE:
                // We find the value rows matching the master row and then 
                // filter these to the exact row by matching the column key.
                //
                // We are trying narrow the column value results to match 
                // the keys from both the master and column query results.

                var matchingRows = valueResults.FindRows
                (
                    group.MasterToValueQueryKeyMap.ToColumnName,
                    masterKeyValue
                );

                var valueColumnIndex = valueResults.GetColumnIndex
                (
                    group.ColumnToValueQueryKeyMap.ToColumnName
                );

                var matchingValueRow = matchingRows.FirstOrDefault
                (
                    r => r.Cells[valueColumnIndex].Value == columnRowKeyValue
                );

                if (matchingValueRow == null)
                {
                    value = null;
                }
                else
                {
                    value = column.ValueBinding.Resolve
                    (
                        matchingValueRow
                    );
                }
            }

            var columnName = column.Name;

            if (false == _columnValueCache.ContainsKey(columnName))
            {
                _columnValueCache.Add
                (
                    columnName,
                    new List<object>()
                );
            }

            _columnValueCache[columnName].Add(value);

            return value;
        }

        /// <summary>
        /// Sorts a collection of table rows by the sorting rules specified
        /// </summary>
        /// <param name="rows">The table rows</param>
        /// <param name="sortingRules">The sorting rules</param>
        /// <returns>The sorted rows</returns>
        private IEnumerable<TableRow> SortRows
            (
                IEnumerable<TableRow> rows,
                IEnumerable<ReportFilterSortingRule> sortingRules
            )
        {
            Validate.IsNotNull(rows);
            Validate.IsNotNull(sortingRules);

            if (false == sortingRules.Any())
            {
                return rows;
            }
            else
            {
                var ruleNumber = 1;
                var sortedRows = (IOrderedEnumerable<TableRow>)rows;

                foreach (var rule in sortingRules)
                {
                    object keySelector(TableRow row) => row.First
                    (
                        cell => cell.Column.Name.ToLower() == rule.ColumnName.ToLower()
                    )
                    .Value;

                    if (rule.Direction == SortDirection.Ascending)
                    {
                        if (ruleNumber == 1)
                        {
                            sortedRows = sortedRows.OrderBy
                            (
                                keySelector
                            );
                        }
                        else
                        {
                            sortedRows = sortedRows.ThenBy
                            (
                                keySelector
                            );
                        }
                    }
                    else
                    {
                        if (ruleNumber == 1)
                        {
                            sortedRows = sortedRows.OrderByDescending
                            (
                                keySelector
                            );
                        }
                        else
                        {
                            sortedRows = sortedRows.ThenByDescending
                            (
                                keySelector
                            );
                        }
                    }

                    ruleNumber++;
                }

                return rows;
            }
        }

        /// <summary>
        /// Generates the totals for a collection of query rows
        /// </summary>
        /// <param name="tableDefinition">The table definition</param>
        /// <param name="filter">The report filter</param>
        /// <param name="rows">The query rows</param>
        /// <returns>The totals as table cells</returns>
        private IEnumerable<TableCell> GenerateTotals
            (
                TableDefinition tableDefinition,
                ReportFilter filter,
                IEnumerable<QueryRow> rows
            )
        {
            var cells = new List<TableCell>();

            var columnDefinitions = GetColumnsDynamically
            (
                tableDefinition,
                filter
            );

            foreach (var columnDefinition in columnDefinitions)
            {
                var cell = default(TableCell);

                if (columnDefinition.HasTotal)
                {
                    var total = default(double);

                    if (columnDefinition.IsDynamic)
                    {
                        var cellValues = _columnValueCache[columnDefinition.Name];
                        var numbers = new List<double>();

                        // Convert the cell values into their double equivalent
                        foreach (var value in cellValues)
                        {
                            if (value == null)
                            {
                                numbers.Add(0);
                            }
                            else
                            {
                                Double.TryParse
                                (
                                    value.ToString(),
                                    out double number
                                );

                                numbers.Add(number);
                            }
                        }

                        total = columnDefinition.TotalAggregator.Execute
                        (
                            numbers.ToArray()
                        );
                    }
                    else
                    {
                        total = columnDefinition.TotalAggregator.Execute
                        (
                            rows.ToArray()
                        );
                    }

                    var format = columnDefinition.TotalFormat;

                    if (format == null)
                    {
                        cell = CreateTableCell
                        (
                            columnDefinition,
                            total
                        );
                    }
                    else
                    {
                        var formattedTotal = String.Format
                        (
                            format,
                            total
                        );

                        cell = CreateTableCell
                        (
                            columnDefinition,
                            formattedTotal
                        );
                    }
                }
                else
                {
                    cell = CreateTableCell
                    (
                        columnDefinition,
                        null
                    );
                }

                cells.Add(cell);
            }

            return cells;
        }

        /// <summary>
        /// Create a new table cell from a column definition and value
        /// </summary>
        /// <param name="columnDefinition">The column definition</param>
        /// <param name="value">The cell value</param>
        /// <param name="action">The cell action (optional)</param>
        /// <returns>The table cell created</returns>
        private TableCell CreateTableCell
            (
                TableColumnDefinition columnDefinition,
                object value,
                ReportAction action = null
            )
        {
            var column = new TableColumn
            (
                columnDefinition.Name,
                columnDefinition.Title,
                columnDefinition.Alignment,
                columnDefinition.Importance,
                columnDefinition.NoWrap
            );
            
            var cell = new TableCell
            (
                column,
                value,
                action
            );

            var formattingOverride = columnDefinition.FormattingTypeOverride;

            if (formattingOverride.HasValue)
            {
                cell.FormattingType = formattingOverride.Value;
            }

            return cell;
        }
    }
}
