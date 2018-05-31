namespace Reportr.Data.Querying
{
    using Reportr.Data;

    /// <summary>
    /// Represents a single query sorting rule
    /// </summary>
    public class QuerySortingRule
    {
        /// <summary>
        /// Constructs the sorting rule with the details
        /// </summary>
        /// <param name="columnName">The column name</param>
        /// <param name="direction">The sorting direction</param>
        public QuerySortingRule
            (
                string columnName,
                SortDirection direction
            )
        {
            Validate.IsNotEmpty(columnName);

            this.ColumnName = columnName;
            this.Direction = direction;
        }

        /// <summary>
        /// Gets the name of the query column
        /// </summary>
        public string ColumnName { get; private set; }

        /// <summary>
        /// Gets the sorting direction
        /// </summary>
        public SortDirection Direction { get; private set; }
    }
}
