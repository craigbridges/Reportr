namespace Reportr.Registration
{
    using Reportr.Data;
    
    /// <summary>
    /// Represents a single submitted sorting rule
    /// </summary>
    public class SubmittedSortingRule
    {
        /// <summary>
        /// Gets or sets the report section type
        /// </summary>
        public ReportSectionType SectionType { get; set; }

        /// <summary>
        /// Gets or sets the component name
        /// </summary>
        public string ComponentName { get; set; }

        /// <summary>
        /// Gets or sets the column name
        /// </summary>
        public string ColumnName { get; set; }

        /// <summary>
        /// Gets or sets the sort direction
        /// </summary>
        public SortDirection Direction { get; set; }
    }
}
