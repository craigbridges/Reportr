namespace Reportr.Templates
{
    using System;
    
    /// <summary>
    /// Represents a single report template
    /// </summary>
    /// <remarks>
    /// A template contains the content layout for the data generated 
    /// by a report or report component (chart, statistic or query).
    /// 
    /// The template content is used by a template rendering engine
    /// to generate the output for a report.
    /// </remarks>
    public class ReportTemplate
    {
        /// <summary>
        /// Constructs the report template with a name
        /// </summary>
        /// <param name="name">The template name</param>
        public ReportTemplate
            (
                string name
            )
        {
            Validate.IsNotEmpty(name);

            this.TemplateId = Guid.NewGuid();
            this.DateCreated = DateTime.UtcNow;
            this.DateModified = DateTime.UtcNow;
            this.Name = name;
        }

        /// <summary>
        /// Gets the templates unique ID
        /// </summary>
        public Guid TemplateId { get; private set; }

        /// <summary>
        /// Gets the date and time the template was created
        /// </summary>
        public DateTime DateCreated { get; private set; }

        /// <summary>
        /// Gets the date and time the template was modified
        /// </summary>
        public DateTime DateModified { get; protected set; }

        /// <summary>
        /// Gets the template name
        /// </summary>
        public string Name { get; protected set; }
        
        /// <summary>
        /// Gets the page header content
        /// </summary>
        public TemplateContent PageHeaderContent { get; protected set; }

        /// <summary>
        /// Sets the page header template content
        /// </summary>
        /// <param name="content">The content</param>
        public void SetPageHeaderContent
            (
                string content
            )
        {
            if (this.PageHeaderContent == null)
            {
                this.PageHeaderContent = new TemplateContent
                (
                    this,
                    content
                );
            }
            else
            {
                this.PageHeaderContent.SetContent
                (
                    content
                );
            }
        }

        /// <summary>
        /// Gets the report header content
        /// </summary>
        public TemplateContent ReportHeaderContent { get; protected set; }

        /// <summary>
        /// Sets the report header template content
        /// </summary>
        /// <param name="content">The content</param>
        public void SetReportHeaderContent
            (
                string content
            )
        {
            if (this.ReportHeaderContent == null)
            {
                this.ReportHeaderContent = new TemplateContent
                (
                    this,
                    content
                );
            }
            else
            {
                this.ReportHeaderContent.SetContent
                (
                    content
                );
            }
        }

        /// <summary>
        /// Gets the report detail content
        /// </summary>
        public TemplateContent DetailContent { get; protected set; }

        /// <summary>
        /// Sets the report detail template content
        /// </summary>
        /// <param name="content">The content</param>
        public void SetDetailContent
            (
                string content
            )
        {
            if (this.DetailContent == null)
            {
                this.DetailContent = new TemplateContent
                (
                    this,
                    content
                );
            }
            else
            {
                this.DetailContent.SetContent
                (
                    content
                );
            }
        }

        /// <summary>
        /// Gets the report footer content
        /// </summary>
        public TemplateContent ReportFooterContent { get; protected set; }

        /// <summary>
        /// Sets the report footer template content
        /// </summary>
        /// <param name="content">The content</param>
        public void SetReportFooterContent
            (
                string content
            )
        {
            if (this.ReportFooterContent == null)
            {
                this.ReportFooterContent = new TemplateContent
                (
                    this,
                    content
                );
            }
            else
            {
                this.ReportFooterContent.SetContent
                (
                    content
                );
            }
        }

        /// <summary>
        /// Gets the page footer content
        /// </summary>
        public TemplateContent PageFooterContent { get; protected set; }

        /// <summary>
        /// Sets the page footer template content
        /// </summary>
        /// <param name="content">The content</param>
        public void SetPageFooterContent
            (
                string content
            )
        {
            if (this.PageFooterContent == null)
            {
                this.PageFooterContent = new TemplateContent
                (
                    this,
                    content
                );
            }
            else
            {
                this.PageFooterContent.SetContent
                (
                    content
                );
            }
        }


        // TODO: manage content for each component type
    }
}
