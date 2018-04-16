namespace Reportr.Components.Metrics
{
    using System;
    
    /// <summary>
    /// Represents a single chart data set
    /// </summary>
    /// <remarks>
    /// A chart data set is a collection of data points that
    /// can be overlaid onto a chart.
    /// 
    /// Some charts require more than one data set to be 
    /// displayed at a time along the same x-axis and y-axis.
    /// 
    /// A common scenario would be a plot graph that comparing
    /// two sets of data. For example, sales by month for the 
    /// current and previous years; the chart would contain 
    /// two data sets, each containing data points for each
    /// month but both representing a different year. In a UI
    /// these would usually be rendered with different colours.
    /// </remarks>
    public class ChartDataSet
    {
        /// <summary>
        /// Constructs the data set with the set details
        /// </summary>
        /// <param name="name">The set name</param>
        /// <param name="dataPoints">The data points</param>
        public ChartDataSet
            (
                string name,
                params ChartDataPoint[] dataPoints
            )
        {
            Validate.IsNotEmpty(name);
            Validate.IsNotNull(dataPoints);

            if (dataPoints.Length == 0)
            {
                throw new ArgumentException
                (
                    "At least one data point is required to create a data set."
                );
            }

            this.Name = name;
            this.DataPoints = dataPoints;
        }

        /// <summary>
        /// Gets the name of the chart data set
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Gets an array of chart data points
        /// </summary>
        public ChartDataPoint[] DataPoints { get; private set; }
    }
}
