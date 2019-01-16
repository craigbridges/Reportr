namespace Reportr.Data.Querying
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;

    /// <summary>
    /// Represents a single SQL statement
    /// </summary>
    public class SqlStatement
    {
        /// <summary>
        /// Constructs the SQL statement with the text
        /// </summary>
        /// <param name="sql">The SQL text</param>
        public SqlStatement
            (
                string sql
            )
        {
            Validate.IsNotEmpty(sql);

            this.Sql = sql;

            this.Parameters = new ReadOnlyDictionary<string, object>
            (
                new Dictionary<string, object>()
            );
        }

        /// <summary>
        /// Constructs the SQL statement with the text and parameters
        /// </summary>
        /// <param name="sql">The SQL text</param>
        /// <param name="parameters">The parameters</param>
        public SqlStatement
            (
                string sql,
                Dictionary<string, object> parameters
            )
        {
            Validate.IsNotEmpty(sql);
            Validate.IsNotNull(parameters);

            this.Sql = sql;

            this.Parameters = new ReadOnlyDictionary<string, object>
            (
                parameters
            );
        }

        /// <summary>
        /// Gets the SQL text
        /// </summary>
        public string Sql { get; private set; }

        /// <summary>
        /// Gets the parameters for the statement
        /// </summary>
        public ReadOnlyDictionary<string, object> Parameters { get; private set; }
    }
}
