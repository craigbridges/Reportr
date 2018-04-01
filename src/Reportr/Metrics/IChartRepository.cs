namespace Reportr.Metrics
{
    using System.Collections.Generic;

    /// <summary>
    /// Defines a contract for a repository that manages charts
    /// </summary>
    public interface IChartRepository
    {
        /// <summary>
        /// Adds a chart to the repository
        /// </summary>
        /// <param name="chart">The chart</param>
        void AddChart
        (
            IChart chart
        );

        /// <summary>
        /// Gets all charts in the repository
        /// </summary>
        /// <returns>A collection of charts</returns>
        IEnumerable<IChart> GetAllCharts();

        /// <summary>
        /// Gets a single chart by name from the repository
        /// </summary>
        /// <param name="name">The chart name</param>
        /// <returns>The matching chart</returns>
        IChart GetChart
        (
            string name
        );
    }
}
