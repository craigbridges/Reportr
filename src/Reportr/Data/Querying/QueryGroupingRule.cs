namespace Reportr.Data.Querying
{
    using Reportr.Data;

    /// <summary>
    /// Represents a single query grouping rule
    /// </summary>
    public sealed class QueryGroupingRule
    {
        /// <summary>
        /// Constructs the grouping rule with the details
        /// </summary>
        /// <param name="column">The query column</param>
        /// <param name="direction">The sorting direction</param>
        public QueryGroupingRule
            (
                QueryColumnInfo column,
                SortDirection direction
            )
        {
            Validate.IsNotNull(column);

            this.Column = column;
            this.Direction = direction;
        }

        /// <summary>
        /// Gets the query column
        /// </summary>
        public QueryColumnInfo Column { get; private set; }

        /// <summary>
        /// Gets the sorting direction
        /// </summary>
        public SortDirection Direction { get; private set; }
    }
}
