﻿namespace Reportr.Data.Entity
{
    using Reportr.Data.Querying;
    using Reportr.Filtering;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Linq;
    using System.Threading.Tasks;

    /// <summary>
    /// Represents a base class for generic Entity Framework report queries
    /// </summary>
    /// <typeparam name="T">The query output type</typeparam>
    public abstract class EfQuery<T> : QueryBase
    {
        private QueryColumnInfo[] _columns = null;

        /// <summary>
        /// Constructs the query with an Entity Framework data source
        /// </summary>
        /// <param name="dataSource">The EF data source</param>
        public EfQuery
            (
                EfDataSource dataSource
            )

            : base(dataSource)
        {
            _columns = ResolveColumns<T>();
        }
        
        /// <summary>
        /// Gets an array of the columns generated by the query
        /// </summary>
        public override QueryColumnInfo[] Columns
        {
            get
            {
                if (_columns == null || _columns.Length == 0)
                {
                    _columns = ResolveColumns<T>();
                }

                return _columns;
            }
        }

        /// <summary>
        /// Gets the database context from the data source
        /// </summary>
        /// <returns>The database context</returns>
        protected DbContext GetContext()
        {
            return ((EfDataSource)this.DataSource).Context;
        }

        /// <summary>
        /// Gets a queryable collection of all entities from a database set
        /// </summary>
        /// <typeparam name="TEntity">The entity type</typeparam>
        /// <param name="context">The database context</param>
        /// <returns>An IQueryable of all entities in the set</returns>
        protected virtual IQueryable<TEntity> GetAll<TEntity>
            (
                DbContext context
            )

            where TEntity : class
        {
            Validate.IsNotNull(context);

            return context.Set<TEntity>().AsNoTracking();
        }

        /// <summary>
        /// Asynchronously generates a queryable from a database context
        /// </summary>
        /// <param name="context">The database context</param>
        /// <param name="parameterValues">The parameter values</param>
        /// <returns>The query generated</returns>
        protected abstract Task<IQueryable<T>> GenerateQuerableAsync
        (
            DbContext context,
            params ParameterValue[] parameterValues
        );

        /// <summary>
        /// Asynchronously fetches the query data using the parameter values
        /// </summary>
        /// <param name="parameterValues">The parameter values</param>
        /// <returns>The query data in the form of an array of rows</returns>
        protected override async Task<IEnumerable<QueryRow>> FetchDataAsync
            (
                params ParameterValue[] parameterValues
            )
        {
            Validate.IsNotNull(parameterValues);

            var context = GetContext();

            var queryable = await GenerateQuerableAsync
            (
                context,
                parameterValues
            )
            .ConfigureAwait
            (
                false
            );

            EnsureRowCountValid(queryable);

            // NOTE:
            // We are checking if the queryable implements the interface
            // IDbAsyncEnumerable which allows us to ensure the queryable
            // is not a mock-up and is a real Entity Framework query.
            //
            // Only sources that implement IDbAsyncEnumerable can be used 
            // for Entity Framework asynchronous operations.

            var queryResults = default(List<T>);

            if (queryable is IDbAsyncEnumerable)
            {
                var listTask = queryable.ToListAsync();

                queryResults = await listTask.ConfigureAwait
                (
                    false
                );
            }
            else
            {
                queryResults = queryable.ToList();
            }
            
            var rows = ConvertToRows<T>
            (
                queryResults
            );

            return rows;
        }
    }
}
