namespace Reportr.Components.Collections
{
    using Reportr.Filtering;
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

            var results = await query.ExecuteAsync
            (
                parameters.ToArray()
            );

            var items = new List<RepeaterItem>();
            var binding = repeaterDefinition.Binding;
            var actionDefinition = repeaterDefinition.Action;

            foreach (var row in results.AllRows)
            {
                var value = binding.Resolve(row);
                var action = default(ReportAction);

                if (repeaterDefinition.Action != null)
                {
                    action = actionDefinition.Resolve(row);
                }

                items.Add
                (
                    new RepeaterItem(value, action)
                );

                // TODO: generate all nested components (if there are any) using row data in addition to the report filter
            }

            var repeater = new Repeater
            (
                repeaterDefinition,
                items.ToArray()
            );

            return repeater;
        }
    }
}
