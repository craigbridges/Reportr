﻿namespace Reportr.Data.Querying
{
    using Nito.AsyncEx.Synchronous;
    using Reportr.Filtering;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Reflection;
    using System.Threading.Tasks;

    /// <summary>
    /// Represents a base implementation for a report query
    /// </summary>
    public abstract class QueryBase : IQuery
    {
        private Dictionary<string, QuerySortingRule> _sortingRules;
        private List<string> _groupingColumns;

        /// <summary>
        /// Constructs the query with a data source
        /// </summary>
        /// <param name="dataSource">The data source</param>
        /// <param name="maximumRows">The maximum rows (optional)</param>
        public QueryBase
            (
                IDataSource dataSource,
                int? maximumRows = null
            )
        {
            Validate.IsNotNull(dataSource);

            _sortingRules = new Dictionary<string, QuerySortingRule>
            (
                StringComparer.OrdinalIgnoreCase
            );

            _groupingColumns = new List<string>();

            this.QueryId = Guid.NewGuid();
            this.DataSource = dataSource;
            this.MaximumRows = maximumRows;
        }

        /// <summary>
        /// Gets the unique ID of the query
        /// </summary>
        public Guid QueryId { get; private set; }

        /// <summary>
        /// Gets the name of the query
        /// </summary>
        public virtual string Name
        {
            get
            {
                return this.GetType().Name.Replace
                (
                    "Query",
                    String.Empty
                );
            }
        }

        /// <summary>
        /// Gets the data source being used by the query
        /// </summary>
        public IDataSource DataSource { get; private set; }

        /// <summary>
        /// Gets an array of the columns generated by the query
        /// </summary>
        public abstract QueryColumnInfo[] Columns { get; }

        /// <summary>
        /// Determines if the query has a column with the name specified
        /// </summary>
        /// <param name="name">The column name</param>
        /// <returns>True, if a matching column is found; otherwise false</returns>
        public bool HasColumn
            (
                string name
            )
        {
            Validate.IsNotEmpty(name);

            return this.Columns.Any
            (
                info => info.Column.Name.ToLower() == name.ToLower()
            );
        }

        /// <summary>
        /// Gets a column from the query matching the name specified
        /// </summary>
        /// <param name="name">The column name</param>
        /// <returns>The matching column</returns>
        public QueryColumnInfo GetColumn
            (
                string name
            )
        {
            Validate.IsNotEmpty(name);

            var column = this.Columns.FirstOrDefault
            (
                info => info.Column.Name.ToLower() == name.ToLower()
            );

            if (column == null)
            {
                var message = "No column was found matching the name '{0}'.";

                throw new KeyNotFoundException
                (
                    String.Format
                    (
                        message,
                        name
                    )
                );
            }

            return column;
        }

        /// <summary>
        /// Gets an array of parameters accepted by the query
        /// </summary>
        public abstract Filtering.ParameterInfo[] Parameters { get; }

        /// <summary>
        /// Gets the maximum number of rows the query will return
        /// </summary>
        /// <remarks>
        /// This is optional and, if null, all rows are returned.
        /// </remarks>
        public int? MaximumRows { get; protected set; }

        /// <summary>
        /// Gets an array of sorting rules for the query
        /// </summary>
        public QuerySortingRule[] SortingRules
        {
            get
            {
                var rules = _sortingRules.Select
                (
                    pair => pair.Value
                );

                return rules.ToArray();
            }
        }

        /// <summary>
        /// Specifies a sorting rule against a column in the query
        /// </summary>
        /// <param name="columnName">The column name</param>
        /// <param name="direction">The sort direction</param>
        public void SortColumn
            (
                string columnName,
                SortDirection direction
            )
        {
            Validate.IsNotEmpty(columnName);

            var columnFound = this.Columns.Any
            (
                info => info.Column.Name.ToLower() == columnName.ToLower()
            );

            if (false == columnFound)
            {
                var message = "The column '{0}' does not exist.";

                throw new InvalidOperationException
                (
                    String.Format
                    (
                        message,
                        columnName
                    )
                );
            }
            
            var rule = new QuerySortingRule
            (
                columnName,
                direction
            );

            _sortingRules[columnName] = rule;
        }

        /// <summary>
        /// Gets an array of grouping columns for the query
        /// </summary>
        public string[] GroupingColumns
        {
            get
            {
                return _groupingColumns.ToArray();
            }
        }

        /// <summary>
        /// Adds a grouping column to the query
        /// </summary>
        /// <param name="columnName">The column name</param>
        public void AddGrouping
            (
                string columnName
            )
        {
            Validate.IsNotEmpty(columnName);

            var columnFound = this.Columns.Any
            (
                info => info.Column.Name.ToLower() == columnName.ToLower()
            );

            if (false == columnFound)
            {
                var message = "The column '{0}' does not exist.";

                throw new InvalidOperationException
                (
                    String.Format
                    (
                        message,
                        columnName
                    )
                );
            }
            
            _groupingColumns.Add(columnName);
        }

        /// <summary>
        /// Executes the query using the parameter values supplied
        /// </summary>
        /// <param name="parameterValues">The parameter values</param>
        /// <returns>The query results</returns>
        public virtual QueryResults Execute
            (
                params ParameterValue[] parameterValues
            )
        {
            var task = ExecuteAsync(parameterValues);

            return task.WaitAndUnwrapException();
        }

        /// <summary>
        /// Asynchronously executes the query using the parameter values supplied
        /// </summary>
        /// <param name="parameterValues">The parameter values</param>
        /// <returns>The query results</returns>
        public virtual async Task<QueryResults> ExecuteAsync
            (
                params ParameterValue[] parameterValues
            )
        {
            var watch = Stopwatch.StartNew();

            if (parameterValues == null)
            {
                parameterValues = new ParameterValue[] { };
            }

            var parameterErrors = ValidateParameterValues
            (
                parameterValues
            );

            if (parameterErrors.Any())
            {
                var results = new QueryResults
                (
                    this,
                    0,
                    false
                );

                return results.WithErrors
                (
                    parameterErrors
                );
            }
            else
            {
                var rows = await FetchDataAsync
                (
                    parameterValues
                );

                rows = SortRows(rows);

                var groupings = GroupRows(rows);

                watch.Stop();

                var executionTime = watch.ElapsedMilliseconds;

                var results = new QueryResults
                (
                    this,
                    executionTime
                );

                return results.WithData
                (
                    groupings
                );
            }
        }

        /// <summary>
        /// Asynchronously fetches the query data using the parameter values
        /// </summary>
        /// <param name="parameterValues">The parameter values</param>
        /// <returns>The query data in the form of an array of rows</returns>
        protected abstract Task<IEnumerable<QueryRow>> FetchDataAsync
        (
            params ParameterValue[] parameterValues
        );

        /// <summary>
        /// Sorts a collection of rows by the queries sorting rules
        /// </summary>
        /// <param name="rows">The rows to sort</param>
        /// <returns>A collection of sorted rows</returns>
        protected virtual IEnumerable<QueryRow> SortRows
            (
                IEnumerable<QueryRow> rows
            )
        {
            Validate.IsNotNull(rows);

            if (false == this.SortingRules.Any())
            {
                return rows;
            }
            else
            {
                var ruleNumber = 1;
                var sortedRows = (IOrderedEnumerable<QueryRow>)rows;

                foreach (var rule in this.SortingRules)
                {
                    object keySelector(QueryRow row) => row.First
                    (
                        cell => cell.Column.Name.ToLower() == rule.ColumnName.ToLower()
                    )
                    .Value;

                    if (rule.Direction == SortDirection.Ascending)
                    {
                        if (ruleNumber == 1)
                        {
                            sortedRows = sortedRows.OrderBy
                            (
                                keySelector
                            );
                        }
                        else
                        {
                            sortedRows = sortedRows.ThenBy
                            (
                                keySelector
                            );
                        }
                    }
                    else
                    {
                        if (ruleNumber == 1)
                        {
                            sortedRows = sortedRows.OrderByDescending
                            (
                                keySelector
                            );
                        }
                        else
                        {
                            sortedRows = sortedRows.ThenByDescending
                            (
                                keySelector
                            );
                        }
                    }

                    ruleNumber++;
                }

                return rows;
            }
        }

        /// <summary>
        /// Groups a collection of rows by the queries grouping rules
        /// </summary>
        /// <param name="rows">The rows to group</param>
        /// <returns>An array of query groupings</returns>
        protected virtual QueryGrouping[] GroupRows
            (
                IEnumerable<QueryRow> rows
            )
        {
            Validate.IsNotNull(rows);

            if (false == _groupingColumns.Any())
            {
                return new QueryGrouping[]
                {
                    new QueryGrouping
                    (
                        this.Columns,
                        rows.ToArray()
                    )
                };
            }
            else
            {
                var groupings = new List<QueryGrouping>();
                var groupedRows = new Dictionary<string, List<QueryRow>>();

                foreach (var row in rows)
                {
                    var groupingValue = String.Empty;

                    foreach (var columnName in _groupingColumns)
                    {
                        groupingValue += row[columnName].Value;
                    }

                    if (groupedRows.ContainsKey(groupingValue))
                    {
                        groupedRows[groupingValue].Add
                        (
                            row
                        );
                    }
                    else
                    {
                        groupedRows.Add
                        (
                            groupingValue,
                            new List<QueryRow> { row }
                        );
                    }
                }

                foreach (var item in groupedRows)
                {
                    var firstRow = item.Value.First();
                    var groupingValues = new Dictionary<QueryColumnInfo, object>();

                    foreach (var columnName in _groupingColumns)
                    {
                        var column = this.Columns.First
                        (
                            info => info.Column.Name.ToLower() == columnName.ToLower()
                        );

                        groupingValues.Add
                        (
                            column,
                            firstRow[columnName].Value
                        );
                    }

                    var grouping = new QueryGrouping
                    (
                        groupingValues,
                        item.Value.ToArray()
                    );

                    groupings.Add(grouping);
                }

                return groupings.ToArray();
            }
        }

        /// <summary>
        /// Validates parameter values against the query
        /// </summary>
        /// <param name="parameterValues">The parameter values</param>
        /// <returns>A dictionary of errors generated</returns>
        private IDictionary<string, string> ValidateParameterValues
            (
                params ParameterValue[] parameterValues
            )
        {
            var errors = new Dictionary<string, string>();
            var parameters = this.Parameters;

            foreach (var value in parameterValues)
            {
                var parameterName = value.Parameter.Name;

                var matchingParameter = parameters.FirstOrDefault
                (
                    p => p.Name.ToLower() == parameterName.ToLower()
                );

                if (matchingParameter == null)
                {
                    var message = "'{0}' did not match any parameters in the query '{1}'.";

                    errors.Add
                    (
                        parameterName,
                        String.Format
                        (
                            message,
                            parameterName,
                            this.Name
                        )
                    );
                }
                else
                {
                    // Ensure all parameter values match the expected type
                    if (value.Value != null)
                    {
                        var actualType = value.Value.GetType();

                        if (matchingParameter.ExpectedType != actualType)
                        {
                            var canConvert = actualType.CanConvert
                            (
                                matchingParameter.ExpectedType,
                                value.Value
                            );

                            if (false == canConvert)
                            {
                                var message = "The type {0} is not valid for the parameter '{1}'.";

                                errors.Add
                                (
                                    parameterName,
                                    String.Format
                                    (
                                        message,
                                        actualType.Name,
                                        parameterName
                                    )
                                );
                            }
                        }
                    }
                }
            }
            
            // Ensure all required parameters have been supplied
            foreach (var parameter in parameters)
            {
                if (parameter.ValueRequired)
                {
                    var valueFound = parameterValues.Any
                    (
                        value => value.Name.ToLower() == parameter.Name.ToLower() 
                            && value.Value != null
                    );

                    if (false == valueFound)
                    {
                        var message = "A value is required for the parameter '{1}'.";

                        errors.Add
                        (
                            parameter.Name,
                            String.Format
                            (
                                message,
                                parameter.Name
                            )
                        );
                    }
                }
            }

            return errors;
        }

        /// <summary>
        /// Resolves the data table schema for a specific output type
        /// </summary>
        /// <typeparam name="TOutput">The query output type</typeparam>
        /// <returns>The table schema</returns>
        protected virtual DataTableSchema ResolveTableSchema<TOutput>()
        {
            var outputType = typeof(TOutput);
            var dataSource = this.DataSource;

            var tableSchema = dataSource.Schema.FirstOrDefault
            (
                dts => dts.Name == outputType.Name
            );

            if (tableSchema == null)
            {
                var properties = outputType.GetProperties
                (
                    BindingFlags.Public | BindingFlags.Instance
                );

                var columnSchemas = new List<DataColumnSchema>();

                foreach (var property in properties)
                {
                    columnSchemas.Add
                    (
                        new DataColumnSchema
                        (
                            property.Name,
                            property.PropertyType
                        )
                    );
                }

                tableSchema = new DataTableSchema
                (
                    outputType.Name,
                    columnSchemas.ToArray()
                );
            }

            return tableSchema;
        }

        /// <summary>
        /// Provides a custom description of the query
        /// </summary>
        /// <returns>The query name</returns>
        public override string ToString()
        {
            return this.Name;
        }
    }
}
