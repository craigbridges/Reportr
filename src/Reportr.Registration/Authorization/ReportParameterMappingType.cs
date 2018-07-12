namespace Reportr.Registration.Authorization
{
    using System.ComponentModel;

    /// <summary>
    /// Defines report parameter mapping types
    /// </summary>
    public enum ReportParameterMappingType
    {
        [Description("Literal")]
        Literal = 0,

        [Description("Meta Data")]
        MetaData = 1
    }
}
