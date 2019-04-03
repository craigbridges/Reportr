namespace Reportr.Data.Sql
{
    using CodeChange.Toolkit.Culture;
    using System;
    using System.Data.SqlClient;
    
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
        /// Constructs the data source with locale configuration
        /// </summary>
        /// <param name="name">The name of the data source</param>
        /// <param name="connectionString">The connection string</param>
        /// <param name="localeConfiguration">The locale configuration</param>
        public SqlDataSource
            (
                string name,
                string connectionString,
                ILocaleConfiguration localeConfiguration
            )
            : base(name, localeConfiguration)
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
                    var connectionString = this.ConnectionString;

                    try
                    {
                        using (var connection = new SqlConnection(connectionString))
                        {
                            _schema = GenerateSchema(connection);
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
    }
}
