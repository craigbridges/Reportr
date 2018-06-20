namespace Reportr.Registration
{
    using System.ComponentModel;

    /// <summary>
    /// Defines report definition source types
    /// </summary>
    public enum ReportDefinitionSourceType
    {
        [Description("Report Definition Builder")]
        Builder = 0,

        [Description("Report Definition Script")]
        Script = 1
    }
}
