﻿namespace Reportr.Data.Entity
{
    using Reportr.Data.Querying;
    using Reportr.Filtering;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Reflection;
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
            ResolveColumns();
        }

        /// <summary>
        /// Resolves the queries columns from the output type
        /// </summary>
        private void ResolveColumns()
        {
            var entityType = typeof(T);
            var tableSchema = ResolveTableSchema<T>();

            var properties = entityType.GetProperties
            (
                BindingFlags.Public | BindingFlags.Instance
            );

            var columnInfos = new List<QueryColumnInfo>();

            foreach (var property in properties)
            {
                columnInfos.Add
                (
                    new QueryColumnInfo
                    (
                        tableSchema,
                        new DataColumnSchema
                        (
                            property.Name,
                            property.PropertyType
                        )
                    )
                );
            }

            _columns = columnInfos.ToArray();
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
                    ResolveColumns();
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

            return context.Set<TEntity>();
        }

        /// <summary>
        /// Generates a queryable for the database context specified
        /// </summary>
        /// <param name="context">The database context</param>
        /// <param name="parameterValues">The parameter values</param>
        /// <returns>The query generated</returns>
        protected abstract IQueryable<T> GenerateQuerable
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

            var queryable = GenerateQuerable
            (
                context,
                parameterValues
            );

            if (this.MaximumRows.HasValue)
            {
                var rowCount = queryable.Count();

                if (rowCount > this.MaximumRows.Value)
                {
                    var message = "The query '{0}' returned {1} rows, but" +
                                  "the maximum number of rows allowed is {2}.";

                    throw new InvalidOperationException
                    (
                        String.Format
                        (
                            message,
                            this.Name,
                            rowCount,
                            this.MaximumRows
                        )
                    );
                }
            }

            var queryResults = await queryable.ToListAsync();
            var rows = new List<QueryRow>();
            var entityType = typeof(T);

            foreach (var item in queryResults)
            {
                var cells = new List<QueryCell>();

                foreach (var info in _columns)
                {
                    var property = entityType.GetProperty
                    (
                        info.Column.Name
                    );

                    var propertyValue = property.GetValue
                    (
                        item
                    );

                    cells.Add
                    (
                        new QueryCell
                        (
                            info.Column,
                            propertyValue
                        )
                    );
                }

                rows.Add
                (
                    new QueryRow
                    (
                        cells.ToArray()
                    )
                );
            }

            return rows;
        }

        /// <summary>
        /// Gets a parameter value from the parameters supplied
        /// </summary>
        /// <typeparam name="TValue">The parameter value type to return</typeparam>
        /// <param name="parameterName">The parameter name</param>
        /// <param name="parameterValues">The parameter values</param>
        /// <returns>The parameter value as the type specified</returns>
        protected virtual TValue GetParameterValue<TValue>
            (
                string parameterName,
                params ParameterValue[] parameterValues
            )
        {
            Validate.IsNotEmpty(parameterName);

            var matchingItem = parameterValues.FirstOrDefault
            (
                pv => pv.Name.ToLower() == parameterName.ToLower()
            );

            if (matchingItem == null || matchingItem.Value == null)
            {
                return default(TValue);
            }
            else
            {
                var currentType = matchingItem.Value.GetType();

                if (currentType == typeof(TValue))
                {
                    return (TValue)matchingItem.Value;
                }
                else
                {
                    var converter = new ObjectConverter<TValue>();

                    return converter.Convert
                    (
                        matchingItem.Value
                    );
                }
            }
        }
    }
}
