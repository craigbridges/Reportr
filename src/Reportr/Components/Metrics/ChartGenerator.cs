namespace Reportr.Components.Metrics
{
    using Reportr.Data;
    using Reportr.Data.Querying;
    using Reportr.Filtering;
    using System;
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
            var dataSets = new List<ChartDataSet>();

            foreach (var setDefinition in chartDefinition.DataSets)
            {
                var query = setDefinition.Query;
                var dataPoints = new List<ChartDataPoint>();

                var parameterValues = filter.GetParameters
                (
                    sectionType,
                    query
                );

                var queryResults = await query.ExecuteAsync
                (
                    parameterValues.ToArray()
                );

                if (queryResults.AllRows.Any())
                {
                    foreach (var row in queryResults.AllRows)
                    {
                        var point = default(ChartDataPoint);

                        var y = ResolveAxisValue
                        (
                            setDefinition.YAxisBinding,
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
                            var x = ResolveAxisValue
                            (
                                setDefinition.XAxisBinding,
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

        /// <summary>
        /// Resolves the value of a single axis using a binding and row
        /// </summary>
        /// <param name="binding">The data binding</param>
        /// <param name="row">The query row</param>
        /// <returns>The axis value resolved</returns>
        private double ResolveAxisValue
            (
                DataBinding binding,
                QueryRow row
            )
        {
            var rawValue = binding.Resolve(row);

            if (rawValue == null)
            {
                return 0;
            }
            else
            {
                if (false == rawValue.GetType().IsNumeric())
                {
                    var message = "The value '{0}' is not numeric.";

                    throw new ArithmeticException
                    (
                        String.Format
                        (
                            message,
                            rawValue
                        )
                    );
                }

                return Convert.ToDouble(rawValue);
            }
        }
    }
}
