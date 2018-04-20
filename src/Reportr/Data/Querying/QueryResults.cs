namespace Reportr.Data.Querying
{
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
                int executionTime,
                bool success = true
            )
        {
            Validate.IsNotNull(query);

            this.QueryName = query.Name;
            this.ExecutionTime = executionTime;
            this.Success = success;
            this.Columns = query.Columns;
        }
        
        /// <summary>
        /// Gets the name of the query that generated the results
        /// </summary>
        public string QueryName { get; private set; }

        /// <summary>
        /// Gets the queries execution time
        /// </summary>
        public int ExecutionTime { get; private set; }

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
        /// Gets an array of the groupings in the result
        /// </summary>
        public QueryGrouping[] Groupings { get; private set; }

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

            this.Groupings = groupings;

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

            return this;
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
