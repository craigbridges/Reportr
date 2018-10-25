namespace Reportr.Conversion.Csv
{
    using System;
    using System.Linq;

    /// <summary>
    /// Represents a single Comma Separated (CSV) document
    /// </summary>
    public class CsvDocument
    {
        /// <summary>
        /// Constructs the CSV document with the headings
        /// </summary>
        /// <param name="headings">The CSV headings</param>
        public CsvDocument
            (
                params string[] headings
            )
        {
            Validate.IsNotNull(headings);

            this.Headings = headings;
            this.Rows = new CsvRow[] { };
        }

        /// <summary>
        /// Constructs the CSV document with the row data
        /// </summary>
        /// <param name="rows">The CSV row data</param>
        public CsvDocument
            (
                params CsvRow[] rows
            )
        {
            Validate.IsNotNull(rows);

            this.Rows = rows;

            if (rows.Any())
            {
                var cells = rows.First().Cells;

                var nullFound = cells.Any
                (
                    cell => cell.Value == null
                );

                if (nullFound)
                {
                    throw new InvalidOperationException
                    (
                        "The CSV heading values cannot be null."
                    );
                }

                var headings = cells.Select
                (
                    cell => cell.Value.ToString()
                );

                this.Headings = headings.ToArray();
            }
            else
            {
                this.Headings = new string[] { };
            }
        }

        /// <summary>
        /// Gets the CSV document headings
        /// </summary>
        public string[] Headings { get; }

        /// <summary>
        /// Gets an array of CSV rows
        /// </summary>
        public CsvRow[] Rows { get; private set; }

        /// <summary>
        /// Adds rows to the CSV document
        /// </summary>
        /// <param name="rows">The CSV rows<param>
        /// <returns>The updated document</returns>
        public CsvDocument WithRows
            (
                params CsvRow[] rows
            )
        {
            Validate.IsNotNull(rows);

            this.Rows = rows;

            return this;
        }
    }
}
