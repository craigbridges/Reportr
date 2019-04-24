namespace Reportr.Components.Collections
{
    using Nito.AsyncEx.Synchronous;
    using Reportr.Data.Querying;
    using Reportr.Filtering;
    using System.Linq;
    using System.Threading.Tasks;

    /// <summary>
    /// Represents various query extensions for collection components
    /// </summary>
    public static class QueryExtensions
    {
        /// <summary>
        /// Executes a query against a report filter
        /// </summary>
        /// <param name="query">The query to execute</param>
        /// <param name="filter">The report filter</param>
        /// <param name="defaultParameters">The default parameter values</param>
        /// <returns>The results of the query</returns>
        public static QueryResults Execute
            (
                this IQuery query,
                ReportFilter filter,
                params ParameterValue[] defaultParameters
            )
        {
            var task = ExecuteAsync
            (
                query,
                filter,
                defaultParameters
            );

            return task.WaitAndUnwrapException();
        }

        /// <summary>
        /// Asynchronously executes a query against a report filter
        /// </summary>
        /// <param name="query">The query to execute</param>
        /// <param name="filter">The report filter</param>
        /// <param name="defaultParameters">The default parameter values</param>
        /// <returns>The results of the query</returns>
        public static async Task<QueryResults> ExecuteAsync
            (
                this IQuery query,
                ReportFilter filter,
                params ParameterValue[] defaultParameters
            )
        {
            var parameters = filter.GetQueryParameters
            (
                query,
                defaultParameters.ToArray()
            );

            var queryTask = query.ExecuteAsync
            (
                parameters.ToArray()
            );

            return await queryTask.ConfigureAwait
            (
                false
            );
        }
    }
}
