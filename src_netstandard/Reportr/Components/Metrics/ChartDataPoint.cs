namespace Reportr.Components.Metrics
{
    using System.Drawing;

    /// <summary>
    /// Represents a single chart data point
    /// </summary>
    public class ChartDataPoint
    {
        /// <summary>
        /// Constructs the chart data point with the data
        /// </summary>
        /// <param name="x">The x-axis value</param>
        /// <param name="y">The y-axis value</param>
        /// <param name="color">The color (optional)</param>
        public ChartDataPoint
            (
                double x,
                double y,
                Color? color = null
            )
        {
            this.X = x;
            this.Y = y;
            this.Color = color;
        }

        /// <summary>
        /// Gets the x-axis value
        /// </summary>
        public double X { get; private set; }

        /// <summary>
        /// Gets the y-axis value
        /// </summary>
        public double Y { get; private set; }

        /// <summary>
        /// Gets the color assigned to the data point
        /// </summary>
        /// <remarks>
        /// This is useful for pie or doughnut charts
        /// </remarks>
        public Color? Color { get; private set; }

        /// <summary>
        /// Gets the statistic action
        /// </summary>
        public ReportAction Action { get; private set; }

        /// <summary>
        /// Adds the action to the data point
        /// </summary>
        /// <param name="action">The action</param>
        /// <returns>The updated data point</returns>
        public ChartDataPoint WithAction
            (
                ReportAction action
            )
        {
            Validate.IsNotNull(action);

            this.Action = action;

            return this;
        }
    }
}
