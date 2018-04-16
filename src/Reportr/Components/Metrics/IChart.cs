namespace Reportr.Components.Metrics
{
    /// <summary>
    /// Defines a contract for a single two-dimensional chart
    /// </summary>
    public interface IChart : IReportComponent<ChartResult>
    {
        /// <summary>
        /// Gets the intended chart type
        /// </summary>
        ChartType IntendedType { get; }
        
        /// <summary>
        /// Gets a flag indicating if decimal places should be allowed
        /// </summary>
        bool AllowDecimals { get; }
    }
}
