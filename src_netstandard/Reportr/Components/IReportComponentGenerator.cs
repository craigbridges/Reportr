namespace Reportr.Components
{
    using Reportr.Filtering;
    using System.Threading.Tasks;

    /// <summary>
    /// Defines a contract for a report component generator
    /// </summary>
    public interface IReportComponentGenerator
    {
        /// <summary>
        /// Generates a component from a report definition and filter
        /// </summary>
        /// <param name="definition">The component definition</param>
        /// <param name="sectionType">The report section type</param>
        /// <param name="filter">The report filter</param>
        /// <returns>The report component generated</returns>
        IReportComponent Generate
        (
            IReportComponentDefinition definition,
            ReportSectionType sectionType,
            ReportFilter filter
        );

        /// <summary>
        /// Asynchronously generates a component from a report definition and filter
        /// </summary>
        /// <param name="definition">The component definition</param>
        /// <param name="sectionType">The report section type</param>
        /// <param name="filter">The report filter</param>
        /// <returns>The report component generated</returns>
        Task<IReportComponent> GenerateAsync
        (
            IReportComponentDefinition definition,
            ReportSectionType sectionType,
            ReportFilter filter
        );
    }
}
