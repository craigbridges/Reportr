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
        /// <param name="action">The action (optional)</param>
        public TableCell
            (
                TableColumn column,
                object value,
                ReportActionOutput action = null
            )
        {
            Validate.IsNotNull(column);

            this.Column = column;
            this.Value = value;
            this.Action = action;
        }

        /// <summary>
        /// Gets the associated column
        /// </summary>
        public TableColumn Column { get; private set; }

        /// <summary>
        /// Gets the cells value
        /// </summary>
        public object Value { get; private set; }
        
        /// <summary>
        /// Gets the cells action
        /// </summary>
        public ReportActionOutput Action { get; private set; }
    }
}
