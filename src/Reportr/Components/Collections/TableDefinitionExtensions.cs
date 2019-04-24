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
                var columns = new List<TableColumnDefinition>
                (
                    definition.StaticColumns
                );


                // TODO: build dynamic column groups
                // ensure cluster of columns is inserted into correct position (validate index first)

                foreach (var group in dynamicGroups)
                {

                }

                return columns;
            }
        }
    }
}
