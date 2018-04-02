namespace Reportr.Templates
{
    using System;
    
    /// <summary>
    /// Represents a bass class for a template
    /// </summary>
    /// <remarks>
    /// A template contains the content layout for the data generated 
    /// by a report or report component (chart, statistic or query).
    /// 
    /// The template content is used by a template rendering engine
    /// to generate the output for a report.
    /// </remarks>
    public abstract class Template
    {
        /// <summary>
        /// Constructs the template with the details
        /// </summary>
        /// <param name="name">The template name</param>
        /// <param name="content">The template content</param>
        /// <param name="outputType">The output type (optional)</param>
        public Template
            (
                string name,
                string content,
                TemplateOutputType outputType = TemplateOutputType.Html
            )
        {
            Validate.IsNotEmpty(name);

            this.Name = name;
            this.DateCreated = DateTime.UtcNow;

            SetContent
            (
                content,
                outputType
            );
        }

        /// <summary>
        /// Gets the template name
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Gets the template content
        /// </summary>
        public string Content { get; private set; }

        /// <summary>
        /// Gets the template output type
        /// </summary>
        public TemplateOutputType OutputType { get; private set; }

        /// <summary>
        /// Gets the date and time the template was created
        /// </summary>
        public DateTime DateCreated { get; private set; }

        /// <summary>
        /// Gets the date and time the template was modified
        /// </summary>
        public DateTime DateModified { get; private set; }

        /// <summary>
        /// Sets the templates content
        /// </summary>
        /// <param name="content">The content</param>
        /// <param name="outputType">The output type (optional)</param>
        public void SetContent
            (
                string content,
                TemplateOutputType outputType = TemplateOutputType.Html
            )
        {
            this.Content = content;
            this.OutputType = outputType;
            this.DateModified = DateTime.UtcNow;
        }
    }
}
