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
        /// <param name="label">The label</param>
        /// <param name="y">The Y coordinate value</param>
        /// <param name="color">The associated color (optional)</param>
        public ChartDataPoint
            (
                ChartAxisLabel label,
                double y,
                Color? color = null
            )
        {
            Validate.IsNotNull(label);

            this.Label = label;
            this.Y = y;
            this.Color = color;
        }

        /// <summary>
        /// Constructs the chart data point with the data
        /// </summary>
        /// <param name="x">The X coordinate value</param>
        /// <param name="y">The Y coordinate value</param>
        /// <param name="label">The label (optional)</param>
        /// <param name="color">The associated color (optional)</param>
        public ChartDataPoint
            (
                double x,
                double y,
                ChartAxisLabel label = null,
                Color? color = null
            )
        {
            this.X = x;
            this.Y = y;
            this.Label = label;
            this.Color = color;
        }

        /// <summary>
        /// Gets the X coordinate value
        /// </summary>
        public double? X { get; private set; }

        /// <summary>
        /// Gets the Y coordinate value
        /// </summary>
        public double Y { get; private set; }

        /// <summary>
        /// Gets the data points label
        /// </summary>
        public ChartAxisLabel Label { get; private set; }

        /// <summary>
        /// Gets the color assigned to the data point
        /// </summary>
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
