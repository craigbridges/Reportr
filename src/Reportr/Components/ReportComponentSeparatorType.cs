namespace Reportr.Components
{
    using System.ComponentModel;

    /// <summary>
    /// Defines report component separator types
    /// </summary>
    public enum ReportComponentSeparatorType
    {
        [Description("None")]
        None = 0,

        [Description("New Row")]
        NewRow = 1,

        [Description("New Page")]
        NewPage = 2
    }
}
