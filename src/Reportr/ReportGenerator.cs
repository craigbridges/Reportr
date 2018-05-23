namespace Reportr
{
    using Reportr.Components;
    using Reportr.Components.Metrics;
    using Reportr.Filtering;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Threading.Tasks;

    /// <summary>
    /// Represents the default implementation of the report generator 
    /// </summary>
    public class ReportGenerator : IReportGenerator
    {
        /// <summary>
        /// Generates a report using a report definition
        /// </summary>
        /// <param name="definition">The report definition</param>
        /// <param name="filter">The filter (optional)</param>
        /// <returns>The generated result</returns>
        public ReportGenerationResult Generate
            (
                ReportDefinition definition,
                ReportFilter filter = null
            )
        {
            var task = Task.Run
            (
                async () => await GenerateAsync(definition, filter)
            );

            return task.Result;
        }

        /// <summary>
        /// Asynchronously generates a report using a report definition
        /// </summary>
        /// <param name="definition">The report definition</param>
        /// <param name="filter">The filter (optional)</param>
        /// <returns>The generation result</returns>
        public async Task<ReportGenerationResult> GenerateAsync
            (
                ReportDefinition definition,
                ReportFilter filter = null
            )
        {
            Validate.IsNotNull(definition);

            var watch = Stopwatch.StartNew();

            if (filter == null)
            {
                filter = definition.GenerateDefaultFilter();
            }

            var pageHeaderResult = await GenerateSectionAsync
            (
                definition,
                filter,
                ReportSectionType.PageHeader
            );

            var reportHeaderResult = await GenerateSectionAsync
            (
                definition,
                filter,
                ReportSectionType.ReportHeader
            );

            var reportBodyResult = await GenerateSectionAsync
            (
                definition,
                filter,
                ReportSectionType.ReportBody
            );

            var pageFooterResult = await GenerateSectionAsync
            (
                definition,
                filter,
                ReportSectionType.PageFooter
            );

            var reportFooterResult = await GenerateSectionAsync
            (
                definition,
                filter,
                ReportSectionType.ReportFooter
            );
            
            watch.Stop();

            var executionTime = watch.ElapsedMilliseconds;

            var errorMessages = CompileErrors
            (
                pageHeaderResult,
                reportHeaderResult,
                reportBodyResult,
                reportFooterResult,
                pageFooterResult
            );

            if (errorMessages.Any())
            {
                return new ReportGenerationResult
                (
                    executionTime,
                    errorMessages
                );
            }
            else
            {
                var report = new Report(definition, filter);
                
                if (pageHeaderResult != null)
                {
                    report = report.WithPageHeader
                    (
                        pageHeaderResult.Section
                    );
                }

                if (reportHeaderResult != null)
                {
                    report = report.WithReportHeader
                    (
                        reportHeaderResult.Section
                    );
                }

                if (reportBodyResult != null)
                {
                    report = report.WithBody
                    (
                        reportBodyResult.Section
                    );
                }

                if (reportFooterResult != null)
                {
                    report = report.WithReportFooter
                    (
                        reportFooterResult.Section
                    );
                }

                if (pageFooterResult != null)
                {
                    report = report.WithPageFooter
                    (
                        pageFooterResult.Section
                    );
                }

                return new ReportGenerationResult
                (
                    executionTime,
                    report
                );
            }
        }

        /// <summary>
        /// Asynchronously generates a report section from a report definition
        /// </summary>
        /// <param name="report">The report definition</param>
        /// <param name="filter">The report filter</param>
        /// <param name="sectionType">The section type</param>
        /// <returns>The generated section</returns>
        private async Task<ReportSectionGenerationResult> GenerateSectionAsync
            (
                ReportDefinition report,
                ReportFilter filter,
                ReportSectionType sectionType
            )
        {
            var watch = Stopwatch.StartNew();

            var sectionDefinition = report.GetSection
            (
                sectionType
            );

            if (sectionDefinition == null)
            {
                return null;
            }
            else
            {
                var componentList = new List<IReportComponent>();
                var errorMessages = new Dictionary<string, string>();

                foreach (var componentDefinition in sectionDefinition.Components)
                {
                    try
                    {
                        var componentGenerator = GetComponentGenerator
                        (
                            componentDefinition.ComponentType
                        );

                        var component = await componentGenerator.GenerateAsync
                        (
                            componentDefinition,
                            sectionType,
                            filter
                        );

                        componentList.Add(component);
                    }
                    catch (Exception ex)
                    {
                        errorMessages.Add
                        (
                            componentDefinition.Name,
                            ex.Message
                        );
                    }
                }

                watch.Stop();

                var executionTime = watch.ElapsedMilliseconds;

                if (errorMessages.Any())
                {
                    return new ReportSectionGenerationResult
                    (
                        executionTime,
                        errorMessages
                    );
                }
                else
                {
                    var section = new ReportSection
                    (
                        sectionDefinition.Title,
                        sectionDefinition.Description,
                        sectionType,
                        componentList.ToArray()
                    );

                    return new ReportSectionGenerationResult
                    (
                        executionTime,
                        section
                    );
                }
            }
        }

        /// <summary>
        /// Compiles all errors generated by sections into a single dictionary
        /// </summary>
        /// <param name="results">The section generation results</param>
        /// <returns>A dictionary of errors</returns>
        private Dictionary<string, string> CompileErrors
            (
                params ReportSectionGenerationResult[] results
            )
        {
            var errorMessages = new Dictionary<string, string>();

            foreach (var result in results)
            {
                if (false == result.Success)
                {
                    foreach (var error in result.ErrorMessages)
                    {
                        errorMessages.Add
                        (
                            error.Key,
                            error.Value
                        );
                    }
                }
            }

            return errorMessages;
        }

        /// <summary>
        /// Gets a component generator for a specified component type
        /// </summary>
        /// <param name="componentType">The component type</param>
        /// <returns>The component generator</returns>
        private IReportComponentGenerator GetComponentGenerator
            (
                ReportComponentType componentType
            )
        {
            switch (componentType)
            {
                case ReportComponentType.Chart:
                    return new ChartGenerator();

                case ReportComponentType.Graphic:
                    throw new NotSupportedException();
                
                case ReportComponentType.Statistic:
                    return new StatisticGenerator();

                case ReportComponentType.Repeater:
                    throw new NotSupportedException();

                case ReportComponentType.Table:
                    throw new NotSupportedException();

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
