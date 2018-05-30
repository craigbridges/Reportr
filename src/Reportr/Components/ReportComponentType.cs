namespace Reportr.Components
{
    using System.ComponentModel;

    /// <summary>
    /// Defines the report component types
    /// </summary>
    public enum ReportComponentType
    {
        [Description("Table")]
        Table = 0,

        [Description("Repeater")]
        Repeater = 1,

        [Description("Statistic")]
        Statistic = 2,

        [Description("Line Graph")]
        LineGraph = 4,

        [Description("Bar Graph")]
        BarGraph = 8,

        [Description("Histogram")]
        Histogram = 16,

        [Description("Pie Chart")]
        PieChart = 32,

        [Description("Cartesian Graph")]
        CartesianGraph = 64,

        [Description("Chart")]
        Chart = LineGraph | BarGraph | Histogram | PieChart | CartesianGraph,

        [Description("Graphic")]
        Graphic = 128,

        [Description("Separator")]
        Separator = 256
    }
}
