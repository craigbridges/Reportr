namespace Reportr.Filtering
{
    using Reportr.Data;
    using Reportr.Data.Querying;
    using System;

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
        /// <param name="columnName">The column name</param>
        /// <param name="direction">The sorting direction</param>
        public ReportFilterSortingRule
            (
                ReportSectionType sectionType,
                string componentName,
                string columnName,
                SortDirection direction
            )

            : base(columnName, direction)
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

        /// <summary>
        /// Generates a new sorting rule which is a clone of the current
        /// </summary>
        /// <returns>The sorting rule that was generated</returns>
        public ReportFilterSortingRule Clone()
        {
            return new ReportFilterSortingRule
            (
                this.SectionType,
                this.ComponentName,
                this.ColumnName,
                this.Direction
            );
        }
    }
}
