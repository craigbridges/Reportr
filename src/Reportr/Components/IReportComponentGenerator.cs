namespace Reportr.Components
{
    using Reportr.Filtering;

    /// <summary>
    /// Defines a contract for a report component generator
    /// </summary>
    /// <typeparam name="TDefinition">The component definition</typeparam>
    /// <typeparam name="TComponent">The component type</typeparam>
    public interface IReportComponentGenerator<TDefinition, TComponent>
        where TDefinition : IReportComponentDefinition
        where TComponent : IReportComponent
    {
        /// <summary>
        /// Generates a component from a report definition and filter
        /// </summary>
        /// <param name="definition">The component definition</param>
        /// <param name="filter">The report filter</param>
        /// <returns>The report component generated</returns>
        TComponent Generate
        (
            TDefinition definition,
            ReportFilter filter
        );
    }
}
