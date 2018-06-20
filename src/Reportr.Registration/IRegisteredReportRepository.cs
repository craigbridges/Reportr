namespace Reportr.Registration
{
    using System.Collections.Generic;
    
    /// <summary>
    /// Defines a contract for a repository that manages registered reports
    /// </summary>
    public interface IRegisteredReportRepository
    {
        /// <summary>
        /// Adds a single registered report to the repository
        /// </summary>
        /// <param name="report">The registered report</param>
        void AddReport
        (
            RegisteredReport report
        );

        /// <summary>
        /// Determines if a report has been registered
        /// </summary>
        /// <param name="name">The name of the report</param>
        /// <returns>True, if a match was found; otherwise false</returns>
        bool IsRegistered
        (
            string name
        );

        /// <summary>
        /// Gets a single registered report from the repository
        /// </summary>
        /// <param name="name">The name of the report</param>
        /// <returns>The matching registered report</returns>
        RegisteredReport GetReport
        (
            string name
        );

        /// <summary>
        /// Gets all registered reports in the repository
        /// </summary>
        /// <returns>A collection of registered reports</returns>
        IEnumerable<RegisteredReport> GetAllReports();

        /// <summary>
        /// Updates a single registered report in the repository
        /// </summary>
        /// <param name="report">The registered report to update</param>
        void UpdateReport
        (
            RegisteredReport report
        );

        /// <summary>
        /// Removes a single registered report from the repository
        /// </summary>
        /// <param name="name">The name of the report</param>
        void RemoveReport
        (
            string name
        );
    }
}
