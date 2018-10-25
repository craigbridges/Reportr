namespace Reportr.Components
{
    using Reportr.Data.Querying;
    using Reportr.Drawing;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Defines a contract for a report component definition
    /// </summary>
    public interface IReportComponentDefinition
    {
        /// <summary>
        /// Gets the unique ID of the component
        /// </summary>
        Guid ComponentId { get; }

        /// <summary>
        /// Gets the name of the component
        /// </summary>
        /// <remarks>
        /// A human readable way of identifying the component.
        /// </remarks>
        string Name { get; }

        /// <summary>
        /// Gets the components title
        /// </summary>
        /// <remarks>
        /// The components display text when rendering a report.
        /// </remarks>
        string Title { get; }

        /// <summary>
        /// Gets the report component type
        /// </summary>
        ReportComponentType ComponentType { get; }

        /// <summary>
        /// Gets a dictionary of component fields
        /// </summary
        /// <remarks>
        /// The fields are a component level collection of 
        /// name-values, where the value can be of any type.
        /// 
        /// Component fields can be used by report templates for 
        /// applying rendering logic. This way a template can 
        /// conditionally render something based on a fields state.
        /// </remarks>
        Dictionary<string, object> Fields { get; }

        /// <summary>
        /// Gets a collection of all queries being used by the component
        /// </summary>
        /// <returns>A collection of queries</returns>
        IEnumerable<IQuery> GetQueriesUsed();

        /// <summary>
        /// Gets or sets the no data message
        /// </summary>
        /// <remarks>
        /// The no data message can be used by the template to display
        /// a custom message when the associated query returns no data.
        /// </remarks>
        string NoDataMessage { get; set; }
        
        /// <summary>
        /// Gets or sets the style information
        /// </summary>
        Style Style { get; set; }
    }
}
