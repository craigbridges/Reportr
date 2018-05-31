namespace Reportr.Components.Metrics
{
    using Reportr.Filtering;
    using System.Linq;
    using System.Threading.Tasks;

    /// <summary>
    /// Represents a report statistic generator
    /// </summary>
    public class StatisticGenerator : ReportComponentGeneratorBase
    {
        /// <summary>
        /// Asynchronously generates a component from a report definition and filter
        /// </summary>
        /// <param name="definition">The component definition</param>
        /// <param name="sectionType">The report section type</param>
        /// <param name="filter">The report filter</param>
        /// <returns>The report component generated</returns>
        public override async Task<IReportComponent> GenerateAsync
            (
                IReportComponentDefinition definition,
                ReportSectionType sectionType,
                ReportFilter filter
            )
        {
            Validate.IsNotNull(definition);
            Validate.IsNotNull(filter);

            var statisticDefinition = definition.As<StatisticDefinition>();
            var aggregator = statisticDefinition.Aggregator;
            var defaultParameters = statisticDefinition.DefaultParameterValues;

            var parameters = filter.GetQueryParameters
            (
                aggregator.Query,
                defaultParameters.ToArray()
            );

            var value = await aggregator.ExecuteAsync
            (
                parameters.ToArray()
            );

            var statistic = new Statistic
            (
                statisticDefinition,
                value
            );

            if (statisticDefinition.Action != null)
            {
                statistic = statistic.WithAction
                (
                    statisticDefinition.Action
                );
            }

            if (statisticDefinition.HasRange)
            {
                statistic = statistic.WithRange
                (
                    statisticDefinition.LowerRange,
                    statisticDefinition.UpperRange
                );
            }

            return statistic;
        }
    }
}
