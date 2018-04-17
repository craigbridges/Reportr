namespace Reportr.Data.Querying
{
    using System.Collections.Generic;

    /// <summary>
    /// Defines a contract for a repository that manages queries
    /// </summary>
    public interface IQueryRepository
    {
        /// <summary>
        /// Adds a query to the repository
        /// </summary>
        /// <param name="query">The query</param>
        void AddQuery
        (
            IQuery query
        );

        /// <summary>
        /// Gets all queries in the repository
        /// </summary>
        /// <returns>A collection of queries</returns>
        IEnumerable<IQuery> GetAllQueries();

        /// <summary>
        /// Gets a single query by name from the repository
        /// </summary>
        /// <param name="name">The query name</param>
        /// <returns>The matching query</returns>
        IQuery GetQuery
        (
            string name
        );
    }
}
