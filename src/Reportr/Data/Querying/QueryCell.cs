namespace Reportr.Data.Querying
{
    using System;

    /// <summary>
    /// Represents the data for a single query cell
    /// </summary>
    public class QueryCell
    {
        /// <summary>
        /// Constructs the query cell with the data
        /// </summary>
        /// <param name="column">The column schema</param>
        /// <param name="value">The cell value</param>
        public QueryCell
            (
                DataColumnSchema column,
                object value
            )
        {
            Validate.IsNotNull(column);

            this.Column = column;
            this.Value = value;

            this.FormattingType = column.ValueType.GetFormattingType
            (
                value
            );
        }

        /// <summary>
        /// Gets the associated column schema
        /// </summary>
        public DataColumnSchema Column { get; private set; }

        /// <summary>
        /// Gets the value formatting type
        /// </summary>
        public DataValueFormattingType FormattingType { get; private set; }

        /// <summary>
        /// Gets the cells value
        /// </summary>
        public object Value { get; private set; }
    }
}
