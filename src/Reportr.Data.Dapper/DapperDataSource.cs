namespace Reportr.Data.Dapper
{
    using CodeChange.Toolkit.Culture;
    using System;
    using System.Data;
    using System.Data.Common;

    /// <summary>
    /// Represents a Dapper data source implementation
    /// </summary>
    public class DapperDataSource : DataSourceBase
    {
        private DataTableSchema[] _schema;

        /// <summary>
        /// Constructs the data source with a database connection
        /// </summary>
        /// <param name="connection">The database connection</param>
        public DapperDataSource
            (
                IDbConnection connection
            )
            : base(connection.Database)
        {
            Validate.IsNotNull(connection);

            this.Connection = connection;
        }

        /// <summary>
        /// Constructs the data source with locale configuration
        /// </summary>
        /// <param name="name">The name of the data source</param>
        /// <param name="localeConfiguration">The locale configuration</param>
        public DapperDataSource
            (
                IDbConnection connection,
                ILocaleConfiguration localeConfiguration
            )
            : base(connection.Database, localeConfiguration)
        {
            Validate.IsNotNull(connection);

            this.Connection = connection;
        }

        /// <summary>
        /// Gets the database connection assigned to the data source
        /// </summary>
        public IDbConnection Connection { get; private set; }

        /// <summary>
        /// Gets an array of the tables held by the data source
        /// </summary>
        public override DataTableSchema[] Schema
        {
            get
            {
                if (_schema == null)
                {
                    try
                    {
                        var connection = this.Connection;

                        if (connection is DbConnection)
                        {
                            _schema = GenerateSchema
                            (
                                (DbConnection)connection
                            );
                        }
                        else
                        {
                            var type = connection.GetType();

                            MarkSchemaAsUnresolvable
                            (
                                $"The connection type {type} is not supported."
                            );
                        }
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

                return _schema;
            }
        }
        
        /// <summary>
        /// Disposes the database connection
        /// </summary>
        protected override void DisposeManagedObjects()
        {
            this.Connection.Dispose();
        }
    }
}
