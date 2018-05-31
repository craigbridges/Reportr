namespace Reportr
{
    using Reportr.Filtering;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Globalization;

    /// <summary>
    /// Represents a single report
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
    public class Report
    {
        /// <summary>
        /// Constructs the report with the details
        /// </summary>
        /// <param name="definition">The report definition</param>
        /// <param name="filterUsed">The filter used</param>
        public Report
            (
                ReportDefinition definition,
                ReportFilter filterUsed
            )
        {
            Validate.IsNotNull(definition);
            Validate.IsNotNull(filterUsed);

            this.ReportId = Guid.NewGuid();
            this.Name = definition.Name;
            this.Title = definition.Title;
            this.Description = definition.Description;
            this.ColumnCount = definition.ColumnCount;
            this.Culture = definition.Culture;
            this.FilterUsed = filterUsed;

            var fieldValues = new Dictionary<string, object>
            (
                definition.Fields
            );

            var fieldParameters = filterUsed.GetFieldParameters();

            foreach (var parameter in fieldParameters)
            {
                fieldValues[parameter.Name] = parameter.Value;
            }

            this.Fields = new ReadOnlyDictionary<string, object>
            (
                fieldValues
            );
        }

        /// <summary>
        /// Gets the unique ID of the report
        /// </summary>
        public Guid ReportId { get; private set; }

        /// <summary>
        /// Gets the reports name
        /// </summary>
        /// <remarks>
        /// The name is used to identify the report and therefore 
        /// must be unique across all reports.
        /// </remarks>
        public string Name { get; private set; }

        /// <summary>
        /// Gets the reports title
        /// </summary>
        /// <remarks>
        /// The title can be displayed by the report template.
        /// </remarks>
        public string Title { get; private set; }

        /// <summary>
        /// Gets the reports description
        /// </summary>
        public string Description { get; private set; }

        /// <summary>
        /// Gets the number of columns in the report
        /// </summary>
        /// <remarks>
        /// The number of columns can be used by the template
        /// to layout the components in a specific way.
        /// </remarks>
        public int? ColumnCount { get; private set; }

        /// <summary>
        /// Gets the culture to be used by the report
        /// </summary>
        public CultureInfo Culture { get; private set; }

        /// <summary>
        /// Gets the filter that was used to generate the report
        /// </summary>
        public ReportFilter FilterUsed { get; private set; }

        /// <summary>
        /// Gets a dictionary of report fields
        /// </summary
        /// <remarks>
        /// The report fields are a top level collection of 
        /// name-values, where the value can be of any type.
        /// 
        /// Report fields can be used by report templates 
        /// for applying rendering logic. This way a template
        /// can conditionally render something based on the 
        /// state of a field.
        /// </remarks>
        public ReadOnlyDictionary<string, object> Fields
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the reports page header section
        /// </summary>
        /// <remarks>
        /// This section is printed at the top of every page. 
        /// For example, you can use the page header to repeat the 
        /// report title on every page.
        /// </remarks>
        public ReportSection PageHeader { get; private set; }

        /// <summary>
        /// Adds the page header to the report
        /// </summary>
        /// <param name="section">The report section</param>
        /// <returns>The updated report</returns>
        public Report WithPageHeader
            (
                ReportSection section
            )
        {
            Validate.IsNotNull(section);

            this.PageHeader = section;

            return this;
        }

        /// <summary>
        /// Gets the report header section
        /// </summary>
        /// <remarks>
        /// This section is printed just once, at the beginning of the report. 
        /// Use the report header for information that might normally appear 
        /// on a cover page, such as a logo, a title, or a date. 
        /// </remarks>
        public ReportSection ReportHeader { get; private set; }

        /// <summary>
        /// Adds the report header to the report
        /// </summary>
        /// <param name="section">The report section</param>
        /// <returns>The updated report</returns>
        public Report WithReportHeader
            (
                ReportSection section
            )
        {
            Validate.IsNotNull(section);

            this.ReportHeader = section;

            return this;
        }

        /// <summary>
        /// Gets the reports body section
        /// </summary>
        /// <remarks>
        /// This section should contain the report components that make up 
        /// the main body of the report. The section is printed just once,
        /// after the page header and report header.
        /// </remarks>
        public ReportSection Body { get; private set; }

        /// <summary>
        /// Adds the body to the report
        /// </summary>
        /// <param name="section">The report section</param>
        /// <returns>The updated report</returns>
        public Report WithBody
            (
                ReportSection section
            )
        {
            Validate.IsNotNull(section);

            this.Body = section;

            return this;
        }

        /// <summary>
        /// Gets the report footer section
        /// </summary>
        /// <remarks>
        /// This section is printed just once, at the end of the report. 
        /// Use the report footer to print report totals or other summary 
        /// information for the entire report.
        /// </remarks>
        public ReportSection ReportFooter { get; private set; }

        /// <summary>
        /// Adds the report footer to the report
        /// </summary>
        /// <param name="section">The report section</param>
        /// <returns>The updated report</returns>
        public Report WithReportFooter
            (
                ReportSection section
            )
        {
            Validate.IsNotNull(section);

            this.ReportFooter = section;

            return this;
        }

        /// <summary>
        /// Gets the reports page footer section
        /// </summary>
        /// <remarks>
        /// This section is printed at the end of every page. 
        /// Use a page footer to print page numbers or per-page information.
        /// </remarks>
        public ReportSection PageFooter { get; private set; }

        /// <summary>
        /// Adds the page footer to the report
        /// </summary>
        /// <param name="section">The report section</param>
        /// <returns>The updated report</returns>
        public Report WithPageFooter
            (
                ReportSection section
            )
        {
            Validate.IsNotNull(section);

            this.PageFooter = section;

            return this;
        }
    }
}
