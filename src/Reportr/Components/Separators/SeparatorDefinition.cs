namespace Reportr.Components.Separators
{
    using Reportr.Data.Querying;
    using System.Collections.Generic;

    /// <summary>
    /// Represents a report component separator definition
    /// </summary>
    public class SeparatorDefinition : ReportComponentDefinitionBase
    {
        /// <summary>
        /// Constructs the report component with the details
        /// </summary>
        /// <param name="name">The name</param>
        /// <param name="title">The title</param>
        /// <param name="separatorType">The separator type</param>
        protected SeparatorDefinition
            (
                string name,
                string title,
                ReportComponentSeparatorType separatorType
            )
            : base(name, title)
        {
            this.SeparatorType = separatorType;
        }

        /// <summary>
        /// Gets or sets the separator type
        /// </summary>
        public ReportComponentSeparatorType SeparatorType
        {
            get;
            protected set;
        }

        /// <summary>
        /// Gets the report component type
        /// </summary>
        public override ReportComponentType ComponentType
        {
            get
            {
                return ReportComponentType.Separator;
            }
        }

        /// <summary>
        /// Gets a collection of all queries being used by the component
        /// </summary>
        /// <returns>A collection of queries</returns>
        public override IEnumerable<IQuery> GetQueriesUsed()
        {
            return new IQuery[] { };
        }
    }
}
