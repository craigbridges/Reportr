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

            var sectionResults = new ReportSectionGenerationResult[]
            {
                pageHeaderResult,
                reportHeaderResult,
                reportBodyResult,
                reportFooterResult,
                pageFooterResult
            };

            var errorMessages = CompileErrorMessages
            (
                sectionResults
            );

            var handledExceptions = CompileHandledExceptions
            (
                sectionResults
            );

            watch.Stop();
            
            var result = new ReportGenerationResult
            (
                report,
                watch.ElapsedMilliseconds,
                errorMessages
            );

            if (handledExceptions.Any())
            {
                result.AddExceptions(handledExceptions);
            }

            return result;
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
                var errorMessages = new List<string>();
                var handledExceptions = new List<Exception>();

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

                try
                {
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
                        componentList.Add
                        (
                            await item.Value.ConfigureAwait(false)
                        );
                    }
                }
                catch (Exception ex)
                {
                    errorMessages.Add(ex.Message);
                    handledExceptions.Add(ex);
                }

                watch.Stop();
                
                var section = new ReportSection
                (
                    sectionDefinition.Title,
                    sectionDefinition.Description,
                    sectionType,
                    componentList.ToArray()
                );

                var result = new ReportSectionGenerationResult
                (
                    section,
                    watch.ElapsedMilliseconds,
                    errorMessages.ToArray()
                );

                if (handledExceptions.Any())
                {
                    result.AddExceptions(handledExceptions);
                }

                return result;
            }
        }

        /// <summary>
        /// Compiles all errors generated by sections into a single dictionary
        /// </summary>
        /// <param name="results">The section generation results</param>
        /// <returns>An array of error messages</returns>
        private string[] CompileErrorMessages
            (
                params ReportSectionGenerationResult[] results
            )
        {
            var errorMessages = new List<string>();

            foreach (var result in results)
            {
                if (result != null && false == result.Success)
                {
                    errorMessages.AddRange
                    (
                        result.ErrorMessages
                    );
                }
            }

            return errorMessages.ToArray();
        }

        /// <summary>
        /// Compiles all exceptions handled by sections into a single dictionary
        /// </summary>
        /// <param name="results">The section generation results</param>
        /// <returns>An array of exceptions</returns>
        private IEnumerable<Exception> CompileHandledExceptions
            (
                params ReportSectionGenerationResult[] results
            )
        {
            var exceptions = new List<Exception>();

            foreach (var result in results)
            {
                if (result != null && result.HasHandledExceptions)
                {
                    exceptions.AddRange
                    (
                        result.HandledExceptions
                    );
                }
            }

            return exceptions.ToArray();
        }
    }
}
