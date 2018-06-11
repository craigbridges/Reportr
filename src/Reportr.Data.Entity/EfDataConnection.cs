namespace Reportr.Data.Entity
{
    using System.Data.Entity;

    /// <summary>
    /// Represents an Entity Framework data connection implementation
    /// </summary>
    public class EfDataConnection : IDataConnection
    {
        /// <summary>
        /// Constructs the connection with a database context
        /// </summary>
        /// <param name="context">The database context</param>
        public EfDataConnection
            (
                DbContext context
            )
        {
            Validate.IsNotNull(context);

            var dbConnection = context.Database.Connection;

            this.DatabaseName = dbConnection.Database;
            this.ConnectionString = dbConnection.ConnectionString;
            this.Context = context;
        }

        /// <summary>
        /// Gets the name of the database
        /// </summary>
        public string DatabaseName { get; private set; }

        /// <summary>
        /// Gets the connection string
        /// </summary>
        public string ConnectionString { get; private set; }

        /// <summary>
        /// Gets the associated EF database context
        /// </summary>
        public DbContext Context { get; private set; }

        /// <summary>
        /// Disposes the database connection
        /// </summary>
        public void Dispose()
        {
            this.Context.Dispose();
        }
    }
}
