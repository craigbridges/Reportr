namespace Reportr
{
    using Nito.AsyncEx.Synchronous;
    using Reportr.Components;
    using Reportr.Filtering;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Threading.Tasks;

    /// <summary>
    /// Represents the default implementation of the report generator 
    /// </summary>
    public sealed class ReportGenerator : IReportGenerator
    {
        private readonly IReportFilterGenerator _filterGenerator;

        /// <summary>
        /// Constructs the report generator with required dependencies
        /// </summary>
        /// <param name="filterGenerator">The filter generator</param>
        public ReportGenerator
            (
                IReportFilterGenerator filterGenerator
            )
        {
            Validate.IsNotNull(filterGenerator);

            _filterGenerator = filterGenerator;
        }

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
            var task = GenerateAsync(definition, filter);

            return task.WaitAndUnwrapException();
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
                filter = _filterGenerator.Generate
                (
                    definition
                );
            }

            var pageHeaderTask = GenerateSectionAsync
            (
                definition,
                filter,
                ReportSectionType.PageHeader
            );

            var reportHeaderTask = GenerateSectionAsync
            (
                definition,
                filter,
                ReportSectionType.ReportHeader
            );

            var reportBodyTask = GenerateSectionAsync
            (
                definition,
                filter,
                ReportSectionType.ReportBody
            );

            var pageFooterTask = GenerateSectionAsync
            (
                definition,
                filter,
                ReportSectionType.PageFooter
            );

            var reportFooterTask = GenerateSectionAsync
            (
                definition,
                filter,
                ReportSectionType.ReportFooter
            );

            await Task.WhenAll
            (
                pageHeaderTask,
                reportHeaderTask,
                reportBodyTask,
                reportFooterTask,
                pageFooterTask
            )
            .ConfigureAwait
            (
                false
            );
            
            var pageHeaderResult = await pageHeaderTask.ConfigureAwait
            (
                false
            );

            var reportHeaderResult = await reportHeaderTask.ConfigureAwait
            (
                false
            );

            var reportBodyResult = await reportBodyTask.ConfigureAwait
            (
                false
            );

            var pageFooterResult = await pageFooterTask.ConfigureAwait
            (
                false
            );

            var reportFooterResult = await reportFooterTask.ConfigureAwait
            (
                false
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
                var generationTasks = new Dictionary<string, Task<IReportComponent>>();
                var componentList = new List<IReportComponent>();
                var errorMessages = new Dictionary<string, string>();

                // Build a dictionary of component generation tasks
                foreach (var componentDefinition in sectionDefinition.Components)
                {
                    var componentName = componentDefinition.Name;

                    var isExcluded = filter.IsExcluded
                    (
                        componentName
                    );

                    if (false == isExcluded)
                    {
                        var componentType = componentDefinition.ComponentType;
                        var componentGenerator = componentType.GetGenerator();

                        var task = componentGenerator.GenerateAsync
                        (
                            componentDefinition,
                            sectionType,
                            filter
                        );

                        generationTasks.Add
                        (
                            componentName,
                            task
                        );
                    }
                }

                await Task.WhenAll
                (
                    generationTasks.Select(pair => pair.Value)
                )
                .ConfigureAwait
                (
                    false
                );

                // Compile the results of each task once they have completed
                foreach (var item in generationTasks)
                {
                    try
                    {
                        componentList.Add
                        (
                            await item.Value.ConfigureAwait(false)
                        );
                    }
                    catch (Exception ex)
                    {
                        errorMessages.Add
                        (
                            item.Key,
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
    }
}
