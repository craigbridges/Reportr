namespace Reportr.Components.Querying
{
    using System.ComponentModel;

    /// <summary>
    /// Defines importance flags for query columns
    /// </summary>
    public enum QueryColumnImportance
    {
        [Description("Low")]
        Low = 0,

        [Description("Medium")]
        Medium = 1,

        [Description("High")]
        High = 2
    }
}
