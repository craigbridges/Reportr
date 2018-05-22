namespace Reportr.Filtering
{
    using Reportr.Data;
    using Reportr.Data.Querying;
    
    /// <summary>
    /// Represents a single report filter sorting rule
    /// </summary>
    public class ReportFilterSortingRule : QuerySortingRule
    {
        /// <summary>
        /// Constructs the sorting rule with the details
        /// </summary>
        /// <param name="sectionType">The section type</param>
        /// <param name="componentName">The component name</param>
        /// <param name="column">The query column</param>
        /// <param name="direction">The sorting direction</param>
        public ReportFilterSortingRule
            (
                ReportSectionType sectionType,
                string componentName,
                QueryColumnInfo column,
                SortDirection direction
            )

            : base(column, direction)
        {
            Validate.IsNotEmpty(componentName);

            this.SectionType = sectionType;
            this.ComponentName = componentName;
        }

        /// <summary>
        /// Gets the report section type
        /// </summary>
        public ReportSectionType SectionType { get; private set; }

        /// <summary>
        /// Gets the component name
        /// </summary>
        public string ComponentName { get; private set; }
    }
}
