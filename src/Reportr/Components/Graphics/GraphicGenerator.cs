namespace Reportr.Components.Graphics
{
    using Reportr.Components.Metrics;
    using Reportr.Filtering;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    /// <summary>
    /// Represents a report graphic generator
    /// </summary>
    public class GraphicGenerator : ReportComponentGeneratorBase
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

            var graphicDefinition = definition.As<GraphicDefinition>();
            
            var graphic = new Graphic
            (
                graphicDefinition
            );
            
            if (graphicDefinition.OverlayStatistics.Any())
            {
                var statisticGenerator = new StatisticGenerator();

                var statisticDefinitions = graphicDefinition.OverlayStatistics.SelectMany
                (
                    pair => pair.Value.Select
                    (
                        item => item
                    )
                );
                
                var statisticTasks = new List<Task<IReportComponent>>();
                
                foreach (var item in statisticDefinitions)
                {
                    statisticTasks.Add
                    (
                        statisticGenerator.GenerateAsync
                        (
                            item,
                            sectionType,
                            filter
                        )
                    );
                }

                await Task.WhenAll(statisticTasks).ConfigureAwait
                (
                    false
                );

                var taskIndex = 0;

                foreach (var item in graphicDefinition.OverlayStatistics)
                {
                    var statisticList = new List<Statistic>();

                    foreach (var statisticDefinition in item.Value)
                    {
                        var task = statisticTasks.ElementAt
                        (
                            taskIndex
                        );

                        var statistic = await task.ConfigureAwait(false) as Statistic;

                        statisticList.Add(statistic);

                        taskIndex++;
                    }

                    graphic.OverlayStatistics.Add
                    (
                        item.Key,
                        statisticList
                    );
                }
            }

            return graphic;
        }
    }
}
