namespace Reportr.Data
{
    using Reportr.Data.Querying;
    using System;
    using System.Threading.Tasks;

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
        /// Gets the connection string used by the data source
        /// </summary>
        string ConnectionString { get; }

        /// <summary>
        /// Gets an array of the tables held by the data source
        /// </summary>
        DataTableSchema[] Schema { get; }

        /// <summary>
        /// Executes a query using the parameter values supplied
        /// </summary>
        /// <param name="query">The query</param>
        /// <param name="parameterValues">The parameter values</param>
        /// <returns>The query results</returns>
        QueryResults ExecuteQuery
        (
            IQuery query,
            params ParameterValue[] parameterValues
        );

        /// <summary>
        /// Asynchronously executes a query using the parameter values supplied
        /// </summary>
        /// <param name="query">The query</param>
        /// <param name="parameterValues">The parameter values</param>
        /// <returns>The query results</returns>
        Task<QueryResults> ExecuteQueryAsync
        (
            IQuery query,
            params ParameterValue[] parameterValues
        );
    }
}
