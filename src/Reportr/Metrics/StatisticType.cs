namespace Reportr.Metrics
{
    using System.ComponentModel;

    /// <summary>
    /// Defines the major statistic types
    /// </summary>
    public enum StatisticType
    {
        [Description("Measure of Frequency")]
        Frequency = 0,

        [Description("Measure of Central Tendency")]
        CentralTendency = 1,

        [Description("Measure of Dispersion or Variation")]
        Dispersion = 2,

        [Description("Measure of Position")]
        Position = 4
    }
}
