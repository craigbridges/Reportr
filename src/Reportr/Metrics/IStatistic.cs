namespace Reportr.Metrics
{
    /// <summary>
    /// Defines a contract for a single statistic
    /// </summary>
    public interface IStatistic : IReportComponent<StatisticResult>
    {
        /// <summary>
        /// Gets the statistic type
        /// </summary>
        StatisticType StatisticType { get; }
    }
}
