namespace Reportr.Components.Collections
{
    using Reportr.Filtering;
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
            var tableDefinition = definition.As<TableDefinition>();
            var query = tableDefinition.Query;
            
            
            // TODO: take into account the default parameter values


            var parameters = filter.GetParameters
            (
                sectionType,
                query
            );

            var results = await query.ExecuteAsync
            (
                parameters.ToArray()
            );

            var tableRows = new List<TableRow>();
            var actionDefinition = tableDefinition.RowAction;

            foreach (var queryRow in results.AllRows)
            {
                var tableCells = new List<TableCell>();

                foreach (var columnDefinition in tableDefinition.Columns)
                {
                    var value = columnDefinition.Binding.Resolve
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

                    var column = new TableColumn
                    (
                        columnDefinition.Name,
                        columnDefinition.Title,
                        columnDefinition.Alignment,
                        columnDefinition.Importance
                    );

                    var cell = new TableCell
                    (
                        column,
                        value,
                        action
                    );

                    tableCells.Add(cell);
                }
                
                var tableRow = new TableRow
                (
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

            var repeater = new Table
            (
                tableDefinition,
                tableRows.ToArray()
            );

            return repeater;
        }
    }
}
