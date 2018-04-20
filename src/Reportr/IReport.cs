namespace Reportr
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;

    /// <summary>
    /// Defines a contract for a report
    /// </summary>
    /// <remarks>
    /// The structure of a report is divided into predefined sections.
    /// 
    /// Report sections divide the report vertically. Depending on their 
    /// type they appear on specific places in the report output and the 
    /// report components they contain are processed and rendered differently.
    /// 
    /// A report consists of a number of sections, each of a different type. 
    /// Every section may contain report components. A report section 
    /// represents a specific area on a report page, used to define how to 
    /// render report components that belong to it.
    /// 
    /// The report sections are page header, report header, detail, 
    /// report footer and page footer.
    /// </remarks>
    public interface IReport
    {
        /// <summary>
        /// Gets the unique ID of the report
        /// </summary>
        Guid ReportId { get; }

        /// <summary>
        /// Gets the reports name
        /// </summary>
        /// <remarks>
        /// The name is used to identify the report and therefore 
        /// must be unique across all reports.
        /// </remarks>
        string Name { get; }

        /// <summary>
        /// Gets the reports title
        /// </summary>
        /// <remarks>
        /// The title can be displayed by the report template.
        /// </remarks>
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
        /// Gets the reports page header section
        /// </summary>
        /// <remarks>
        /// This section is printed at the top of every page. 
        /// For example, you can use the page header to repeat the 
        /// report title on every page.
        /// </remarks>
        IReportSection PageHeader { get; }

        /// <summary>
        /// Gets the report header section
        /// </summary>
        /// <remarks>
        /// This section is printed just once, at the beginning of the report. 
        /// Use the report header for information that might normally appear 
        /// on a cover page, such as a logo, a title, or a date. 
        /// </remarks>
        IReportSection ReportHeader { get; }

        /// <summary>
        /// Gets the reports detail section
        /// </summary>
        /// <remarks>
        /// This section should contain the report components that make up 
        /// the main body of the report. The section is printed just once,
        /// after the page header and report header.
        /// </remarks>
        IReportSection Detail { get; }

        /// <summary>
        /// Gets the report footer section
        /// </summary>
        /// <remarks>
        /// This section is printed just once, at the end of the report. 
        /// Use the report footer to print report totals or other summary 
        /// information for the entire report.
        /// </remarks>
        IReportSection ReportFooter { get; }

        /// <summary>
        /// Gets the reports page footer section
        /// </summary>
        /// <remarks>
        /// This section is printed at the end of every page. 
        /// Use a page footer to print page numbers or per-page information.
        /// </remarks>
        IReportSection PageFooter { get; }
    }
}
