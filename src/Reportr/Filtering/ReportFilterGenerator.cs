namespace Reportr.Filtering
{
    using Reportr.Data;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Represents the default report filter generator implementation
    /// </summary>
    public class ReportFilterGenerator : IReportFilterGenerator
    {
        /// <summary>
        /// Generates a default filter for a report definition
        /// </summary>
        /// <param name="definition">The report definition</param>
        /// <returns>The report filter</returns>
        public ReportFilter Generate
            (
                ReportDefinition definition
            )
        {
            Validate.IsNotNull(definition);

            var filter = new ReportFilter
            (
                definition.Parameters.ToArray()
            );

            return filter;
        }

        /// <summary>
        /// Generates a filter for a report and parameter values
        /// </summary>
        /// <param name="definition">The report definition</param>
        /// <param name="parameterValues">The parameter values</param>
        /// <returns>The report filter</returns>
        public ReportFilter Generate
            (
                ReportDefinition definition,
                IDictionary<string, object> parameterValues
            )
        {
            Validate.IsNotNull(definition);
            Validate.IsNotNull(parameterValues);

            var filter = Generate(definition);

            filter.SetParameterValues
            (
                parameterValues
            );

            return filter;
        }

        /// <summary>
        /// Generates a filter for a report, parameter values and sorting rules
        /// </summary>
        /// <param name="definition">The report definition</param>
        /// <param name="parameterValues">The parameter values</param>
        /// <param name="sortingRules">The sorting rules</param>
        /// <returns>The report filter</returns>
        /// <remarks>
        /// The sorting rules consist of a dictionary with the 
        /// component name that contains a key value pair referencing 
        /// a column name with a sort direction.
        /// </remarks>
        public ReportFilter Generate
            (
                ReportDefinition definition,
                IDictionary<string, object> parameterValues,
                IDictionary<string, KeyValuePair<string, SortDirection>> sortingRules
            )
        {
            Validate.IsNotNull(definition);
            Validate.IsNotNull(parameterValues);
            Validate.IsNotNull(sortingRules);

            var filter = Generate
            (
                definition,
                parameterValues
            );

            foreach (var item in sortingRules)
            {
                var componentName = item.Key;
                var columnName = item.Value.Key;
                var direction = item.Value.Value;

                var sectionType = GetSectionType
                (
                    definition,
                    componentName
                );
                
                filter.SetSortingRule
                (
                    sectionType,
                    componentName,
                    columnName,
                    direction
                );
            }

            return filter;
        }

        /// <summary>
        /// Gets the section type for a report component
        /// </summary>
        /// <param name="definition">The report definition</param>
        /// <param name="componentName">The component name</param>
        /// <returns>The section type</returns>
        private ReportSectionType GetSectionType
            (
                ReportDefinition definition,
                string componentName
            )
        {
            var allSectionTypes = Enum.GetValues
            (
                typeof(ReportSectionType)
            );

            foreach (ReportSectionType sectionType in allSectionTypes)
            {
                var component = definition.FindComponent
                (
                    sectionType,
                    componentName
                );

                if (component != null)
                {
                    return sectionType;
                }
            }

            return ReportSectionType.ReportBody;
        }
    }
}
