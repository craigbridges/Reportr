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

                var columns = new List<TableColumnDefinition>
                (
                    definition.StaticColumns
                );

                var defaultParameters = definition.DefaultParameterValues.ToArray();

                foreach (var group in dynamicGroups)
                {
                    var queryTask = group.ColumnQuery.ExecuteAsync
                    (
                        filter,
                        defaultParameters
                    );

                    var results = await queryTask.ConfigureAwait
                    (
                        false
                    );

                    var columnCluster = new List<TableColumnDefinition>();

                    foreach (var row in results.AllRows)
                    {
                        foreach (var columnTemplate in group.Columns.ToList())
                        {
                            var headerBinding = columnTemplate.HeaderBinding;

                            var title = headerBinding.Resolve<string>
                            (
                                row
                            );

                            var name = title.RemoveSpecialCharacters();

                            var dynamicColumn = new TableDynamicColumnDefinition
                            (
                                name,
                                headerBinding,
                                columnTemplate.ValueBinding,
                                columnTemplate.TotalAggregator,
                                columnTemplate.TotalFormat
                            );

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
                        columns.InsertRange
                        (
                            insertIndex,
                            columnCluster
                        );
                    }

                    columnCount = columns.Count;
                }

                return columns;
            }
        }
    }
}
