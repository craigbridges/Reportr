namespace Reportr.Data.Querying
{
    using System.Collections.Generic;
    
    /// <summary>
    /// Represents various extension methods for queries
    /// </summary>
    public static class QueryExtensions
    {
        /// <summary>
        /// Compiles default parameter values for a query
        /// </summary>
        /// <param name="query">The query</param>
        /// <returns>An array of parameter values</returns>
        public static ParameterValue[] CompileDefaultParameters
            (
                this IQuery query
            )
        {
            Validate.IsNotNull(query);

            var values = new List<ParameterValue>();

            foreach (var info in query.Parameters)
            {
                var defaultValue = info.DefaultValue;

                if (defaultValue != null)
                {
                    values.Add
                    (
                        new ParameterValue
                        (
                            info,
                            defaultValue
                        )
                    );
                }
            }

            return values.ToArray();
        }
    }
}
