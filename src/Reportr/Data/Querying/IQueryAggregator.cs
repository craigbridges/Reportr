namespace Reportr.Data.Querying
{
    using System.Threading.Tasks;

    /// <summary>
    /// Defines a contract for a query aggregator
    /// </summary>
    /// <remarks>
    /// A query aggregator computes a single value from the results of a query.
    /// </remarks>
    public interface IQueryAggregator
    {
        /// <summary>
        /// Gets the query to aggregate
        /// </summary>
        IQuery Query { get; }

        /// <summary>
        /// Gets the query field binding for the operation
        /// </summary>
        DataBinding Binding { get; }

        /// <summary>
        /// Executes the aggregate query and computes the result
        /// </summary>
        /// <param name="parameters">The parameter values</param>
        /// <returns>The result computed</returns>
        double Execute
        (
            params ParameterValue[] parameters
        );

        /// <summary>
        /// Asynchronously executes the aggregate query and computes the result
        /// </summary>
        /// <param name="parameters">The parameter values</param>
        /// <returns>The result computed</returns>
        Task<double> ExecuteAsync
        (
            params ParameterValue[] parameters
        );
    }
}
