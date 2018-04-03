namespace Reportr.Templates
{
    using System;

    /// <summary>
    /// Represents a single report template
    /// </summary>
    public class ReportTemplate : Template
    {
        /// <summary>
        /// Constructs the report template with the details
        /// </summary>
        /// <param name="name">The template name</param>
        /// <param name="mainContent">The main content</param>
        /// <param name="printableContent">The printable content</param>
        /// <param name="outputType">The output type (optional)</param>
        public ReportTemplate
            (
                string name,
                string mainContent,
                string printableContent,
                TemplateOutputType outputType = TemplateOutputType.Html
            )
            : base
            (
                name,
                mainContent,
                printableContent,
                outputType
            )
        { }

        /// <summary>
        /// Gets the page header content
        /// </summary>
        public string PageHeaderContent { get; private set; }
        
        /// <summary>
        /// Gets the page footer content
        /// </summary>
        public string PageFooterContent { get; private set; }

        /// <summary>
        /// Adds the marginal content to the report template
        /// </summary>
        /// <param name="pageHeaderContent">The page header content</param>
        /// <param name="pageFooterContent">The page footer content</param>
        /// <returns>The updated report template</returns>
        public ReportTemplate WithMarginalContent
            (
                string pageHeaderContent,
                string pageFooterContent
            )
        {
            this.PageHeaderContent = pageHeaderContent;
            this.PageFooterContent = pageFooterContent;
            this.DateModified = DateTime.UtcNow;

            return this;
        }
    }
}
