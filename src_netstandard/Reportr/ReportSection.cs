namespace Reportr
{
    using Reportr.Components;

    /// <summary>
    /// Represents a single report section
    /// </summary>
    public class ReportSection
    {
        /// <summary>
        /// Constructs the report section with the core details
        /// </summary>
        /// <param name="title">The section title</param>
        /// <param name="description">The description</param>
        /// <param name="sectionType">The section type</param>
        /// <param name="components">The report components</param>
        public ReportSection
            (
                string title,
                string description,
                ReportSectionType sectionType,
                params IReportComponent[] components
            )
        {
            Validate.IsNotEmpty(title);
            Validate.IsNotNull(components);

            this.Title = title;
            this.Description = description;
            this.SectionType = sectionType;
            this.Components = components;

            if (components.Length > 0)
            {
                this.HasData = true;
            }
        }
        
        /// <summary>
        /// Gets the sections title
        /// </summary>
        public string Title { get; private set; }

        /// <summary>
        /// Gets the sections description
        /// </summary>
        public string Description { get; private set; }

        /// <summary>
        /// Gets the section type
        /// </summary>
        public ReportSectionType SectionType { get; private set; }

        /// <summary>
        /// Gets the components in the section
        /// </summary>
        public IReportComponent[] Components { get; private set; }

        /// <summary>
        /// Gets a flag indicating if the report section has any data
        /// </summary>
        public bool HasData { get; private set; }
    }
}
