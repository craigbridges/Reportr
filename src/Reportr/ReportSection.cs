namespace Reportr
{
    /// <summary>
    /// Represents the default implementation for a report section
    /// </summary>
    public class ReportSection : IReportSection
    {
        /// <summary>
        /// Constructs the report section with the core details
        /// </summary>
        /// <param name="name">The section name</param>
        /// <param name="component">The report component</param>
        public ReportSection
            (
                string name,
                IReportComponent component
            )
        {
            Validate.IsNotEmpty(name);
            Validate.IsNotNull(component);

            this.Name = name;
            this.Component = component;
        }

        /// <summary>
        /// Gets the sections name
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Gets the component associated with the section
        /// </summary>
        public IReportComponent Component { get; private set; }

        /// <summary>
        /// Gets the sections title
        /// </summary>
        public string Title { get; private set; }

        /// <summary>
        /// Gets the sections description
        /// </summary>
        public string Description { get; private set; }

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
        /// Gets the sections column span
        /// </summary>
        public int? ColumnSpan { get; private set; }

        /// <summary>
        /// Gets the name of the template assigned to the section
        /// </summary>
        public string TemplateName { get; private set; }

        /// <summary>
        /// Adds the column span to the report section
        /// </summary>
        /// <param name="columnSpan">The column span</param>
        /// <returns>The updated report section</returns>
        public ReportSection WithLayout
            (
                int columnSpan
            )
        {
            this.ColumnSpan = columnSpan;

            return this;
        }

        /// <summary>
        /// Adds the template name to the report section
        /// </summary>
        /// <param name="templateName">The template name</param>
        /// <returns>The updated report section</returns>
        public ReportSection WithLayout
            (
                string templateName
            )
        {
            Validate.IsNotEmpty(templateName);

            this.TemplateName = templateName;

            return this;
        }

        /// <summary>
        /// Adds the layout details to the report section
        /// </summary>
        /// <param name="columnSpan">The column span</param>
        /// <param name="templateName">The template name</param>
        /// <returns>The updated report section</returns>
        public ReportSection WithLayout
            (
                int columnSpan,
                string templateName
            )
        {
            Validate.IsNotEmpty(templateName);

            this.ColumnSpan = columnSpan;
            this.TemplateName = templateName;

            return this;
        }
    }
}
