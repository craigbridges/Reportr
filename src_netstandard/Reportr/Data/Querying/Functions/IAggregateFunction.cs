namespace Reportr.Data.Querying.Functions
{
    using Reportr.Filtering;
    using System.Threading.Tasks;

    /// <summary>
    /// Defines a contract for an aggregate function
    /// </summary>
    /// <remarks>
    /// An aggregate function is a mathematical computation involving a 
    /// set of values rather than a single value.
    /// 
    /// The calculation performed by an aggregate function returns 
    /// a single value from multiple values returned in query rows.
    /// </remarks>
    public interface IAggregateFunction
    {
        /// <summary>
        /// Gets the query field binding for the operation
        /// </summary>
        DataBinding Binding { get; }

        /// <summary>
        /// Executes the aggregate function and computes the result
        /// </summary>
        /// <param name="query">The query to execute</param>
        /// <param name="parameters">The parameter values</param>
        /// <returns>The result computed</returns>
        double Execute
        (
            IQuery query,
            params ParameterValue[] parameters
        );

        /// <summary>
        /// Asynchronously executes the aggregate query and computes the result
        /// </summary>
        /// <param name="query">The query to execute</param>
        /// <param name="parameters">The parameter values</param>
        /// <returns>The result computed</returns>
        Task<double> ExecuteAsync
        (
            IQuery query,
            params ParameterValue[] parameters
        );

        /// <summary>
        /// Executes the aggregate function and computes the result
        /// </summary>
        /// <param name="rows">The rows to perform the computation on</param>
        /// <returns>The result computed</returns>
        double Execute
        (
            params QueryRow[] rows
        );
    }
}
