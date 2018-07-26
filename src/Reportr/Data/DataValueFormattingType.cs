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

        [Description("Decimal Number")]
        DecimalNumber = 4,

        [Description("Whole Number")]
        WholeNumber = 8,

        [Description("Date")]
        Date = 16,

        [Description("Time")]
        Time = 32,

        [Description("Date and Time")]
        DateAndTime = 64
    }
}
