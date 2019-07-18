namespace Reportr.Data.Sql
{
    using CodeChange.Toolkit.Culture;
    using Reportr.Globalization;
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
                ILocaleConfiguration localeConfiguration,
                params QueryColumnInfo[] queryColumns
            )
        {
            Validate.IsNotNull(localeConfiguration);

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

                        var transformer = CulturalTransformerFactory.GetInstance
                        (
                            fieldValue
                        );

                        fieldValue = transformer.Transform
                        (
                            fieldValue,
                            localeConfiguration
                        );

                        var columnSchema = columnSchemas.FirstOrDefault
                        (
                            c => c.Name.Equals(fieldName, StringComparison.OrdinalIgnoreCase)
                        );

                        if (columnSchema == null)
                        {
                            throw new InvalidOperationException
                            (
                                $"The field name '{fieldName}' was not expected."
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
