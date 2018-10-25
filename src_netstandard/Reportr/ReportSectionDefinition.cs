namespace Reportr
{
    using Reportr.Components;
    using Reportr.Data.Querying;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;

    /// <summary>
    /// Represents the definition of a single report section
    /// </summary>
    public class ReportSectionDefinition
    {
        /// <summary>
        /// Constructs the report section with the details
        /// </summary>
        /// <param name="title">The title</param>
        /// <param name="sectionType">The section type</param>
        /// <param name="components">The components</param>
        internal ReportSectionDefinition
            (
                string title,
                ReportSectionType sectionType,
                params IReportComponentDefinition[] components
            )
        {
            Validate.IsNotEmpty(title);

            this.Title = title;
            this.SectionType = sectionType;
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
        /// Gets the section type
        /// </summary>
        public ReportSectionType SectionType { get; protected set; }

        /// <summary>
        /// Gets the components in the section
        /// </summary>
        public ICollection<IReportComponentDefinition> Components
        {
            get;
            protected set;
        }

        /// <summary>
        /// Aggregates queries from all components in the section
        /// </summary>
        /// <returns>A collection of queries</returns>
        public IEnumerable<IQuery> AggregateQueries()
        {
            var queries = new List<IQuery>();

            foreach (var component in this.Components.ToList())
            {
                queries.AddRange
                (
                    component.GetQueriesUsed()
                );
            }

            return queries;
        }
    }
}
