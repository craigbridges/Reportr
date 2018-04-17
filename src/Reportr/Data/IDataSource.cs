namespace Reportr.Data
{
    /// <summary>
    /// Defines a contract for a report data source
    /// </summary>
    public interface IDataSource
    {
        /// <summary>
        /// Gets the name of the data source
        /// </summary>
        string Name { get; }
        
        /// <summary>
        /// Gets the connection string used by the data source
        /// </summary>
        string ConnectionString { get; }

        /// <summary>
        /// Gets an array of the tables held by the data source
        /// </summary>
        DataTableSchema[] Schema { get; }
    }
}
