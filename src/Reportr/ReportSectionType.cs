namespace Reportr
{
    using System.ComponentModel;

    /// <summary>
    /// Defines a collection of supported report section types
    /// </summary>
    public enum ReportSectionType
    {
        [Description("Page Header")]
        PageHeader = 0,

        [Description("Report Header")]
        ReportHeader = 1,

        [Description("Report Body")]
        ReportBody = 2,

        [Description("Report Footer")]
        ReportFooter = 4,

        [Description("Page Footer")]
        PageFooter = 8
    }
}
