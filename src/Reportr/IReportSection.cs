namespace Reportr
{
    /// <summary>
    /// Defines a contract for a single report section
    /// </summary>
    public interface IReportSection
    {
        /// <summary>
        /// Gets the sections name
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets the sections title
        /// </summary>
        string Title { get; }

        /// <summary>
        /// Gets the sections column span
        /// </summary>
        int? ColumnSpan { get; }

        /// <summary>
        /// Gets the name of the template assigned to the section
        /// </summary>
        string TemplateName { get; }

        /// <summary>
        /// Gets the component associated with the section
        /// </summary>
        IReportComponent Component { get; }
    }
}
