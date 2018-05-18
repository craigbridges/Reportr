namespace Reportr.Data
{
    using System;
    using System.Data;

    /// <summary>
    /// Defines a contract for a report data source
    /// </summary>
    public interface IDataSource : IDisposable
    {
        /// <summary>
        /// Gets the unique ID of the data source
        /// </summary>
        Guid SourceId { get; }

        /// <summary>
        /// Gets the name of the data source
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets the connection used by the data source
        /// </summary>
        IDbConnection Connection { get; }

        /// <summary>
        /// Gets an array of the tables held by the data source
        /// </summary>
        DataTableSchema[] Schema { get; }
    }
}
