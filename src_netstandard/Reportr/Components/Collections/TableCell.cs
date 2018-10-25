namespace Reportr.Components.Collections
{
    using Newtonsoft.Json;
    using Reportr.Data;

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
                ReportAction action = null
            )
        {
            Validate.IsNotNull(column);

            this.Column = column;
            this.Value = value;
            this.Action = action;

            if (value == null)
            {
                this.FormattingType = DataValueFormattingType.None;
            }
            else
            {
                this.FormattingType = value.GetType().GetFormattingType
                (
                    value
                );
            }
        }

        /// <summary>
        /// Gets the associated column
        /// </summary>
        [JsonIgnore]
        public TableColumn Column { get; private set; }

        /// <summary>
        /// Gets the value formatting type
        /// </summary>
        public DataValueFormattingType FormattingType { get; internal set; }

        /// <summary>
        /// Gets the cells value
        /// </summary>
        public object Value { get; private set; }
        
        /// <summary>
        /// Gets the cells action
        /// </summary>
        public ReportAction Action { get; private set; }
    }
}
