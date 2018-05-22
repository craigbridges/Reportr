namespace Reportr.Filtering
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;

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
        /// Gets a dictionary of query sorting rules by query
        /// </summary>
        public ICollection<ReportFilterSortingRule> SortingRules
        {
            get;
            private set;
        }
    }
}
