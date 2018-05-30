namespace Reportr.Components.Separators
{
    using System.ComponentModel;

    /// <summary>
    /// Defines report component separator types
    /// </summary>
    public enum ReportComponentSeparatorType
    {
        [Description("Auto")]
        Auto = 0,

        [Description("New Row")]
        NewRow = 1,

        [Description("New Page")]
        NewPage = 2,

        [Description("Heading")]
        Heading = 4
    }
}
