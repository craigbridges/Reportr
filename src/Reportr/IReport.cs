namespace Reportr
{
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
        // TODO: consider the following structure as well:
        // Page Header:       e.g. Logos
        // Report Header:     e.g.Group Name Section
        // Detail Section:    e.g.Columns of Data
        // Report Footer      e.g.Subtotal
        // Page Footer        e.g.Page no, User Id etc


        /// <summary>
        /// Gets the reports name
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets the name of the template assigned to the report
        /// </summary>
        string TemplateName { get; }

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
        /// Gets the number of columns in the report
        /// </summary>
        /// <remarks>
        /// The column count is used to determine how the report
        /// is laid out. A column count of 1 indicates that each
        /// section will appear on a separate row.
        /// 
        /// Reports with column counts greater than 1 will 
        /// automatically push sections onto new rows once the
        /// column count has been reached.
        /// </remarks>
        int[] ColumnCount { get; }

        /// <summary>
        /// Gets an array of sections in the report
        /// </summary>
        ReportSection[] Sections { get; }
    }
}
