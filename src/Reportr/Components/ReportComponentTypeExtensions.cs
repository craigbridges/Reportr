namespace Reportr.Components
{
    using Reportr.Components.Collections;
    using Reportr.Components.Graphics;
    using Reportr.Components.Metrics;
    using Reportr.Components.Separators;
    using System;
    
    /// <summary>
    /// Various extension methods for the report component type enum
    /// </summary>
    internal static class ReportComponentTypeExtensions
    {
        /// <summary>
        /// Gets a component generator for the report component type
        /// </summary>
        /// <param name="componentType">The component type</param>
        /// <returns>The component generator</returns>
        public static IReportComponentGenerator GetGenerator
            (
                this ReportComponentType componentType
            )
        {
            switch (componentType)
            {
                case ReportComponentType.Chart:
                    return new ChartGenerator();

                case ReportComponentType.Graphic:
                    return new GraphicGenerator();

                case ReportComponentType.Statistic:
                    return new StatisticGenerator();

                case ReportComponentType.Repeater:
                    return new RepeaterGenerator();

                case ReportComponentType.Table:
                    return new TableGenerator();

                case ReportComponentType.Separator:
                    return new SeparatorGenerator();

                default:
                    var message = "The component type {0} is not supported.";

                    throw new InvalidOperationException
                    (
                        String.Format
                        (
                            message,
                            componentType
                        )
                    );
            }
        }
    }
}
