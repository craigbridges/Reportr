namespace Reportr.Components
{
    using Reportr.Data.Querying;
    using System;

    /// <summary>
    /// Defines a contract for a single report component
    /// </summary>
    public interface IReportComponent
    {
        /// <summary>
        /// Gets the unique ID of the component
        /// </summary>
        Guid ComponentId { get; }

        /// <summary>
        /// Gets the name of the component
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets the no data message
        /// </summary>
        /// <remarks>
        /// The no data message can be used by the template to display
        /// a custom message when the associated query returns no data.
        /// </remarks>
        string NoDataMessage { get; }

        /// <summary>
        /// Gets the report component type
        /// </summary>
        ReportComponentType ComponentType { get; }
        
        /// <summary>
        /// Gets the components associated query
        /// </summary>
        IQuery Query { get; }

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
        decimal? RelativeWidth { get; }

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
        decimal? RelativeHeight { get; }

        /// <summary>
        /// Sets the components relative size values
        /// </summary>
        /// <param name="width">The relative width</param>
        /// <param name="height">The relative height</param>
        void SetRelativeSize
        (
            decimal width,
            decimal? height
        );

        /// <summary>
        /// Generates the component output from the results of a query
        /// </summary>
        /// <param name="results">The query results</param>
        /// <returns>The output generated</returns>
        IReportComponentOutput GenerateOutput
        (
            QueryResults results
        );
    }
}
