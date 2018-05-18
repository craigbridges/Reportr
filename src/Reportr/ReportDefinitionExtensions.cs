﻿namespace Reportr
{
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Provides various extension methods for report definitions
    /// </summary>
    public static class ReportDefinitionExtensions
    {
        /// <summary>
        /// Generates a default filter for the report definition
        /// </summary>
        /// <param name="report">The report definition</param>
        /// <returns>The filter generated</returns>
        public static ReportFilter GenerateDefaultFilter
            (
                this ReportDefinition report
            )
        {
            Validate.IsNotNull(report);

            var filter = new ReportFilter();
            var queryGroups = report.AggregateQueries();

            foreach (var group in queryGroups)
            {
                var parameterValues = new List<ParameterValue>();

                foreach (var query in group.Value)
                {
                    foreach (var parameter in query.Parameters)
                    {
                        var valueFound = parameterValues.Any
                        (
                            pv => pv.Name.ToLower() == parameter.Name.ToLower()
                        );

                        if (false == valueFound)
                        {
                            parameterValues.Add
                            (
                                new ParameterValue
                                (
                                    parameter,
                                    parameter.DefaultValue
                                )
                            );
                        }
                    }
                }

                filter.ParameterValues.Add
                (
                    group.Key,
                    parameterValues.ToArray()
                );
            }

            return filter;
        }
    }
}
