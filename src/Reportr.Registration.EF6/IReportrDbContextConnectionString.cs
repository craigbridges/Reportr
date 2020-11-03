namespace Reportr.Registration.Entity
{
    /// <summary>
    /// Defines a contract that defines a connection string for the Reportr DbContext
    /// </summary>
    public interface IReportrDbContextConnectionString
    {
        /// <summary>
        /// Gets the name of the configuration entry or the connection string value
        /// </summary>
        string NameOrConnectionString { get; }
    }
}
