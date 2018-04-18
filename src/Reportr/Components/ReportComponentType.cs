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

        [Description("Chart")]
        Chart = 2,

        [Description("Statistic")]
        Statistic = 4,

        [Description("Graphic")]
        Graphic = 8
    }
}
