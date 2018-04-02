namespace Reportr.Templates
{
    /// <summary>
    /// Represents a single report section template
    /// </summary>
    public class ReportSectionTemplate : Template
    {
        /// <summary>
        /// Constructs the template with the details
        /// </summary>
        /// <param name="name">The template name</param>
        /// <param name="content">The template content</param>
        /// <param name="outputType">The output type (optional)</param>
        public ReportSectionTemplate
            (
                string name,
                string content,
                TemplateOutputType outputType = TemplateOutputType.Html
            )

            : base(name, content, outputType)
        { }
    }
}
