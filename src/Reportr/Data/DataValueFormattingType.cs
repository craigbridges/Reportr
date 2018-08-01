namespace Reportr.Data
{
    using System.ComponentModel;

    /// <summary>
    /// Defines data value formatting types
    /// </summary>
    /// <remarks>
    /// Format types specify how a string representation 
    /// of a data value should be formatted.
    /// </remarks>
    public enum DataValueFormattingType
    {
        [Description("No Formatting")]
        None = 0,

        [Description("Boolean")]
        Boolean = 1,

        [Description("Currency")]
        Currency = 2,

        [Description("Percentage")]
        Percentage = 4,

        [Description("Decimal Number")]
        DecimalNumber = 8,

        [Description("Whole Number")]
        WholeNumber = 16,

        [Description("Date")]
        Date = 32,

        [Description("Time")]
        Time = 64,

        [Description("Date and Time")]
        DateAndTime = 128,

        [Description("Enumeration")]
        Enum = 256
    }
}
