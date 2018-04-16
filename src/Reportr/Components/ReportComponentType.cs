namespace Reportr.Components
{
    using System.ComponentModel;

    /// <summary>
    /// Defines the report component types
    /// </summary>
    public enum ReportComponentType
    {
        [Description("Chart")]
        Chart = 0,

        [Description("Query")]
        Query = 1,

        [Description("Statistic")]
        Statistic = 2
    }
}
