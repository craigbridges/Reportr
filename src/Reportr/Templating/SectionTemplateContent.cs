namespace Reportr.Templating
{
    /// <summary>
    /// Represents a single section template content
    /// </summary>
    public class SectionTemplateContent : TemplateContent
    {
        /// <summary>
        /// Constructs the template content with the content
        /// </summary>
        /// <param name="template">The report template</param>
        /// <param name="content">The content</param>
        internal SectionTemplateContent
            (
                ReportTemplate template,
                string content
            )
            : base(template, content)
        { }
    }
}
