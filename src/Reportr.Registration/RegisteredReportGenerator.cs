namespace Reportr.Registration
{
    using Nito.AsyncEx.Synchronous;
    using Reportr.Registration.Authorization;
    using System;
    using System.Collections.Specialized;
    using System.Threading.Tasks;

    /// <summary>
    /// Represents the default registered report generator implementation
    /// </summary>
    public sealed class RegisteredReportGenerator : IRegisteredReportGenerator
    {
        /// <summary>
        /// Generates a report from a registered report and filter values
        /// </summary>
        /// <param name="reportName">The report name</param>
        /// <param name="filterValues">The filter parameter values</param>
        /// <param name="userInfo">The user information</param>
        /// <returns>The generated result</returns>
        public ReportGenerationResult Generate
            (
                string reportName,
                NameValueCollection filterValues,
                ReportUserInfo userInfo
            )
        {
            var task = GenerateAsync
            (
                reportName,
                filterValues,
                userInfo
            );

            return task.WaitAndUnwrapException();
        }

        /// <summary>
        /// Asynchronously generates a report from a registered report and filter values
        /// </summary>
        /// <param name="reportName">The report name</param>
        /// <param name="filterValues">The filter parameter values</param>
        /// <param name="userInfo">The user information</param>
        /// <returns>The generation result</returns>
        public Task<ReportGenerationResult> GenerateAsync
            (
                string reportName,
                NameValueCollection filterValues,
                ReportUserInfo userInfo
            )
        {
            Validate.IsNotEmpty(reportName);
            Validate.IsNotNull(filterValues);
            Validate.IsNotNull(userInfo);

            // TODO: validate user access to report
            // TODO: get registered report
            // TODO: build registered report into definition
            // TODO: compile filter values into report filter
            // TODO: set user restrictions in filter
            // TODO: call IReportGenerator generate method and return result

            throw new NotImplementedException();
        }
    }
}
