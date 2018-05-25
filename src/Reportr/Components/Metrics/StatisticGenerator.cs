﻿namespace Reportr.Components.Metrics
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
            var statisticDefinition = definition.As<StatisticDefinition>();
            var aggregator = statisticDefinition.Aggregator;

            var parameters = filter.GetParameters
            (
                sectionType,
                aggregator.Query
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

            return statistic;
        }
    }
}