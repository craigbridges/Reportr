namespace Reportr.Querying
{
    /// <summary>
    /// Represents the data for a single query cell
    /// </summary>
    public class QueryCell
    {
        /// <summary>
        /// Constructs the query cell with the data
        /// </summary>
        /// <param name="columnName">The column name</param>
        /// <param name="value">The cell value</param>
        public QueryCell
            (
                string columnName,
                object value
            )
        {
            Validate.IsNotEmpty(columnName);

            this.ColumnName = columnName;
            this.Value = value;
        }

        /// <summary>
        /// Gets the associated column name
        /// </summary>
        public string ColumnName { get; private set; }

        /// <summary>
        /// Gets the cells value
        /// </summary>
        public object Value { get; private set; }
    }
}
