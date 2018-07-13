namespace Reportr.Registration
{
    using System.Collections.Generic;
    
    /// <summary>
    /// Represents a collection of submitted report filter values
    /// </summary>
    public class SubmittedReportFilterValues
    {
        /// <summary>
        /// Gets or sets the submitted parameter values
        /// </summary>
        public IEnumerable<SubmittedParameterValue> ParameterValues { get; set; }

        /// <summary>
        /// Gets or sets the submitted sorting rules
        /// </summary>
        public IEnumerable<SubmittedSortingRule> SortingRules { get; set; }
    }
}
