namespace Reportr.Filtering
{
    using System.ComponentModel;

    /// <summary>
    /// Defines a series of report parameter target types
    /// </summary>
    public enum ReportParameterTargetType
    {
        [Description("Filter Query")]
        Filter = 0,

        [Description("Set Report Field")]
        SetField = 1,

        [Description("Exclude Report Component")]
        ExcludeComponent = 2
    }
}
