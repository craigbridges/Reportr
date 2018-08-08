namespace Reportr.Filtering
{
    using System.ComponentModel;
    
    /// <summary>
    /// Defines default value types for parameters
    /// </summary>
    public enum ParameterDefaultValueType
    {
        [Description("No Default")]
        NoDefault = 0,

        [Description("Static Value")]
        StaticValue = 1,

        [Description("Value Generator")]
        Generator = 2
    }
}
