namespace Reportr.Conversion.Csv
{
    using Reportr.Components;
    using Reportr.Components.Collections;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    
    /// <summary>
    /// Represents a report converter that converts to CSV format
    /// </summary>
    public class ReportCsvConverter : IReportConverter<CsvDocument>
    {
        /// <summary>
        /// Converts a report to a CSV document
        /// </summary>
        /// <param name="report">The report to convert</param>
        /// <returns>The CSV document</returns>
        public CsvDocument Convert
            (
                Report report
            )
        {
            Validate.IsNotNull(report);

            var tables = ExtractTables(report);
            var allColumns = new List<string>();
            var allRows = new List<CsvRow>();

            // Build a list of unique column names from the tables
            foreach (var table in tables)
            {
                foreach (var column in table.Columns)
                {
                    var columnName = column.Name;

                    var columnFound = allColumns.Contains
                    (
                        columnName,
                        StringComparer.CurrentCultureIgnoreCase
                    );

                    if (false == columnFound)
                    {
                        allColumns.Add(columnName);
                    }
                }
            }

            var document = new CsvDocument
            (
                allColumns.ToArray()
            );

            // Build a list of CSV rows from the tables
            foreach (var table in tables)
            {
                var tableRows = table.AllRows.ToList();

                if (table.HasTotals)
                {
                    tableRows.Add
                    (
                        new TableRow
                        (
                            table.Totals
                        )
                    );
                }

                foreach (var tableRow in tableRows)
                {
                    var cellValues = allColumns.ToDictionary
                    (
                        name => name,
                        name => (object)default(string)
                    );

                    foreach (var tableCell in tableRow.Cells)
                    {
                        var columnName = tableCell.Column.Name;

                        cellValues[columnName] = tableCell.Value;
                    }

                    var csvCells = new List<CsvCell>();

                    foreach (var pair in cellValues)
                    {
                        csvCells.Add
                        (
                            new CsvCell(pair.Value)
                        );
                    }

                    var csvRow = new CsvRow
                    (
                        csvCells.ToArray()
                    );

                    allRows.Add(csvRow);
                }
            }

            document = document.WithRows
            (
                allRows.ToArray()
            );

            return document;
        }

        /// <summary>
        /// Extracts all tables from a report
        /// </summary>
        /// <param name="report">The report</param>
        /// <returns>A collection of tables</returns>
        private IEnumerable<Table> ExtractTables
            (
                Report report
            )
        {
            var allTables = new List<Table>();

            if (report.ReportHeader != null)
            {
                var sectionTables = ExtractTables
                (
                    report.ReportHeader
                );

                allTables.AddRange(sectionTables);
            }

            if (report.PageHeader != null)
            {
                var sectionTables = ExtractTables
                (
                    report.PageHeader
                );

                allTables.AddRange(sectionTables);
            }

            if (report.Body != null)
            {
                var sectionTables = ExtractTables
                (
                    report.Body
                );

                allTables.AddRange(sectionTables);
            }

            if (report.PageFooter != null)
            {
                var sectionTables = ExtractTables
                (
                    report.PageFooter
                );

                allTables.AddRange(sectionTables);
            }

            if (report.ReportFooter != null)
            {
                var sectionTables = ExtractTables
                (
                    report.ReportFooter
                );

                allTables.AddRange(sectionTables);
            }

            return allTables;
        }

        /// <summary>
        /// Extracts all tables from a report section
        /// </summary>
        /// <param name="section">The report section</param>
        /// <returns>A collection of tables</returns>
        private IEnumerable<Table> ExtractTables
            (
                ReportSection section
            )
        {
            return ExtractTables
            (
                section.Components
            );
        }

        /// <summary>
        /// Extracts all tables from a collection of report components
        /// </summary>
        /// <param name="components">The report components</param>
        /// <returns>A collection of tables</returns>
        private IEnumerable<Table> ExtractTables
            (
                params IReportComponent[] components
            )
        {
            var allTables = new List<Table>();

            var rootTables = components.Where
            (
                c => c.ComponentDefinition.ComponentType == ReportComponentType.Table
            );

            foreach (Table table in rootTables)
            {
                allTables.Add(table);
            }
            
            var repeaters = components.Where
            (
                c => c.ComponentDefinition.ComponentType == ReportComponentType.Repeater
            );

            foreach (Repeater repeater in repeaters)
            {
                foreach (var item in repeater.Items)
                {
                    if (item.NestedComponents.Any())
                    {
                        var nestedTables = ExtractTables
                        (
                            item.NestedComponents
                        );

                        allTables.AddRange(nestedTables);
                    }
                }
            }

            return allTables;
        }
    }
}
