namespace Reportr.Conversion.Csv
{
    /// <summary>
    /// Represents a single Comma Separated (CSV) row
    /// </summary>
    public class CsvRow
    {
        /// <summary>
        /// Constructs the CSV document with the cell data
        /// </summary>
        /// <param name="cells">The CSV cell data</param>
        public CsvRow
            (
                params CsvCell[] cells
            )
        {
            Validate.IsNotNull(cells);

            this.Cells = cells;
        }

        /// <summary>
        /// Gets an array of CSV cells
        /// </summary>
        public CsvCell[] Cells { get; }
    }
}
