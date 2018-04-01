namespace Reportr.Metrics
{
    using System.ComponentModel;

    /// <summary>
    /// Defines the most common chart types
    /// </summary>
    public enum ChartType
    {
        [Description("Line Graph")]
        LineGraph = 0,

        [Description("Bar Graph")]
        BarGraph = 1,

        [Description("Histogram")]
        Histogram = 2,

        [Description("Pie Chart")]
        PieChart = 4,

        [Description("Cartesian Graph")]
        CartesianGraph = 8
    }
}
