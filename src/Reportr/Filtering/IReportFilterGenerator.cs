namespace Reportr.Filtering
{
    using Reportr.Data.Querying;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Defines contract for a report filter generator
    /// </summary>
    public interface IReportFilterGenerator
    {
        ReportFilter Generate
        (
            ReportDefinition definition,
            IDictionary<string, object> parameterValues,
            IDictionary<string, QuerySortingRule> sortingRules
        );
    }
}
