namespace Reportr.Components.Collections
{
    using Nito.AsyncEx.Synchronous;
    using Reportr.Filtering;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    /// <summary>
    /// Represents various extension methods for table definitions
    /// </summary>
    public static class TableDefinitionExtensions
    {
        /// <summary>
        /// Builds columns dynamically for a table definition
        /// </summary>
        /// <param name="definition">The table definition</param>
        /// <param name="filter">The report filter</param>
        /// <returns>A collection of column definitions</returns>
        public static IEnumerable<TableColumnDefinition> BuildColumnsDynamically
            (
                this TableDefinition definition,
                ReportFilter filter
            )
        {
            var task = BuildColumnsDynamicallyAsync
            (
                definition,
                filter
            );

            return task.WaitAndUnwrapException();
        }

        /// <summary>
        /// Asynchronously builds columns dynamically for a table definition
        /// </summary>
        /// <param name="definition">The table definition</param>
        /// <param name="filter">The report filter</param>
        /// <returns>A collection of column definitions</returns>
        public static async Task<IEnumerable<TableColumnDefinition>> BuildColumnsDynamicallyAsync
            (
                this TableDefinition definition,
                ReportFilter filter
            )
        {
            Validate.IsNotNull(definition);

            var dynamicGroups = definition.DynamicColumnGroups.ToList();

            if (dynamicGroups.Count == 0)
            {
                return definition.StaticColumns;
            }
            else
            {
                var columnCount = definition.StaticColumns.Count;

                var allColumns = new List<TableColumnDefinition>
                (
                    definition.StaticColumns
                );

                var defaultParameters = definition.DefaultParameterValues.ToArray();

                foreach (var group in dynamicGroups)
                {
                    group.Validate();

                    var columnQueryTask = group.ColumnQuery.ExecuteAsync
                    (
                        filter,
                        defaultParameters
                    );

                    var valueQueryTask = group.ValueQuery.ExecuteAsync
                    (
                        filter,
                        defaultParameters
                    );

                    var columnResults = await columnQueryTask.ConfigureAwait
                    (
                        false
                    );

                    var valueResults = await valueQueryTask.ConfigureAwait
                    (
                        false
                    );

                    group.AddValueResults(valueResults);

                    var columnCluster = new List<TableColumnDefinition>();

                    foreach (var row in columnResults.AllRows)
                    {
                        foreach (var columnTemplate in group.Columns.ToList())
                        {
                            var headerBinding = columnTemplate.HeaderBinding;
                            var valueBinding = columnTemplate.ValueBinding;

                            var title = headerBinding.Resolve<string>
                            (
                                row
                            );

                            var name = title.RemoveSpecialCharacters();

                            var nameUsed =
                            (
                                columnCluster.Any
                                (
                                    d => d.Name.ToLower() == name.ToLower()
                                )
                                ||
                                allColumns.Any
                                (
                                    d => d.Name.ToLower() == name.ToLower()
                                )
                            );

                            // Don't allow the same name to be used more than once
                            if (nameUsed)
                            {
                                continue;
                            }

                            var dynamicColumn = new TableDynamicColumnDefinition
                            (
                                name,
                                group,
                                headerBinding,
                                valueBinding
                            );

                            if (columnTemplate.HasTotal)
                            {
                                dynamicColumn.DefineTotal
                                (
                                    columnTemplate.TotalAggregator,
                                    columnTemplate.TotalFormat
                                );
                            }

                            if (columnTemplate.HasStyling)
                            {
                                dynamicColumn.DefineStyle
                                (
                                    columnTemplate.Title,
                                    columnTemplate.Alignment,
                                    columnTemplate.Importance,
                                    columnTemplate.NoWrap
                                );
                            }

                            if (columnTemplate.HasCellAction)
                            {
                                dynamicColumn.DefineAction
                                (
                                    columnTemplate.CellAction
                                );
                            }

                            var rowKeyValue = row.FindCellValue
                            (
                                group.ColumnToValueQueryKeyMap.FromColumnName
                            );

                            dynamicColumn.SetRowKeyValue(rowKeyValue);
                            columnCluster.Add(dynamicColumn);
                        }
                    }

                    var insertIndex = group.InsertPosition ?? columnCount;

                    if (insertIndex < 0 || insertIndex > columnCount)
                    {
                        insertIndex = columnCount;
                    }

                    if (columnCluster.Count > 0)
                    {
                        allColumns.InsertRange
                        (
                            insertIndex,
                            columnCluster
                        );
                    }

                    columnCount = allColumns.Count;
                }

                return allColumns;
            }
        }
    }
}
