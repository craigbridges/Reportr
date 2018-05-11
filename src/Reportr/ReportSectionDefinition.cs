namespace Reportr
{
    using Reportr.Components;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;

    /// <summary>
    /// Represents the definition of a single report section
    /// </summary>
    public class ReportSectionDefinition
    {
        /// <summary>
        /// Constructs the report section with the details
        /// </summary>
        /// <param name="title">The title</param>
        /// <param name="components">The components</param>
        internal ReportSectionDefinition
            (
                string title,
                params IReportComponentDefinition[] components
            )
        {
            Validate.IsNotEmpty(title);

            this.Title = title;
            this.Components = new Collection<IReportComponentDefinition>();

            if (components != null)
            {
                foreach (var component in components)
                {
                    this.Components.Add(component);
                }
            }
        }

        /// <summary>
        /// Gets the sections title
        /// </summary>
        public string Title { get; protected set; }

        /// <summary>
        /// Gets or sets the sections description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets the components in the section
        /// </summary>
        public ICollection<IReportComponentDefinition> Components
        {
            get;
            protected set;
        }
    }
}
