namespace Reportr.Data.Sql
{
    using Reportr.Data.Querying;
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using System.Linq;
    
    /// <summary>
    /// Various extension methods for SQL data readers
    /// </summary>
    public static class SqlDataReaderExtensions
    {
        /// <summary>
        /// Converts the results of a data reader to a collection of query rows
        /// </summary>
        /// <param name="reader">The data reader</param>
        /// <param name="queryColumns">The query columns</param>
        /// <returns>A collection of query rows</returns>
        public static IEnumerable<QueryRow> ToQueryRows
            (
                this SqlDataReader reader,
                params QueryColumnInfo[] queryColumns
            )
        {
            var rows = new List<QueryRow>();

            if (reader.HasRows)
            {
                var columnSchemas = queryColumns.Select
                (
                    info => info.Column
                );

                while (reader.Read())
                {
                    var cells = new List<QueryCell>();

                    for (var i = 0; i < reader.FieldCount; i++)
                    {
                        var fieldName = reader.GetName(i);
                        var fieldValue = reader.GetValue(i);

                        var columnSchema = columnSchemas.FirstOrDefault
                        (
                            c => c.Name.ToLower() == fieldName.ToLower()
                        );

                        if (columnSchema == null)
                        {
                            var message = "The field name '{0}' was not expected.";

                            throw new InvalidOperationException
                            (
                                String.Format
                                (
                                    message,
                                    fieldName
                                )
                            );
                        }

                        cells.Add
                        (
                            new QueryCell
                            (
                                columnSchema,
                                fieldValue
                            )
                        );
                    }
                }
            }

            return rows;
        }
    }
}
