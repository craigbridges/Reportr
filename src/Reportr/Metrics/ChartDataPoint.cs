namespace Reportr.Metrics
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
                string label,
                double y,
                Color? color = null
            )
        {
            Validate.IsNotEmpty(label);

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
                string label = null,
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
        public string Label { get; private set; }

        /// <summary>
        /// Gets the color assigned to the data point
        /// </summary>
        public Color? Color { get; private set; }
    }
}
