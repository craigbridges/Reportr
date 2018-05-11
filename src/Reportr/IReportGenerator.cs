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
        /// <returns>The report generated</returns>
        ReportOutput Generate
        (
            ReportDefinition definition,
            IReportFilter filter = null
        );

        /// <summary>
        /// Asynchronously generates a report using a report definition
        /// </summary>
        /// <param name="definition">The report definition</param>
        /// <param name="filter">The filter (optional)</param>
        /// <returns>The report generated</returns>
        Task<ReportOutput> GenerateAsync
        (
            ReportDefinition definition,
            IReportFilter filter = null
        );
    }
}
