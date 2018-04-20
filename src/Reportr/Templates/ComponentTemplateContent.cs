namespace Reportr.Templates
{
    using Reportr.Components;

    /// <summary>
    /// Represents a single component template content
    /// </summary>
    public class ComponentTemplateContent : TemplateContent
    {
        /// <summary>
        /// Constructs the template content with the content
        /// </summary>
        /// <param name="template">The report template</param>
        /// <param name="content">The content</param>
        /// <param name="componentType">The component type</param>
        internal ComponentTemplateContent
            (
                ReportTemplate template,
                string content,
                ReportComponentType componentType
            )
            : base(template, content)
        {
            this.ComponentType = componentType;
        }

        /// <summary>
        /// Gets the report component type
        /// </summary>
        public ReportComponentType ComponentType { get; protected set; }
    }
}
