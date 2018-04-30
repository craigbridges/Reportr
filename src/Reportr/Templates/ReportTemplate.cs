namespace Reportr.Templates
{
    using Reportr.Components;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;

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
            this.ComponentContents = new Collection<ComponentTemplateContent>();
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
        public SectionTemplateContent PageHeaderContent { get; protected set; }

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
                this.PageHeaderContent = new SectionTemplateContent
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
        public SectionTemplateContent ReportHeaderContent { get; protected set; }

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
                this.ReportHeaderContent = new SectionTemplateContent
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
        public SectionTemplateContent DetailContent { get; protected set; }

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
                this.DetailContent = new SectionTemplateContent
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
        public SectionTemplateContent ReportFooterContent { get; protected set; }

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
                this.ReportFooterContent = new SectionTemplateContent
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
        public SectionTemplateContent PageFooterContent { get; protected set; }

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
                this.PageFooterContent = new SectionTemplateContent
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

        /// <summary>
        /// Gets a collection of component template contents
        /// </summary>
        public ICollection<ComponentTemplateContent> ComponentContents
        {
            get;
            protected set;
        }

        /// <summary>
        /// Sets the template content for a report component type
        /// </summary>
        /// <param name="componentType">The component type</param>
        /// <param name="content">The content</param>
        public void SetComponentContent
            (
                ReportComponentType componentType,
                string content
            )
        {
            var templateContent = this.ComponentContents.FirstOrDefault
            (
                c => c.ComponentType == componentType
            );

            if (templateContent == null)
            {
                templateContent = new ComponentTemplateContent
                (
                    this,
                    content,
                    componentType
                );

                this.ComponentContents.Add(templateContent);
            }
            else
            {
                templateContent.SetContent(content);
            }
        }

        /// <summary>
        /// Determines if the template has content for a component type
        /// </summary>
        /// <param name="componentType">The component type</param>
        /// <returns>True, if content was found; otherwise false</returns>
        public bool HasContentForComponent
            (
                ReportComponentType componentType
            )
        {
            return this.ComponentContents.Any
            (
                c => c.ComponentType == componentType
            );
        }

        /// <summary>
        /// Gets the template content for a specific component type
        /// </summary>
        /// <param name="componentType">The component type</param>
        /// <returns>The template content</returns>
        public ComponentTemplateContent GetComponentContent
            (
                ReportComponentType componentType
            )
        {
            var templateContent = this.ComponentContents.FirstOrDefault
            (
                c => c.ComponentType == componentType
            );

            if (templateContent == null)
            {
                var message = "The content for the component type {0} has not been set.";

                throw new KeyNotFoundException
                (
                    String.Format
                    (
                        message,
                        componentType
                    )
                );
            }

            return templateContent;
        }
    }
}
