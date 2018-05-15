namespace Reportr.Templating
{
    using System;

    /// <summary>
    /// Represents a single template content
    /// </summary>
    public class TemplateContent
    {
        /// <summary>
        /// Constructs the template content with the content
        /// </summary>
        /// <param name="template">The report template</param>
        /// <param name="content">The content</param>
        internal TemplateContent
            (
                ReportTemplate template,
                string content
            )
        {
            Validate.IsNotNull(template);

            this.Template = template;
            this.DateCreated = DateTime.UtcNow;

            SetContent(content);
        }

        /// <summary>
        /// Gets the template managing the content
        /// </summary>
        public ReportTemplate Template { get; protected set; }

        /// <summary>
        /// Gets the date and time the content was created
        /// </summary>
        public DateTime DateCreated { get; private set; }

        /// <summary>
        /// Gets the date and time the content was modified
        /// </summary>
        public DateTime DateModified { get; protected set; }

        /// <summary>
        /// Gets the template content
        /// </summary>
        public string Content { get; protected set; }

        /// <summary>
        /// Sets the template content value
        /// </summary>
        /// <param name="content">The content to set</param>
        internal void SetContent
            (
                string content
            )
        {
            this.Content = content;
            this.DateModified = DateTime.UtcNow;
        }
    }
}
