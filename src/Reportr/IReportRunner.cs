namespace Reportr
{
    using System.Threading.Tasks;

    /// <summary>
    /// Defines a contract for a service that runs reports
    /// </summary>
    public interface IReportRunner
    {        
        /// <summary>
        /// Runs a report using the default filter
        /// </summary>
        /// <param name="report">The report</param>
        /// <returns>The report output</returns>
        IReportOutput Run
        (
            IReport report
        );

        /// <summary>
        /// Asynchronously runs a report using the default filter
        /// </summary>
        /// <param name="report">The report</param>
        /// <returns>The report output</returns>
        Task<IReportOutput> RunAsync
        (
            IReport report
        );

        /// <summary>
        /// Runs a report using the filter specified
        /// </summary>
        /// <param name="report">The report</param>
        /// <param name="filter">The filter</param>
        /// <returns>The report output</returns>
        IReportOutput Run
        (
            IReport report,
            IReportFilter filter
        );

        /// <summary>
        /// Asynchronously runs a report using the filter specified
        /// </summary>
        /// <param name="report">The report</param>
        /// <param name="filter">The filter</param>
        /// <returns>The report output</returns>
        Task<IReportOutput> RunAsync
        (
            IReport report,
            IReportFilter filter
        );
    }
}
