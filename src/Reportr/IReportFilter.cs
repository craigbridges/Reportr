namespace Reportr
{
    using Reportr.Components.Querying;
    using System.Collections.Generic;

    /// <summary>
    /// Defines a contract for a report filter
    /// </summary>
    /// <remarks>
    /// A report filter is used to filter the data in one or more
    /// sections within the report. This includes sorting columns
    /// within queries that have been defined in sections.
    /// </remarks>
    public interface IReportFilter
    {
        /// <summary>
        /// Gets a dictionary of parameter values for each section
        /// </summary>
        /// <remarks>
        /// The dictionary key is used to identify the name of the
        /// report section. Each section can have one or more
        /// parameter values assigned to it.
        /// 
        /// Only one value is allowed per parameter; if more than
        /// one parameter value is specified against a parameter,
        /// the last value found will be used.
        /// </remarks>
        Dictionary<string, ParameterValue[]> ParameterValues { get; }

        /// <summary>
        /// Sets the parameter values against a report section
        /// </summary>
        /// <param name="sectionName">The section name</param>
        /// <param name="values">The parameter values</param>
        void SetParameters
        (
            string sectionName,
            params ParameterValue[] values
        );

        /// <summary>
        /// Gets a dictionary of query sorting rules by query
        /// </summary>
        /// <remarks>
        /// The dictionary key is used to identity the name of the
        /// query. The sorting rules are applied to each query, 
        /// one at a time. If a sorting rule targets the same column
        /// twice, each new rule will override the previous.
        /// </remarks>
        Dictionary<string, QuerySortingRule[]> QuerySortingRules { get; }

        /// <summary>
        /// Sets the sorting rules for a specified query
        /// </summary>
        /// <param name="queryName">The query name</param>
        /// <param name="rules">The sorting rules</param>
        void SetSortingRules
        (
            string queryName,
            params QuerySortingRule[] rules
        );
    }
}
