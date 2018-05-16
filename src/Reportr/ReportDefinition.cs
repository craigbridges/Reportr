namespace Reportr
{
    using Reportr.Components;
    using Reportr.Data.Querying;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Globalization;
    
    /// <summary>
    /// Represents the definition of a single report
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
    public class ReportDefinition
    {
        /// <summary>
        /// Constructs the report definition with the details
        /// </summary>
        /// <param name="name">The name</param>
        /// <param name="title">The title</param>
        /// <param name="description">The description (optional)</param>
        public ReportDefinition
            (
                string name,
                string title,
                string description = null
            )
        {
            Validate.IsNotEmpty(name);
            Validate.IsNotEmpty(title);

            this.ReportId = Guid.NewGuid();
            this.Name = name;
            this.Title = title;
            this.Description = description;

            this.Culture = CultureInfo.CurrentCulture;
            this.FilterParameters = new Collection<ParameterInfo>();
            this.Fields = new Dictionary<string, object>();

            this.Body = new ReportSectionDefinition
            (
                title,
                ReportSectionType.ReportBody
            );
        }

        /// <summary>
        /// Gets the unique ID of the report
        /// </summary>
        public Guid ReportId { get; protected set; }

        /// <summary>
        /// Gets the reports name
        /// </summary>
        /// <remarks>
        /// The name is used to identify the report and therefore 
        /// must be unique across all reports.
        /// </remarks>
        public string Name { get; protected set; }

        /// <summary>
        /// Gets the reports title
        /// </summary>
        /// <remarks>
        /// The title can be displayed by the report template.
        /// </remarks>
        public string Title { get; protected set; }

        /// <summary>
        /// Gets or sets the reports description
        /// </summary>
        public string Description { get; set; }
        
        /// <summary>
        /// Gets or sets the number of columns in the report
        /// </summary>
        /// <remarks>
        /// The number of columns can be used by the template
        /// to layout the components in a specific way.
        /// </remarks>
        public int? ColumnCount { get; set; }

        /// <summary>
        /// Gets or sets the culture to be used by the report
        /// </summary>
        public CultureInfo Culture { get; set; }
        
        /// <summary>
        /// Gets the filter parameters for the report
        /// </summary>
        public ICollection<ParameterInfo> FilterParameters { get; protected set; }

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
        public Dictionary<string, object> Fields { get; protected set; }

        /// <summary>
        /// Gets the reports page header section
        /// </summary>
        /// <remarks>
        /// This section is printed at the top of every page. 
        /// For example, you can use the page header to repeat the 
        /// report title on every page.
        /// </remarks>
        public ReportSectionDefinition PageHeader { get; protected set; }

        /// <summary>
        /// Defines the reports page header
        /// </summary>
        /// <param name="title">The title</param>
        /// <param name="components">The components to initialise with</param>
        public void DefinePageHeader
            (
                string title,
                params IReportComponentDefinition[] components
            )
        {
            this.PageHeader = new ReportSectionDefinition
            (
                title,
                ReportSectionType.PageHeader,
                components
            );
        }

        /// <summary>
        /// Removes the reports page header
        /// </summary>
        public void RemovePageHeader()
        {
            this.PageHeader = null;
        }

        /// <summary>
        /// Gets the report header section
        /// </summary>
        /// <remarks>
        /// This section is printed just once, at the beginning of the report. 
        /// Use the report header for information that might normally appear 
        /// on a cover page, such as a logo, a title, or a date. 
        /// </remarks>
        public ReportSectionDefinition ReportHeader { get; protected set; }

        /// <summary>
        /// Defines the reports header
        /// </summary>
        /// <param name="title">The title</param>
        /// <param name="components">The components to initialise with</param>
        public void DefineReportHeader
            (
                string title,
                params IReportComponentDefinition[] components
            )
        {
            this.ReportHeader = new ReportSectionDefinition
            (
                title,
                ReportSectionType.ReportHeader,
                components
            );
        }

        /// <summary>
        /// Removes the reports header
        /// </summary>
        public void RemoveReportHeader()
        {
            this.ReportHeader = null;
        }

        /// <summary>
        /// Gets the reports body section
        /// </summary>
        /// <remarks>
        /// This section should contain the report components that make up 
        /// the main body of the report. The section is printed just once,
        /// after the page header and report header.
        /// </remarks>
        public ReportSectionDefinition Body { get; protected set; }

        /// <summary>
        /// Defines the reports body
        /// </summary>
        /// <param name="title">The title</param>
        /// <param name="components">The components to initialise with</param>
        public void DefineBody
            (
                string title,
                params IReportComponentDefinition[] components
            )
        {
            this.Body = new ReportSectionDefinition
            (
                title,
                ReportSectionType.ReportBody,
                components
            );
        }

        /// <summary>
        /// Gets the report footer section
        /// </summary>
        /// <remarks>
        /// This section is printed just once, at the end of the report. 
        /// Use the report footer to print report totals or other summary 
        /// information for the entire report.
        /// </remarks>
        public ReportSectionDefinition ReportFooter { get; protected set; }

        /// <summary>
        /// Defines the reports footer
        /// </summary>
        /// <param name="title">The title</param>
        /// <param name="components">The components to initialise with</param>
        public void DefineReportFooter
            (
                string title,
                params IReportComponentDefinition[] components
            )
        {
            this.ReportFooter = new ReportSectionDefinition
            (
                title,
                ReportSectionType.ReportFooter,
                components
            );
        }

        /// <summary>
        /// Removes the reports footer
        /// </summary>
        public void RemoveReportFooter()
        {
            this.ReportFooter = null;
        }

        /// <summary>
        /// Gets the reports page footer section
        /// </summary>
        /// <remarks>
        /// This section is printed at the end of every page. 
        /// Use a page footer to print page numbers or per-page information.
        /// </remarks>
        public ReportSectionDefinition PageFooter { get; protected set; }

        /// <summary>
        /// Defines the reports page footer
        /// </summary>
        /// <param name="title">The title</param>
        /// <param name="components">The components to initialise with</param>
        public void DefinePageFooter
            (
                string title,
                params IReportComponentDefinition[] components
            )
        {
            this.PageFooter = new ReportSectionDefinition
            (
                title,
                ReportSectionType.PageFooter,
                components
            );
        }

        /// <summary>
        /// Removes the reports page footer
        /// </summary>
        public void RemovePageFooter()
        {
            this.PageFooter = null;
        }

        /// <summary>
        /// Aggregates queries from all components in all sections
        /// </summary>
        /// <returns>A dictionary of queries against section types</returns>
        public Dictionary<ReportSectionType, IEnumerable<IQuery>> AggregateQueries()
        {
            var queries = new Dictionary<ReportSectionType, IEnumerable<IQuery>>
            {
                {
                    ReportSectionType.ReportBody,
                    this.Body.AggregateQueries()
                }
            };

            if (this.PageHeader != null)
            {
                queries.Add
                (
                    ReportSectionType.PageHeader,
                    this.PageHeader.AggregateQueries()
                );
            }

            if (this.ReportHeader != null)
            {
                queries.Add
                (
                    ReportSectionType.ReportHeader,
                    this.ReportHeader.AggregateQueries()
                );
            }

            if (this.PageFooter != null)
            {
                queries.Add
                (
                    ReportSectionType.PageFooter,
                    this.PageFooter.AggregateQueries()
                );
            }

            if (this.ReportFooter != null)
            {
                queries.Add
                (
                    ReportSectionType.ReportFooter,
                    this.ReportFooter.AggregateQueries()
                );
            }

            return queries;
        }
    }
}
