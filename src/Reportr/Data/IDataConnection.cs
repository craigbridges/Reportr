namespace Reportr.Data
{
    using System;
    
    /// <summary>
    /// Defines a contract for a single data connection
    /// </summary>
    public interface IDataConnection : IDisposable
    {
        /// <summary>
        /// Gets the name of the database
        /// </summary>
        string DatabaseName { get; }

        /// <summary>
        /// Gets the connection string
        /// </summary>
        string ConnectionString { get; }
    }
}
