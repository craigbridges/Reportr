namespace Reportr.Data.Querying
{
    using Reportr.Filtering;
    using System.Collections.Generic;
    using System.Linq;

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
                // Add the default lookup item filter values
                if (info.HasLookup && info.LookupFilterParameters.Any())
                {
                    var lookupValues = new List<ParameterValue>();

                    foreach (var filterInfo in info.LookupFilterParameters)
                    {
                        lookupValues.Add
                        (
                            new ParameterValue
                            (
                                filterInfo,
                                filterInfo.DefaultValue
                            )
                        );
                    }

                    values.Add
                    (
                        new ParameterValue
                        (
                            info,
                            info.DefaultValue,
                            lookupValues.ToArray()
                        )
                    );
                }
                else
                {
                    values.Add
                    (
                        new ParameterValue
                        (
                            info,
                            info.DefaultValue
                        )
                    );
                }
            }

            return values.ToArray();
        }
    }
}
