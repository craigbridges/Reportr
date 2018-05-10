namespace Reportr.Data.Querying
{
    /// <summary>
    /// Represents information about a single query column
    /// </summary>
    public class QueryColumnInfo
    {
        /// <summary>
        /// Constructs the column information with the details
        /// </summary>
        /// <param name="table">The table schema</param>
        /// <param name="column">The column schema</param>
        public QueryColumnInfo
            (
                DataColumnSchema table,
                DataColumnSchema column
            )
        {
            Validate.IsNotNull(table);
            Validate.IsNotNull(column);

            this.Table = table;
            this.Column = column;
        }

        /// <summary>
        /// Gets the table schema
        /// </summary>
        public DataColumnSchema Table { get; private set; }

        /// <summary>
        /// Gets the column schema
        /// </summary>
        public DataColumnSchema Column { get; private set; }
    }
}
