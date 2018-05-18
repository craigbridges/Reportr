namespace Reportr
{
    using Reportr.Data.Querying;
    using System.Collections.Generic;

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
            this.ParameterValues = new Dictionary<ReportSectionType, ParameterValue[]>();
            this.QuerySortingRules = new Dictionary<string, QuerySortingRule[]>();
        }

        /// <summary>
        /// Gets a dictionary of parameter values for each section
        /// </summary>
        /// <remarks>
        /// The dictionary key is used to identify the type of
        /// report section. Each section can have one or more
        /// parameter values assigned to it.
        /// 
        /// Only one value is allowed per parameter; if more than
        /// one parameter value is specified against a parameter,
        /// the last value found will be used.
        /// </remarks>
        public Dictionary<ReportSectionType, ParameterValue[]> ParameterValues
        {
            get;
            private set;
        }
        
        /// <summary>
        /// Gets a dictionary of query sorting rules by query
        /// </summary>
        /// <remarks>
        /// The dictionary key is used to identity the name of the
        /// query. The sorting rules are applied to each query, 
        /// one at a time. If a sorting rule targets the same column
        /// twice, each new rule will override the previous.
        /// </remarks>
        public Dictionary<string, QuerySortingRule[]> QuerySortingRules
        {
            get;
            private set;
        }
    }
}
