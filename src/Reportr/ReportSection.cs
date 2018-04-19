namespace Reportr
{
    using Reportr.Components;

    /// <summary>
    /// Represents the default implementation for a report section
    /// </summary>
    public class ReportSection : IReportSection
    {
        /// <summary>
        /// Constructs the report section with the core details
        /// </summary>
        /// <param name="name">The section name</param>
        /// <param name="components">The report components</param>
        public ReportSection
            (
                string name,
                params IReportComponent[] components
            )
        {
            Validate.IsNotEmpty(name);
            Validate.IsNotNull(components);

            this.Name = name;
            this.Components = components;
        }

        /// <summary>
        /// Gets the sections name
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Gets the components in the section
        /// </summary>
        public IReportComponent[] Components { get; private set; }

        /// <summary>
        /// Adds the descriptors to the report section
        /// </summary>
        /// <param name="title">The title</param>
        /// <param name="description">The description (optional)</param>
        /// <returns>The updated report section</returns>
        public ReportSection WithDescriptors
            (
                string title,
                string description = null
            )
        {
            Validate.IsNotEmpty(title);

            this.Title = title;
            this.Description = description;

            return this;
        }

        /// <summary>
        /// Gets the sections title
        /// </summary>
        public string Title { get; private set; }

        /// <summary>
        /// Gets the sections description
        /// </summary>
        public string Description { get; private set; }
    }
}
