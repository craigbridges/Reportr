namespace Reportr.Components
{
    using System.ComponentModel;
    
    /// <summary>
    /// Defines a collection of report action types
    /// </summary>
    public enum ReportActionType
    {
        [Description("Link")]
        Link = 0,

        [Description("Execute Script")]
        Script = 1,

        [Description("Report Filter")]
        Filter = 2
    }
}
