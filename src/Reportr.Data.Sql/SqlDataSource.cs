namespace Reportr.Data.Sql
{
    using DatabaseSchemaReader;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using System.Linq;

    /// <summary>
    /// Represents an SQL data source implementation
    /// </summary>
    public class SqlDataSource : DataSourceBase
    {
        private DataTableSchema[] _schema;

        /// <summary>
        /// Constructs the data source with a database context
        /// </summary>
        /// <param name="name">The name of the data source</param>
        /// <param name="connectionString">The connection string</param>
        public SqlDataSource
            (
                string name,
                string connectionString
            )
            : base(name)
        {
            Validate.IsNotEmpty(connectionString);

            this.ConnectionString = connectionString;

            _schema = null;
        }

        /// <summary>
        /// Gets the database connection string
        /// </summary>
        public string ConnectionString { get; private set; }

        /// <summary>
        /// Gets an array of the tables held by the data source
        /// </summary>
        public override DataTableSchema[] Schema
        {
            get
            {
                if (_schema == null)
                {
                    _schema = GenerateSchema();
                }

                return _schema;
            }
        }

        /// <summary>
        /// Generates the table schema using a database connection string
        /// </summary>
        /// <returns>The schema generated</returns>
        private DataTableSchema[] GenerateSchema()
        {
            var connectionString = this.ConnectionString;

            using (var connection = new SqlConnection(connectionString))
            {
                var reader = new DatabaseReader(connection);
                var schema = reader.ReadAll();
                var tableSchemas = new List<DataTableSchema>();

                foreach (var dbTable in schema.Tables)
                {
                    var columnSchemas = new List<DataColumnSchema>();

                    foreach (var dbColumn in dbTable.Columns)
                    {
                        columnSchemas.Add
                        (
                            new DataColumnSchema
                            (
                                dbColumn.Name,
                                dbColumn.DataType.GetNetType()
                            )
                        );
                    }

                    var tableSchema = new DataTableSchema
                    (
                        dbTable.Name,
                        columnSchemas.ToArray()
                    );

                    // Add the primary keys if there are any in the table
                    if (dbTable.PrimaryKey.Columns.Count > 0)
                    {
                        var primaryKeyColumns = new List<DataColumnSchema>();

                        foreach (var columnName in dbTable.PrimaryKey.Columns)
                        {
                            var dbColumn = dbTable.FindColumn
                            (
                                columnName
                            );

                            primaryKeyColumns.Add
                            (
                                new DataColumnSchema
                                (
                                    dbColumn.Name,
                                    dbColumn.DataType.GetNetType()
                                )
                            );
                        }

                        tableSchema = tableSchema.WithPrimaryKey
                        (
                            primaryKeyColumns.ToArray()
                        );
                    }

                    // Add the foreign keys if there are any in the table
                    if (dbTable.ForeignKeys.Any())
                    {
                        var foreignKeySchemas = new List<DataForeignKey>();

                        foreach (var fk in dbTable.ForeignKeys)
                        {
                            var fkColumns = new List<DataColumnSchema>();
                            var refPrimaryColumns = new List<DataColumnSchema>();
                            var refAllColumns = new List<DataColumnSchema>();

                            // Build a list of the foreign key columns
                            foreach (var columnName in fk.Columns)
                            {
                                var dbColumn = dbTable.FindColumn
                                (
                                    columnName
                                );

                                fkColumns.Add
                                (
                                    new DataColumnSchema
                                    (
                                        dbColumn.Name,
                                        dbColumn.DataType.GetNetType()
                                    )
                                );
                            }

                            var referencedTable = fk.ReferencedTable(schema);

                            // Build a list of the referenced primary columns
                            foreach (var columnName in referencedTable.PrimaryKey.Columns)
                            {
                                var dbColumn = dbTable.FindColumn
                                (
                                    columnName
                                );

                                refPrimaryColumns.Add
                                (
                                    new DataColumnSchema
                                    (
                                        dbColumn.Name,
                                        dbColumn.DataType.GetNetType()
                                    )
                                );
                            }

                            // Build a list of the referenced table columns
                            foreach (var dbColumn in referencedTable.Columns)
                            {
                                refAllColumns.Add
                                (
                                    new DataColumnSchema
                                    (
                                        dbColumn.Name,
                                        dbColumn.DataType.GetNetType()
                                    )
                                );
                            }

                            var refTableSchema = new DataTableSchema
                            (
                                fk.RefersToTable,
                                refAllColumns.ToArray()
                            );

                            var fkSchema = new DataForeignKey
                            (
                                fkColumns.ToArray(),
                                refTableSchema,
                                refPrimaryColumns.ToArray()
                            );
                        }

                        tableSchema = tableSchema.WithForeignKeys
                        (
                            foreignKeySchemas.ToArray()
                        );
                    }
                    
                    tableSchemas.Add(tableSchema);
                }

                return tableSchemas.ToArray();
            }
        }
    }
}
