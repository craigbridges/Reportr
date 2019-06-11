namespace Reportr.Data.Querying
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;

    /// <summary>
    /// Represents the results of the execution of a query
    /// </summary>
    public class QueryResults : IEnumerable<QueryGrouping>
    {
        /// <summary>
        /// Constructs the query results with the details
        /// </summary>
        /// <param name="query">The query that was executed</param>
        /// <param name="executionTime">The execution time in milliseconds</param>
        /// <param name="success">True, if the query executed successfully</param>
        public QueryResults
            (
                IQuery query,
                long executionTime,
                bool success = true
            )
        {
            Validate.IsNotNull(query);

            this.QueryName = query.Name;
            this.ExecutionTime = executionTime;
            this.Success = success;

            this.Columns = query.Columns;
            this.Groupings = new QueryGrouping[] { };
            this.AllRows = new QueryRow[] { };
        }

        /// <summary>
        /// Gets the name of the query that generated the results
        /// </summary>
        public string QueryName { get; private set; }

        /// <summary>
        /// Gets the queries execution time (in milliseconds)
        /// </summary>
        public long ExecutionTime { get; private set; }

        /// <summary>
        /// Gets a flag indicating if the queries ran successfully
        /// </summary>
        public bool Success { get; private set; }

        /// <summary>
        /// Adds the error messages to the query results
        /// </summary>
        /// <param name="errors">The error messages to add</param>
        /// <returns>The updated query results</returns>
        public QueryResults WithErrors
            (
                IDictionary<string, string> errors
            )
        {
            Validate.IsNotNull(errors);

            this.ErrorMessages = new ReadOnlyDictionary<string, string>
            (
                errors
            );

            this.Success = false;

            return this;
        }

        /// <summary>
        /// Gets any error messages that were generated
        /// </summary>
        /// <remarks>
        /// The error messages are grouped by error code.
        /// </remarks>
        public IDictionary<string, string> ErrorMessages { get; private set; }

        /// <summary>
        /// Gets an array of the columns from the query
        /// </summary>
        public QueryColumnInfo[] Columns { get; private set; }

        /// <summary>
        /// Gets the index of a specified column within the query results
        /// </summary>
        /// <param name="columnName">The column name</param>
        /// <returns>The column index</returns>
        internal int GetColumnIndex
            (
                string columnName
            )
        {
            Validate.IsNotEmpty(columnName);

            var matchingColumn = this.Columns.FirstOrDefault
            (
                c => c.Column.Name.Equals(columnName, StringComparison.OrdinalIgnoreCase)
            );

            if (matchingColumn == null)
            {
                throw new ArgumentException
                (
                    $"The column '{columnName}' could not be found."
                );
            }

            var columnIndex = this.Columns.ToList().IndexOf
            (
                matchingColumn
            );

            return columnIndex;
        }

        /// <summary>
        /// Gets an array of the groupings in the result
        /// </summary>
        public QueryGrouping[] Groupings { get; private set; }

        /// <summary>
        /// Gets a flag indicating if the result has multiple groupings
        /// </summary>
        public bool HasMultipleGroupings { get; private set; }

        /// <summary>
        /// Gets an array of the all rows in the result
        /// </summary>
        public QueryRow[] AllRows { get; private set; }

        /// <summary>
        /// Gets the query row count
        /// </summary>
        public int RowCount { get; private set; }

        /// <summary>
        /// Adds the query data to the result
        /// </summary>
        /// <param name="groupings">The groupings generated</param>
        /// <returns>The updated query result</returns>
        public QueryResults WithData
            (
                params QueryGrouping[] groupings
            )
        {
            Validate.IsNotNull(groupings);

            if (groupings.Length == 0)
            {
                throw new ArgumentException
                (
                    "At least one query grouping must be defined."
                );
            }

            this.Groupings = groupings;
            this.HasMultipleGroupings = groupings.Length > 1;

            var allRows = new List<QueryRow>();

            foreach (var grouping in groupings)
            {
                allRows.AddRange
                (
                    grouping.Rows
                );
            }

            this.AllRows = allRows.ToArray();
            this.RowCount = allRows.Count;
            this.Success = true;

            return this;
        }

        /// <summary>
        /// Adds the query data to the result
        /// </summary>
        /// <param name="rows">The rows generated</param>
        /// <returns>The updated query result</returns>
        public QueryResults WithData
            (
                params QueryRow[] rows
            )
        {
            Validate.IsNotNull(rows);

            this.Groupings = new QueryGrouping[] { };
            this.HasMultipleGroupings = false;

            this.AllRows = rows;
            this.RowCount = rows.Length;
            this.Success = true;

            return this;
        }

        /// <summary>
        /// Finds all rows in the results matching a column name and value
        /// </summary>
        /// <param name="columnName">The column name</param>
        /// <param name="value">The value to match on</param>
        /// <returns>The matching query row; if found; otherwise null</returns>
        public IEnumerable<QueryRow> FindRows
            (
                string columnName,
                object value
            )
        {
            var columnIndex = GetColumnIndex
            (
                columnName
            );

            if (value != null)
            {
                var rows = this.AllRows.Where
                (
                    r => r.Cells[columnIndex].Value != null
                        && r.Cells[columnIndex].Value.ToString().Equals
                        (
                            value.ToString(),
                            StringComparison.InvariantCultureIgnoreCase
                        )
                );

                return rows.ToList();
            }
            else
            {
                var rows = this.AllRows.Where
                (
                    r => r.Cells[columnIndex].Value == null
                );

                return rows.ToList();
            }
        }

        /// <summary>
        /// Gets the group at the index specified
        /// </summary>
        /// <param name="index">The row index (zero based)</param>
        /// <returns>The matching query row</returns>
        public QueryGrouping this[int index]
        {
            get
            {
                return this.Groupings[index];
            }
        }

        /// <summary>
        /// Gets an enumerator for the collection of groupings
        /// </summary>
        /// <returns>The enumerator</returns>
        public IEnumerator<QueryGrouping> GetEnumerator()
        {
            return this.Groupings.ToList().GetEnumerator();
        }

        /// <summary>
        /// Gets a generic enumerator for the collection of groupings
        /// </summary>
        /// <returns>The generic enumerator</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
