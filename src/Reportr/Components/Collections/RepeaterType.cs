namespace Reportr.Components.Collections
{
    using System.ComponentModel;

    /// <summary>
    /// Defines all the standard repeater types
    /// </summary>
    public enum RepeaterType
    {
        [Description("Unordered List")]
        UnorderedList = 0,

        [Description("Ordered List")]
        OrderedList = 2,

        [Description("Block")]
        Block = 3
    }
}
