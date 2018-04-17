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
        /// <param name="importance">The importance (optional)</param>
        public QueryColumnInfo
            (
                DataColumnSchema table,
                DataColumnSchema column,
                QueryColumnImportance importance = QueryColumnImportance.Low
            )
        {
            Validate.IsNotNull(table);
            Validate.IsNotNull(column);

            this.Table = table;
            this.Column = column;
            this.Importance = importance;
        }

        /// <summary>
        /// Gets the table schema
        /// </summary>
        public DataColumnSchema Table { get; private set; }

        /// <summary>
        /// Gets the column schema
        /// </summary>
        public DataColumnSchema Column { get; private set; }
        
        /// <summary>
        /// Gets the importance of the column
        /// </summary>
        public QueryColumnImportance Importance { get; private set; }
    }
}
