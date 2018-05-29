namespace Reportr.Filtering
{
    using Reportr.Data;

    /// <summary>
    /// Represents a report sortable column
    /// </summary>
    public class ReportSortableColumn
    {
        /// <summary>
        /// Constructs the sortable column with the details
        /// </summary>
        /// <param name="queryName">The query name</param>
        /// <param name="columnName">The column name</param>
        /// <param name="defaultDirection">The default sort direction</param>
        internal ReportSortableColumn
            (
                string queryName,
                string columnName,
                SortDirection defaultDirection = SortDirection.Ascending
            )
        {
            Validate.IsNotEmpty(queryName);
            Validate.IsNotEmpty(columnName);

            this.QueryName = queryName;
            this.ColumnName = columnName;
            this.DefaultDirection = defaultDirection;
        }

        /// <summary>
        /// Gets the name of the query
        /// </summary>
        public string QueryName { get; protected set; }

        /// <summary>
        /// Gets the name of the column
        /// </summary>
        public string ColumnName { get; protected set; }

        /// <summary>
        /// Gets the default sort direction
        /// </summary>
        public SortDirection DefaultDirection { get; protected set; }
    }
}
