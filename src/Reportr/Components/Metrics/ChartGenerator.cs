namespace Reportr.Components.Metrics
{
    using Reportr.Data.Querying;
    using Reportr.Filtering;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    /// <summary>
    /// Represents a report chart generator
    /// </summary>
    public class ChartGenerator : ReportComponentGeneratorBase
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
            var chartDefinition = definition.As<ChartDefinition>();
            var queryTasks = new Dictionary<ChartDataSetDefinition, Task<QueryResults>>();
            var dataSets = new List<ChartDataSet>();

            // Build a dictionary of query tasks to execute
            foreach (var setDefinition in chartDefinition.DataSets)
            {
                var query = setDefinition.Query;

                var parameterValues = filter.GetParameters
                (
                    sectionType,
                    query
                );

                var task = query.ExecuteAsync
                (
                    parameterValues.ToArray()
                );

                queryTasks.Add(setDefinition, task);
            }

            await Task.WhenAll
            (
                queryTasks.Select(pair => pair.Value)
            );

            // Compile and process the results of each query
            foreach (var item in queryTasks)
            {
                var dataPoints = new List<ChartDataPoint>();
                var queryResults = await item.Value;
                var setDefinition = item.Key;

                if (queryResults.AllRows.Any())
                {
                    foreach (var row in queryResults.AllRows)
                    {
                        var point = default(ChartDataPoint);

                        var y = setDefinition.YAxisBinding.Resolve<double>
                        (
                            row
                        );


                        // TODO: resolve the label, if there is one
                        var label = default(ChartAxisLabel);


                        if (setDefinition.XAxisBinding == null)
                        {
                            point = new ChartDataPoint
                            (
                                label,
                                y,
                                setDefinition.Color
                            );
                        }
                        else
                        {
                            var x = setDefinition.XAxisBinding.Resolve<double>
                            (
                                row
                            );

                            point = new ChartDataPoint
                            (
                                x,
                                y,
                                label,
                                setDefinition.Color
                            );
                        }

                        dataPoints.Add(point);
                    }

                    var set = new ChartDataSet
                    (
                        setDefinition.Name,
                        dataPoints.ToArray()
                    );

                    dataSets.Add(set);
                }
            }
            
            var chart = new Chart
            (
                chartDefinition,
                dataSets.ToArray()
            );

            return chart;
        }
    }
}
