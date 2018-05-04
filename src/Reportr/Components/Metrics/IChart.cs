namespace Reportr.Components.Metrics
{
    using System.Collections.Generic;

    /// <summary>
    /// Defines a contract for a single two-dimensional chart
    /// </summary>
    public interface IChart : IReportComponent, IEnumerable<ChartDataSet>
    {
        /// <summary>
        /// Gets an array of chart data sets
        /// </summary>
        ChartDataSet[] DataSets { get; }
    }
}
