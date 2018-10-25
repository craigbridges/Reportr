namespace Reportr.Filtering
{
    using Reportr.Data;
    using System.Collections.Generic;

    /// <summary>
    /// Defines contract for a report filter generator
    /// </summary>
    public interface IReportFilterGenerator
    {
        /// <summary>
        /// Generates a default filter for a report definition
        /// </summary>
        /// <param name="definition">The report definition</param>
        /// <returns>The report filter</returns>
        ReportFilter Generate
        (
            ReportDefinition definition
        );

        /// <summary>
        /// Generates a filter for a report and parameter values
        /// </summary>
        /// <param name="definition">The report definition</param>
        /// <param name="parameterValues">The parameter values</param>
        /// <returns>The report filter</returns>
        ReportFilter Generate
        (
            ReportDefinition definition,
            IDictionary<string, object> parameterValues
        );

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
        ReportFilter Generate
        (
            ReportDefinition definition,
            IDictionary<string, object> parameterValues,
            IDictionary<string, KeyValuePair<string, SortDirection>> sortingRules
        );
    }
}
