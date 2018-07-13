namespace Reportr.Registration
{
    using Reportr.Registration.Authorization;
    using System.Collections.Specialized;
    using System.Threading.Tasks;

    /// <summary>
    /// Defines a contract for a service that generates registered reports
    /// </summary>
    public interface IRegisteredReportGenerator
    {
        /// <summary>
        /// Generates a report from a registered report and filter values
        /// </summary>
        /// <param name="reportName">The report name</param>
        /// <param name="filterValues">The filter parameter values</param>
        /// <param name="userInfo">The user information</param>
        /// <returns>The generated result</returns>
        ReportGenerationResult Generate
        (
            string reportName,
            NameValueCollection filterValues,
            ReportUserInfo userInfo
        );

        /// <summary>
        /// Asynchronously generates a report from a registered report and filter values
        /// </summary>
        /// <param name="reportName">The report name</param>
        /// <param name="filterValues">The filter parameter values</param>
        /// <param name="userInfo">The user information</param>
        /// <returns>The generation result</returns>
        Task<ReportGenerationResult> GenerateAsync
        (
            string reportName,
            NameValueCollection filterValues,
            ReportUserInfo userInfo
        );
    }
}
