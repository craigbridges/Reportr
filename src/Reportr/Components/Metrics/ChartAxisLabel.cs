namespace Reportr.Components.Metrics
{
    using System;
    using System.Drawing;

    /// <summary>
    /// Represents the definition of a single chart axis label
    /// </summary>
    public class ChartAxisLabel
    {
        public ChartAxisLabel()
        {

        }

        /// <summary>
        /// Gets the labels text
        /// </summary>
        public string Text { get; protected set; }

        /// <summary>
        /// Gets the labels color
        /// </summary>
        public Color? Color { get; protected set; }
    }
}
