namespace Reportr.Querying
{
    using System.Collections.Generic;

    /// <summary>
    /// Defines a contract for a single query
    /// </summary>
    public interface IQuery
    {
        /// <summary>
        /// Gets the name of the query
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets an array of parameters accepted by the query
        /// </summary>
        ParameterInfo[] Parameters { get; }

        /// <summary>
        /// Executes the query using the parameter values supplied
        /// </summary>
        /// <param name="parameterValues">The parameter values</param>
        /// <returns>The query result</returns>
        QueryResult Execute
        (
            Dictionary<string, object> parameterValues
        );
    }
}
