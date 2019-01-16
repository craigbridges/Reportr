namespace Reportr.Components.Collections
{
    using Reportr.Data;
    using Reportr.Data.Querying;
    using Reportr.Filtering;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    /// <summary>
    /// Represents a report repeater generator
    /// </summary>
    public class RepeaterGenerator : ReportComponentGeneratorBase
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

            var repeaterDefinition = definition.As<RepeaterDefinition>();
            var query = repeaterDefinition.Query;
            var defaultParameters = repeaterDefinition.DefaultParameterValues;

            var parameters = filter.GetQueryParameters
            (
                query,
                defaultParameters.ToArray()
            );

            var queryTask = query.ExecuteAsync
            (
                parameters.ToArray()
            );

            var results = await queryTask.ConfigureAwait
            (
                false
            );

            var generatedItems = new List<RepeaterItem>();
            var itemTasks = new List<Task<RepeaterItem>>();
            
            foreach (var row in results.AllRows)
            {
                itemTasks.Add
                (
                    GenerateItemAsync
                    (
                        repeaterDefinition,
                        sectionType,
                        filter,
                        row
                    )
                );
            }

            await Task.WhenAll
            (
                itemTasks.ToArray()
            )
            .ConfigureAwait
            (
                false
            );
            
            foreach (var task in itemTasks)
            {
                generatedItems.Add
                (
                    await task.ConfigureAwait(false)
                );
            }

            if (false == repeaterDefinition.DisableSorting)
            {
                var sortDirection = filter.FindSortDirection
                (
                    sectionType,
                    definition.Name,
                    "Item"
                );

                // Apply the sorting direction, if it has been specified
                if (sortDirection != null)
                {
                    if (sortDirection.Value == SortDirection.Ascending)
                    {
                        generatedItems = generatedItems.OrderBy
                        (
                            a => a.Value
                        )
                        .ToList();
                    }
                    else
                    {
                        generatedItems = generatedItems.OrderByDescending
                        (
                            a => a.Value
                        )
                        .ToList();
                    }
                }
            }

            var repeater = new Repeater
            (
                repeaterDefinition,
                generatedItems.ToArray()
            );

            return repeater;
        }

        /// <summary>
        /// Asynchronously generates a single repeater item for a query row
        /// </summary>
        /// <param name="definition">The repeater definition</param>
        /// <param name="sectionType">The section type</param>
        /// <param name="filter">The report filter</param>
        /// <param name="row">The query row</param>
        /// <returns>The repeater item generated</returns>
        private async Task<RepeaterItem> GenerateItemAsync
            (
                RepeaterDefinition definition,
                ReportSectionType sectionType,
                ReportFilter filter,
                QueryRow row
            )
        {
            var value = definition.Binding.Resolve(row);
            var action = default(ReportAction);

            if (definition.Action != null)
            {
                action = definition.Action.Resolve(row);
            }

            var nestedComponents = await GenerateNestedComponentsAsync
            (
                definition,
                sectionType,
                filter,
                row
            );

            return new RepeaterItem
            (
                value,
                action,
                nestedComponents
            );
        }

        /// <summary>
        /// Asynchronously generates a nested components for a query row
        /// </summary>
        /// <param name="repeaterDefinition">The repeater definition</param>
        /// <param name="sectionType">The section type</param>
        /// <param name="parentFilter">The parent report filter</param>
        /// <param name="row">The query row</param>
        /// <returns>The nested components generated</returns>
        private async Task<IReportComponent[]> GenerateNestedComponentsAsync
            (
                RepeaterDefinition repeaterDefinition,
                ReportSectionType sectionType,
                ReportFilter parentFilter,
                QueryRow row
            )
        {
            var generationTasks = new List<Task<IReportComponent>>();
            var componentList = new List<IReportComponent>();
            
            foreach (var nestedComponent in repeaterDefinition.NestedComponents)
            {
                var componentDefinition = nestedComponent.Definition;
                var componentName = componentDefinition.Name;

                var isExcluded = parentFilter.IsExcluded
                (
                    componentName
                );

                if (false == isExcluded)
                {
                    var componentType = componentDefinition.ComponentType;
                    var componentGenerator = componentType.GetGenerator();
                    
                    var nestedFilter = nestedComponent.GenerateNestedFilter
                    (
                        parentFilter,
                        row
                    );

                    var task = componentGenerator.GenerateAsync
                    (
                        componentDefinition,
                        sectionType,
                        nestedFilter
                    );

                    generationTasks.Add(task);
                }
            }

            await Task.WhenAll
            (
                generationTasks.ToArray()
            )
            .ConfigureAwait
            (
                false
            );
            
            foreach (var task in generationTasks)
            {
                componentList.Add
                (
                    await task.ConfigureAwait(false)
                );
            }

            return componentList.ToArray();
        }
    }
}
