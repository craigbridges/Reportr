﻿namespace Reportr.Data.Sql
{
    using Reportr.Data.Querying;
    using Reportr.Filtering;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Linq;
    using System.Threading.Tasks;

    /// <summary>
    /// Represents a base class for SQL report queries
    /// </summary>
    public abstract class SqlQuery : QueryBase
    {
        private List<QueryColumnInfo> _columns;

        /// <summary>
        /// Constructs the query with an SQL data source
        /// </summary>
        /// <param name="dataSource">The SQL data source</param>
        public SqlQuery
            (
                SqlDataSource dataSource
            )

            : base(dataSource)
        {
            _columns = new List<QueryColumnInfo>();

            DefineColumns();
        }

        /// <summary>
        /// Defines the columns used in the query
        /// </summary>
        protected abstract void DefineColumns();

        /// <summary>
        /// Defines a single query column
        /// </summary>
        /// <param name="tableName">The table name</param>
        /// <param name="columnName">The column name</param>
        /// <param name="valueType">The column value type</param>
        protected void DefineColumn
            (
                string tableName,
                string columnName,
                Type valueType
            )
        {
            Validate.IsNotEmpty(tableName);
            Validate.IsNotEmpty(columnName);
            Validate.IsNotNull(valueType);

            var columnSchema = new DataColumnSchema
            (
                columnName,
                valueType
            );

            DefineColumn
            (
                tableName,
                columnSchema
            );
        }

        /// <summary>
        /// Defines a single query column
        /// </summary>
        /// <param name="tableName">The table name</param>
        /// <param name="columnSchema">The column schema</param>
        protected void DefineColumn
            (
                string tableName,
                DataColumnSchema columnSchema
            )
        {
            Validate.IsNotEmpty(tableName);
            Validate.IsNotNull(columnSchema);

            var tableSchema = this.DataSource.GetSchemaTable
            (
                tableName
            );

            var columnName = columnSchema.Name;

            var isDefined = _columns.Any
            (
                info => info.Column.Name.ToLower() == columnName.ToLower()
            );

            if (isDefined)
            {
                var message = "A column named '{0}' has already been defined.";

                throw new InvalidOperationException
                (
                    String.Format
                    (
                        message,
                        columnName
                    )
                );
            }

            _columns.Add
            (
                new QueryColumnInfo
                (
                    tableSchema,
                    columnSchema
                )
            );
        }

        /// <summary>
        /// Gets an array of the columns generated by the query
        /// </summary>
        public override QueryColumnInfo[] Columns
        {
            get
            {
                return _columns.ToArray();
            }
        }

        /// <summary>
        /// Gets the database connection string from the data source
        /// </summary>
        /// <returns>The connection string</returns>
        protected string GetConnectionString()
        {
            return ((SqlDataSource)this.DataSource).ConnectionString;
        }

        /// <summary>
        /// Gets a parameterized SQL statement for the query
        /// </summary>
        /// <param name="parameterValues">The parameter values</param>
        /// <returns>The SQL statement</returns>
        protected abstract ParameterizedSql GenerateSqlStatement
        (
            params ParameterValue[] parameterValues
        );

        /// <summary>
        /// Generates the SQL command for the query
        /// </summary>
        /// <param name="connection">The SQL connection</param>
        /// <param name="parameterValues">The parameter values</param>
        /// <returns>The SQL command generated</returns>
        protected virtual SqlCommand GenerateSqlCommand
            (
                SqlConnection connection,
                params ParameterValue[] parameterValues
            )
        {
            var sql = GenerateSqlStatement(parameterValues);

            var command = new SqlCommand(sql.Statement, connection)
            {
                CommandType = CommandType.Text
            };

            if (sql.ParameterValues != null)
            {
                foreach (var pv in sql.ParameterValues)
                {
                    command.Parameters.Add(pv.Name);
                    command.Parameters[pv.Name].Value = pv.Value;
                }
            }

            return command;
        }

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

            var connectionString = GetConnectionString();
            
            using (var connection = new SqlConnection(connectionString))
            {
                var command = GenerateSqlCommand
                (
                    connection,
                    parameterValues
                );

                connection.Open();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    return reader.ToQueryRows
                    (
                        _columns.ToArray()
                    );
                }
            }
        }
    }
}