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
        /// <param name="components">The report components</param>
        public ReportSection
            (
                string title,
                string description,
                params IReportComponent[] components
            )
        {
            Validate.IsNotEmpty(title);
            Validate.IsNotNull(components);

            this.Title = title;
            this.Description = description;
            this.Components = components;
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
        /// Gets the components in the section
        /// </summary>
        public IReportComponent[] Components { get; private set; }
    }
}
