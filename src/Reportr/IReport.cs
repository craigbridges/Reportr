namespace Reportr
{
    using System.Collections.Generic;
    using System.Globalization;

    /// <summary>
    /// Defines a contract for a report
    /// </summary>
    /// <remarks>
    /// A report is built up of a collection of sections that fit
    /// into a predefined number of columns.
    /// 
    /// Each section contains a single component 
    /// (chart, statistic or query) and can have multiple templates 
    /// for different types of output formats (e.g. HTML, CSV etc).
    /// </remarks>
    public interface IReport
    {
        /// <summary>
        /// Gets the reports name
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets the reports title
        /// </summary>
        string Title { get; }

        /// <summary>
        /// Gets the reports description
        /// </summary>
        string Description { get; }

        /// <summary>
        /// Gets or sets the current culture used by the report
        /// </summary>
        CultureInfo CurrentCulture { get; set; }
        
        /// <summary>
        /// Gets an array of sections in the report
        /// </summary>
        IReportSection[] Sections { get; }

        /// <summary>
        /// Gets an array of filter parameters for the report
        /// </summary>
        ParameterInfo[] FilterParameters { get; }

        /// <summary>
        /// Gets a dictionary of report fields
        /// </summary
        /// <remarks>
        /// The report fields are a top level collection of 
        /// name-values, where the value can be of any type.
        /// </remarks>
        IDictionary<string, object> Fields { get; }

        /// <summary>
        /// Generates a default filter for the report
        /// </summary>
        /// <returns>The filter generated</returns>
        IReportFilter GenerateDefaultFilter();

        /// <summary>
        /// Runs the report using the default filter
        /// </summary>
        /// <returns>The report output</returns>
        ReportOutput Run();

        /// <summary>
        /// Runs the report using the filter specified
        /// </summary>
        /// <param name="filter">The filter</param>
        /// <returns>The report output</returns>
        ReportOutput Run
        (
            IReportFilter filter
        );
    }
}
