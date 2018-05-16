namespace Reportr
{
    using System.Threading.Tasks;

    /// <summary>
    /// Defines a contract for a service that generates reports
    /// </summary>
    public interface IReportGenerator
    {
        /// <summary>
        /// Generates a report using a report definition
        /// </summary>
        /// <param name="definition">The report definition</param>
        /// <param name="filter">The filter (optional)</param>
        /// <returns>The generated result</returns>
        ReportGenerationResult Generate
        (
            ReportDefinition definition,
            ReportFilter filter = null
        );

        /// <summary>
        /// Asynchronously generates a report using a report definition
        /// </summary>
        /// <param name="definition">The report definition</param>
        /// <param name="filter">The filter (optional)</param>
        /// <returns>The generation result</returns>
        Task<ReportGenerationResult> GenerateAsync
        (
            ReportDefinition definition,
            ReportFilter filter = null
        );
    }
}
