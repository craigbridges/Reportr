namespace Reportr.Data.Entity
{
    using CodeChange.Toolkit.Culture;
    using System.Collections.Generic;
    using System.Data.Entity;
    using EntityFramework.Metadata.Extensions;
    using EntityFramework.Metadata;
    using System;

    /// <summary>
    /// Represents an Entity Framework data source implementation
    /// </summary>
    public class EfDataSource : DataSourceBase
    {
        private DataTableSchema[] _schema;

        /// <summary>
        /// Constructs the data source with a database context
        /// </summary>
        /// <param name="context">The database context</param>
        public EfDataSource
            (
                DbContext context
            )
            : base(context.Database.Connection.Database)
        {
            Validate.IsNotNull(context);

            this.Context = context;
        }

        /// <summary>
        /// Constructs the data source with locale configuration
        /// </summary>
        /// <param name="name">The name of the data source</param>
        /// <param name="localeConfiguration">The locale configuration</param>
        public EfDataSource
            (
                DbContext context,
                ILocaleConfiguration localeConfiguration
            )
            : base(context.Database.Connection.Database, localeConfiguration)
        {
            Validate.IsNotNull(context);

            this.Context = context;
        }

        /// <summary>
        /// Gets the database context assigned to the data source
        /// </summary>
        public DbContext Context { get; private set; }

        /// <summary>
        /// Gets an array of the tables held by the data source
        /// </summary>
        public override DataTableSchema[] Schema
        {
            get
            {
                if (_schema == null)
                {
                    PopulateSchema
                    (
                        this.Context
                    );
                }

                return _schema;
            }
        }

        /// <summary>
        /// Populates the table schema using a database context
        /// </summary>
        /// <param name="context">The database context</param>
        private void PopulateSchema
            (
                DbContext context
            )
        {
            try
            {
                var entityMaps = context.Db();
                var tableSchemas = new List<DataTableSchema>();

                foreach (var map in entityMaps)
                {
                    var columnSchemas = MapTableProperties(map);

                    var table = new DataTableSchema
                    (
                        map.TableName,
                        columnSchemas
                    );

                    // Set the primary key details
                    if (map.Pks != null)
                    {
                        var pkColumns = new List<DataColumnSchema>();

                        foreach (var property in map.Pks)
                        {
                            var column = new DataColumnSchema
                            (
                                property.ColumnName,
                                property.Type
                            );

                            pkColumns.Add(column);
                        }

                        table = table.WithPrimaryKey
                        (
                            pkColumns.ToArray()
                        );
                    }

                    // Set the foreign key details
                    if (map.Fks != null)
                    {
                        var foreignKeys = new List<DataForeignKey>();

                        foreach (var property in map.Fks)
                        {
                            var referencedProperty = property.FkTargetColumn;
                            var fkMap = referencedProperty.EntityMap;
                            var fkTableColumns = MapTableProperties(map);

                            var key = new DataForeignKey
                            (
                                new DataColumnSchema[]
                                {
                                new DataColumnSchema
                                (
                                    property.ColumnName,
                                    property.Type
                                )
                                },
                                new DataTableSchema
                                (
                                    fkMap.TableName,
                                    fkTableColumns
                                ),
                                new DataColumnSchema[]
                                {
                                new DataColumnSchema
                                (
                                    referencedProperty.ColumnName,
                                    referencedProperty.Type
                                )
                                }
                            );

                            foreignKeys.Add(key);
                        }

                        table = table.WithForeignKeys
                        (
                            foreignKeys.ToArray()
                        );
                    }

                    tableSchemas.Add(table);
                }

                _schema = tableSchemas.ToArray();

                this.DateSchemaResolved = DateTime.UtcNow;
            }
            catch (Exception ex)
            {
                _schema = new DataTableSchema[] { };

                MarkSchemaAsUnresolvable
                (
                    ex.Message
                );
            }
        }

        /// <summary>
        /// Maps an entities mapping properties to column schema's
        /// </summary>
        /// <param name="map">The entity map</param>
        /// <returns>An array of column schema's</returns>
        private DataColumnSchema[] MapTableProperties
            (
                IEntityMap map
            )
        {
            var columnSchemas = new List<DataColumnSchema>();

            foreach (var property in map.Properties)
            {
                var column = new DataColumnSchema
                (
                    property.ColumnName,
                    property.Type
                );

                columnSchemas.Add(column);
            }

            return columnSchemas.ToArray();
        }

        /// <summary>
        /// Disposes the database connection
        /// </summary>
        protected override void DisposeManagedObjects()
        {
            this.Context.Dispose();
        }
    }
}
