namespace Reportr.Data.Querying.Functions
{
    using Reportr.Filtering;
    using System.Threading.Tasks;

    /// <summary>
    /// Defines a contract for a query aggregate function
    /// </summary>
    /// <remarks>
    /// An aggregate function is a mathematical computation involving a 
    /// set of values rather than a single value.
    /// 
    /// The calculation performed by a query aggregate function returns 
    /// a single value from multiple values returned in the query results.
    /// </remarks>
    public interface IQueryAggregateFunction
    {
        /// <summary>
        /// Gets the query to execute
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
