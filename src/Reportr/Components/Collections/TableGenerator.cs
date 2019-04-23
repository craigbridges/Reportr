namespace Reportr.Components.Collections
{
    using Reportr.Data;
    using Reportr.Data.Querying;
    using Reportr.Filtering;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    /// <summary>
    /// Represents a report table generator
    /// </summary>
    public class TableGenerator : ReportComponentGeneratorBase
    {
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

            var tableDefinition = definition.As<TableDefinition>();
            var query = tableDefinition.Query;
            var defaultParameters = tableDefinition.DefaultParameterValues;

            var parameters = filter.GetQueryParameters
            (
                query,
                defaultParameters.ToArray()
            );

            var queryTask = query.ExecuteAsync
            (
                parameters.ToArray()
            );

            var results = await queryTask.ConfigureAwait
            (
                false
            );

            var table = default(Table);

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
                        var columns = table.Columns;

                        var totals = GenerateTotals
                        (
                            tableDefinition,
                            columns,
                            queryGroup.Rows
                        );

                        tableGroup.SetTotals(totals);
                    }

                    tableGroups.Add(tableGroup);
                }

                table = new Table
                (
                    tableDefinition,
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
                    tableRows
                );
            }

            if (table.AllRows.Any() && tableDefinition.HasTotals)
            {
                var columns = table.Columns;

                var totals = GenerateTotals
                (
                    tableDefinition,
                    columns,
                    results.AllRows
                );

                table.SetTotals(totals);
            }

            return table;
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

            foreach (var queryRow in queryRows)
            {
                var tableCells = new List<TableCell>();

                foreach (var columnDefinition in tableDefinition.Columns)
                {
                    var value = columnDefinition.ValueBinding.Resolve
                    (
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
        /// <param name="columns">The table columns</param>
        /// <param name="rows">The query rows</param>
        /// <returns>The totals as table cells</returns>
        private IEnumerable<TableCell> GenerateTotals
            (
                TableDefinition tableDefinition,
                IEnumerable<TableColumn> columns,
                IEnumerable<QueryRow> rows
            )
        {
            var cells = new List<TableCell>();

            foreach (var columnDefinition in tableDefinition.Columns)
            {
                var column = columns.FirstOrDefault
                (
                    c => c.Name == columnDefinition.Name
                );

                var cell = default(TableCell);

                if (columnDefinition.HasTotal)
                {
                    var total = columnDefinition.TotalAggregator.Execute
                    (
                        rows.ToArray()
                    );

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
