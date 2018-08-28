namespace Reportr.Conversion.Csv
{
    /// <summary>
    /// Represents a single Comma Separated (CSV) cell
    /// </summary>
    public class CsvCell
    {
        /// <summary>
        /// Constructs the cell with a value
        /// </summary>
        /// <param name="value">The cell value</param>
        public CsvCell
            (
                object value
            )
        {
            this.Value = value;
        }

        /// <summary>
        /// Gets the CSV cell value
        /// </summary>
        public object Value { get; }
    }
}
