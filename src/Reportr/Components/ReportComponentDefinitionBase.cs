namespace Reportr.Components
{
    using Reportr.Data.Querying;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Represents a base class for all report component definitions
    /// </summary>
    public abstract class ReportComponentDefinitionBase : IReportComponentDefinition
    {
        /// <summary>
        /// Constructs the report component with the details
        /// </summary>
        /// <param name="name">The name</param>
        /// <param name="title">The title</param>
        protected ReportComponentDefinitionBase
            (
                string name,
                string title
            )
        {
            Validate.IsNotEmpty(name);
            Validate.IsNotEmpty(title);

            this.ComponentId = Guid.NewGuid();
            this.Name = name;
            this.Title = title;
            this.Fields = new Dictionary<string, object>();
        }

        /// <summary>
        /// Gets the unique ID of the component
        /// </summary>
        public Guid ComponentId { get; protected set; }

        /// <summary>
        /// Gets the name of the component
        /// </summary>
        public string Name { get; protected set; }

        /// <summary>
        /// Gets the components title
        /// </summary>
        public string Title { get; protected set; }

        /// <summary>
        /// Gets the report component type
        /// </summary>
        public abstract ReportComponentType ComponentType { get; }

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
        public Dictionary<string, object> Fields { get; protected set; }

        /// <summary>
        /// Gets a collection of all queries being used by the component
        /// </summary>
        /// <returns>A collection of queries</returns>
        public abstract IEnumerable<IQuery> GetQueriesUsed();

        /// <summary>
        /// Gets or sets the no data message
        /// </summary>
        /// <remarks>
        /// The no data message can be used by the template to display
        /// a custom message when the associated query returns no data.
        /// </remarks>
        public string NoDataMessage { get; set; }
        
        /// <summary>
        /// Gets the components desired relative width
        /// </summary>
        /// <remarks>
        /// The relative width value can be used by the report 
        /// template to scale the components output relatively to
        /// its container.
        /// 
        /// The relative width represents a percentage and must be
        /// either null or a value between 1 and 100.
        /// </remarks>
        public decimal? RelativeWidth { get; protected set; }

        /// <summary>
        /// Gets the components desired relative height
        /// </summary>
        /// <remarks>
        /// The relative height value can be used by the report 
        /// template to scale the components output relatively to
        /// its container.
        /// 
        /// The relative height represents a percentage and must be
        /// either null or a value between 1 and 100.
        /// </remarks>
        public decimal? RelativeHeight { get; protected set; }

        /// <summary>
        /// Sets the components relative size values
        /// </summary>
        /// <param name="width">The relative width</param>
        /// <param name="height">The relative height</param>
        public void SetRelativeSize
            (
                decimal width,
                decimal? height
            )
        {
            if (width < 0)
            {
                throw new ArgumentOutOfRangeException
                (
                    "The width cannot be less than zero."
                );
            }

            if (height.HasValue && height < 0)
            {
                throw new ArgumentOutOfRangeException
                (
                    "The height cannot be less than zero."
                );
            }

            this.RelativeWidth = width;
            this.RelativeHeight = height;
        }

        /// <summary>
        /// Gets or sets the component separator type
        /// </summary>
        public ReportComponentSeparatorType SeparatorType { get; set; }
    }
}
