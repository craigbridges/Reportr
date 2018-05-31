namespace Reportr.Filtering
{
    using Reportr.Data.Querying;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;

    /// <summary>
    /// Represents a single report filter
    /// </summary>
    /// <remarks>
    /// A report filter is used to filter the data in one or more
    /// sections within the report. This includes sorting columns
    /// within queries that have been defined in sections.
    /// </remarks>
    public class ReportFilter
    {
        /// <summary>
        /// Constructs an empty report filter
        /// </summary>
        public ReportFilter()
        {
            this.ParameterValues = new Collection<ReportFilterParameterValue>();
            this.SortingRules = new Collection<ReportFilterSortingRule>();
        }

        /// <summary>
        /// Gets a collection of parameter values for the filter
        /// </summary>
        public ICollection<ReportFilterParameterValue> ParameterValues
        {
            get;
            private set;
        }
        
        /// <summary>
        /// Gets parameter values for a section type
        /// </summary>
        /// <param name="sectionType">The report section type</param>
        /// <returns>A collection of parameter values</returns>
        public IEnumerable<ReportFilterParameterValue> GetParameters
            (
                ReportSectionType sectionType
            )
        {
            return this.ParameterValues.Where
            (
                value => value.SectionType == sectionType
            );
        }

        /// <summary>
        /// Gets parameter values for a section type and query
        /// </summary>
        /// <param name="sectionType">The report section type</param>
        /// <param name="query">The query</param>
        /// <param name="defaultValues">The default values</param>
        /// <returns>A collection of parameter values</returns>
        public IEnumerable<ReportFilterParameterValue> GetParameters
            (
                ReportSectionType sectionType,
                IQuery query,
                params ParameterValue[] defaultValues
            )
        {
            Validate.IsNotNull(query);
            
            var sectionParameters = GetParameters
            (
                sectionType
            );

            var queryParameters = new List<ReportFilterParameterValue>();

            foreach (var parameterInfo in query.Parameters)
            {
                var matchingParameter = sectionParameters.FirstOrDefault
                (
                    pv => pv.Parameter == parameterInfo
                );

                if (matchingParameter != null)
                {
                    queryParameters.Add(matchingParameter);
                }
                else if (defaultValues != null)
                {
                    // Try and find a matching parameter from the default values

                    matchingParameter = sectionParameters.FirstOrDefault
                    (
                        pv => pv.Parameter == parameterInfo
                    );

                    if (matchingParameter != null)
                    {
                        queryParameters.Add(matchingParameter);
                    }
                }
            }

            return queryParameters;
        }

        /// <summary>
        /// Gets a dictionary of query sorting rules by query
        /// </summary>
        public ICollection<ReportFilterSortingRule> SortingRules
        {
            get;
            private set;
        }
        
        /// <summary>
        /// Gets sorting rules for a section type
        /// </summary>
        /// <param name="sectionType">The report section type</param>
        /// <param name="componentName">The component name</param>
        /// <returns>A collection of sorting rules</returns>
        public IEnumerable<ReportFilterSortingRule> GetSortingRules
            (
                ReportSectionType sectionType,
                string componentName
            )
        {
            return this.SortingRules.Where
            (
                rule => rule.SectionType == sectionType
                    && rule.ComponentName.ToLower() == componentName.ToLower()
            );
        }
    }
}
