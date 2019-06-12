namespace Reportr.Filtering
{
    using System.ComponentModel;

    /// <summary>
    /// Defines input types for parameter values
    /// </summary>
    public enum ParameterInputType
    {
        [Description("Text")]
        Text = 0,

        [Description("Whole Number")]
        WholeNumber = 1,

        [Description("Decimal Number")]
        DecimalNumber = 2,

        [Description("Boolean")]
        Boolean = 4,

        [Description("Date")]
        Date = 8,

        [Description("Time")]
        Time = 16,

        [Description("Color")]
        Color = 32,

        [Description("Single Select Lookup")]
        SingleSelectLookup = 64,

        [Description("Multi Select Lookup")]
        MultiSelectLookup = 128,

        [Description("Lookup")]
        Lookup = SingleSelectLookup | MultiSelectLookup
    }
}
