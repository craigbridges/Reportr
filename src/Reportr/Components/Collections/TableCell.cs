namespace Reportr.Components.Collections
{
    /// <summary>
    /// Represents the data for a single table cell
    /// </summary>
    public class TableCell
    {
        /// <summary>
        /// Constructs the table cell with the data
        /// </summary>
        /// <param name="column">The column</param>
        /// <param name="value">The cell value</param>
        public TableCell
            (
                TableColumn column,
                object value
            )
        {
            Validate.IsNotNull(column);

            this.Column = column;
            this.Value = value;
        }

        /// <summary>
        /// Gets the associated column
        /// </summary>
        public TableColumn Column { get; private set; }

        /// <summary>
        /// Gets the cells value
        /// </summary>
        public object Value { get; private set; }
    }
}
