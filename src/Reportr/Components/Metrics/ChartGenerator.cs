namespace Reportr.Components.Metrics
{
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

            var labelList = new List<ChartAxisLabel>();

            // Compile and process the results of each query
            foreach (var item in queryTasks)
            {
                var dataPoints = new List<ChartDataPoint>();
                var queryResults = await item.Value;
                var setDefinition = item.Key;

                if (queryResults.AllRows.Any())
                {
                    var rowNumber = 1;

                    foreach (var row in queryResults.AllRows)
                    {
                        var xLabelValue = default(object);

                        var xValue = setDefinition.XAxisBinding.Resolve<double>
                        (
                            row
                        );

                        if (setDefinition.XAxisLabelBinding == null)
                        {
                            xLabelValue = xValue;
                        }
                        else
                        {
                            xLabelValue = setDefinition.XAxisLabelBinding.Resolve
                            (
                                row
                            );
                        }

                        var xLabel = GenerateLabel
                        (
                            xLabelValue,
                            rowNumber,
                            chartDefinition.XAxisLabelTemplate
                        );

                        var yValue = setDefinition.YAxisBinding.Resolve<double>
                        (
                            row
                        );
                        
                        var point = new ChartDataPoint
                        (
                            xValue,
                            yValue,
                            setDefinition.Color
                        );

                        dataPoints.Add(point);

                        if (false == labelList.Any(l => l.Text == xLabel.Text))
                        {
                            labelList.Add(xLabel);
                        }
                    }

                    var set = new ChartDataSet
                    (
                        setDefinition.Name,
                        dataPoints.ToArray()
                    );

                    dataSets.Add(set);

                    rowNumber++;
                }
            }
            
            var chart = new Chart
            (
                chartDefinition,
                labelList,
                dataSets.ToArray()
            );

            return chart;
        }

        /// <summary>
        /// Generates a chart axis label using a value and template
        /// </summary>
        /// <param name="value">The axis value</param>
        /// <param name="rowNumber">The query row number</param>
        /// <param name="template">The label template (optional)</param>
        /// <returns>The label generated</returns>
        private ChartAxisLabel GenerateLabel
            (
                object value,
                int rowNumber,
                ChartAxisLabel template = null
            )
        {
            if (template == null)
            {
                if (value == null)
                {
                    return new ChartAxisLabel(rowNumber);
                }
                else
                {
                    var valueType = value.GetType();

                    if (valueType.IsNumeric())
                    {
                        return new ChartAxisLabel
                        (
                            Convert.ToDouble(value)
                        );
                    }
                    else if (valueType == typeof(DateTime) || valueType == typeof(DateTime?))
                    {
                        return new ChartAxisLabel
                        (
                            (DateTime)value
                        );
                    }
                    else
                    {
                        return new ChartAxisLabel
                        (
                            value.ToString()
                        );
                    }
                }
            }
            else
            {
                if (value == null)
                {
                    return template;
                }
                else
                {
                    var valueType = value.GetType();
                    var label = template.Clone();

                    switch (template.ValueType)
                    {
                        case ChartValueType.Double:

                            if (false == valueType.IsNumeric())
                            {
                                var message = "The value '{0}' is not numeric.";

                                throw new InvalidCastException
                                (
                                    String.Format(message, value)
                                );
                            }
                            
                            label.DoubleValue = Convert.ToDouble
                            (
                                value
                            );

                            break;

                        case ChartValueType.DateTime:

                            if (valueType != typeof(DateTime) && valueType != typeof(DateTime?))
                            {
                                var message = "The value '{0}' is not a valid date.";

                                throw new InvalidCastException
                                (
                                    String.Format(message, value)
                                );
                            }

                            label.DateValue = (DateTime)value;
                            break;

                        default:
                            label.CustomText = value.ToString();
                            break;
                    }

                    return label;
                }
            }
        }
    }
}
