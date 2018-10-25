namespace Reportr.Integrations.Autofac.Repositories
{
    using global::Autofac;
    using Reportr.Data.Querying;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Represents an Autofac query repository implementation
    /// </summary>
    public sealed class AutofacQueryRepository : IQueryRepository
    {
        private readonly ILifetimeScope _context;
        private IEnumerable<IQuery> _queries;

        /// <summary>
        /// Constructs the repository with a component context
        /// </summary>
        /// <param name="context">The Autofac component context</param>
        public AutofacQueryRepository
            (
                ILifetimeScope context
            )
        {
            Validate.IsNotNull(context);

            _context = context;
            _queries = context.Resolve<IEnumerable<IQuery>>();
        }

        /// <summary>
        /// Adds a query to the repository
        /// </summary>
        /// <param name="query">The query</param>
        public void AddQuery
            (
                IQuery query
            )
        {
            Validate.IsNotNull(query);

            var found = QueryExists
            (
                query.Name
            );

            if (found)
            {
                var message = "The query '{0}' has already been added.";

                throw new InvalidOperationException
                (
                    String.Format
                    (
                        message,
                        query
                    )
                );
            }

            var queryList = _queries.ToList();

            queryList.Add(query);

            _queries = queryList.ToArray();
        }

        /// <summary>
        /// Determines if a query exists with the name specified
        /// </summary>
        /// <param name="name">The name of the query</param>
        /// <returns>True, if the query exists; otherwise false</returns>
        public bool QueryExists
            (
                string name
            )
        {
            Validate.IsNotEmpty(name);

            return _queries.Any
            (
                m => m.Name.ToLower() == name.ToLower()
            );
        }

        /// <summary>
        /// Gets a single query by name
        /// </summary>
        /// <param name="name">The query name</param>
        /// <returns>The query</returns>
        public IQuery GetQuery
            (
                string name
            )
        {
            Validate.IsNotEmpty(name);

            var query = _queries.FirstOrDefault
            (
                m => m.Name.ToLower() == name.ToLower()
            );

            if (query == null)
            {
                var message = "No query was found with the name '{0}'.";

                throw new KeyNotFoundException
                (
                    String.Format
                    (
                        message,
                        name
                    )
                );
            }

            return query;
        }

        /// <summary>
        /// Gets all queries in the repository
        /// </summary>
        /// <returns>A collection of queries</returns>
        public IEnumerable<IQuery> GetAllQueries()
        {
            return _queries.OrderBy
            (
                a => a.Name
            );
        }

        /// <summary>
        /// Removes a query from the repository
        /// </summary>
        /// <param name="name">The query name</param>
        public void RemoveQuery
            (
                string name
            )
        {
            Validate.IsNotEmpty(name);

            var query = GetQuery(name);
            var queryList = _queries.ToList();

            queryList.Remove(query);

            _queries = queryList.ToArray();
        }
    }
}
