namespace Reportr.Filtering
{
    using System.ComponentModel;
    
    /// <summary>
    /// Defines parameter lookup data source types
    /// </summary>
    public enum ParameterLookupSourceType
    {
        [Description("Query")]
        Query = 0,

        [Description("Enumeration")]
        Enum = 1
    }
}
