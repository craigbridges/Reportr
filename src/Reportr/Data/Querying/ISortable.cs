namespace Reportr.Data.Querying
{
    using System;
    
    /// <summary>
    /// Defines a contract for a data type that can be sorted
    /// </summary>
    public interface ISortable
    {
        /// <summary>
        /// Gets an array of sorting rules for the query
        /// </summary>
        QuerySortingRule[] SortingRules { get; }

        /// <summary>
        /// Specifies a sorting rule against a column in the query
        /// </summary>
        /// <param name="columnName">The column name</param>
        /// <param name="direction">The sort direction</param>
        void SortColumn
        (
            string columnName,
            SortDirection direction
        );
    }
}
