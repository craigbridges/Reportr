namespace Reportr.Components
{
    using System.Collections.Generic;

    /// <summary>
    /// Defines a contract for a report component
    /// </summary>
    public interface IReportComponent
    {
        /// <summary>
        /// Gets the definition that generated the component
        /// </summary>
        IReportComponentDefinition ComponentDefinition { get; }

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
        Dictionary<string, object> Fields { get; }
    }
}
