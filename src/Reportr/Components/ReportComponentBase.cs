namespace Reportr.Components
{
    using System.Collections.Generic;

    /// <summary>
    /// Represents a base class for a report component
    /// </summary>
    public abstract class ReportComponentBase : IReportComponent
    {
        /// <summary>
        /// Constructs the report component with the details
        /// </summary>
        /// <param name="component">The component definition</param>
        protected ReportComponentBase
            (
                IReportComponentDefinition component
            )
        {
            Validate.IsNotNull(component);

            this.ComponentDefinition = component;
            this.Fields = component.Fields;
        }

        /// <summary>
        /// Gets the definition used to generate the component
        /// </summary>
        public IReportComponentDefinition ComponentDefinition
        {
            get;
            protected set;
        }

        /// <summary>
        /// Gets a dictionary of component fields
        /// </summary
        /// <remarks>
        /// The fields are a component level collection of 
        /// name-values, where the value can be of any type.
        /// 
        /// Component fields can be used by report templates 
        /// for applying rendering logic. This way a template
        /// can conditionally render something based on the 
        /// state of a field.
        /// </remarks>
        public Dictionary<string, object> Fields
        {
            get;
            protected set;
        }
    }
}
