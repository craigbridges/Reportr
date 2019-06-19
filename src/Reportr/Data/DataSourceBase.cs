namespace Reportr.Data
{
    using CodeChange.Toolkit.Culture;
    using DatabaseSchemaReader;
    using System;
    using System.Collections.Generic;
    using System.Data.Common;
    using System.Linq;

    /// <summary>
    /// Represents a base class for data source implementations
    /// </summary>
    public abstract class DataSourceBase : IDataSource
    {
        private bool _disposeCalled = false;

        /// <summary>
        /// Constructs the data source with default values
        /// </summary>
        /// <param name="name">The name of the data source</param>
        public DataSourceBase
            (
                string name
            )
        {
            Validate.IsNotEmpty(name);

            this.SourceId = Guid.NewGuid();
            this.Name = name;
            this.LocaleConfiguration = new DefaultLocaleConfiguration();
        }

        /// <summary>
        /// Constructs the data source with locale configuration
        /// </summary>
        /// <param name="name">The name of the data source</param>
        /// <param name="localeConfiguration">The locale configuration</param>
        public DataSourceBase
            (
                string name,
                ILocaleConfiguration localeConfiguration
            )
            : this(name)
        {
            Validate.IsNotNull(localeConfiguration);

            this.LocaleConfiguration = localeConfiguration;
        }

        /// <summary>
        /// Destructs the data source by disposing resources
        /// </summary>
        ~DataSourceBase()
        {
            Dispose(false);
        }

        /// <summary>
        /// Gets the unique ID of the data source
        /// </summary>
        public Guid SourceId { get; private set; }

        /// <summary>
        /// Gets the name of the data source
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Gets the locale configuration for the culture
        /// </summary>
        public ILocaleConfiguration LocaleConfiguration { get; private set; }

        /// <summary>
        /// Gets an array of the tables held by the data source
        /// </summary>
        public abstract DataTableSchema[] Schema { get; }

        /// <summary>
        /// Generates the table schema using a database connection
        /// </summary>
        /// <param name="connection">The database connection</param>
        /// <returns>The schema generated</returns>
        protected virtual DataTableSchema[] GenerateSchema
            (
                DbConnection connection
            )
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
                        if (false == String.IsNullOrEmpty(fk.RefersToTable))
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
                    }

                    tableSchema = tableSchema.WithForeignKeys
                    (
                        foreignKeySchemas.ToArray()
                    );
                }

                tableSchemas.Add(tableSchema);
            }

            this.DateSchemaResolved = DateTime.UtcNow;

            return tableSchemas.ToArray();
        }

        /// <summary>
        /// Gets the date and time the schema was resolved
        /// </summary>
        public DateTime? DateSchemaResolved { get; protected set; }

        /// <summary>
        /// Gets a flag indicating if the data schema is unresolvable
        /// </summary>
        public bool SchemaUnresolvable { get; protected set; }

        /// <summary>
        /// Gets the error message generated while trying to resolve the schema
        /// </summary>
        public string SchemaGenerationErrorMessage { get; protected set; }

        /// <summary>
        /// Marks the data source schema as unresolvable with the error
        /// </summary>
        /// <param name="errorMessage">The error message</param>
        protected void MarkSchemaAsUnresolvable
            (
                string errorMessage
            )
        {
            this.DateSchemaResolved = DateTime.UtcNow;
            this.SchemaUnresolvable = true;
            this.SchemaGenerationErrorMessage = errorMessage;
        }

        /// <summary>
        /// Gets a table from the database schema
        /// </summary>
        /// <param name="name">The table name</param>
        /// <returns>The table schema</returns>
        public DataTableSchema GetSchemaTable
            (
                string name
            )
        {
            Validate.IsNotEmpty(name);

            var table = this.Schema.FirstOrDefault
            (
                dts => dts.Name.Equals(name, StringComparison.OrdinalIgnoreCase)
            );

            if (table == null)
            {
                throw new KeyNotFoundException
                (
                    $"The data source does not contain a table named '{name}'."
                );
            }

            return table;
        }

        /// <summary>
        /// Provides a custom description of the data source
        /// </summary>
        /// <returns>The data source name</returns>
        public override string ToString()
        {
            return this.Name;
        }

        /// <summary>
        /// Disposes the data source by freeing up managed and unmanaged objects
        /// </summary>
        /// <param name="disposing">True, if the data source is disposing</param>
        protected virtual void Dispose
            (
                bool disposing
            )
        {
            if (false == _disposeCalled)
            {
                if (disposing)
                {
                    DisposeManagedObjects();
                }

                DisposeUnanagedObjects();

                _disposeCalled = true;
            }
        }

        /// <summary>
        /// Frees up any managed objects used by the data source
        /// </summary>
        protected virtual void DisposeManagedObjects()
        {
            // NOTE: nothing to free up
        }

        /// <summary>
        /// Frees up any unmanaged objects used by the data source
        /// </summary>
        protected virtual void DisposeUnanagedObjects()
        {
            // NOTE: nothing to free up
        }

        /// <summary>
        /// Performs application-defined tasks associated with 
        /// freeing, releasing, or resetting unmanaged resources
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
        }
    }
}
