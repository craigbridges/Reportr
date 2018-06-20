namespace Reportr.Registration
{
    using System;
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
        void AddRegisteredReport
        (
            RegisteredReport report
        );

        /// <summary>
        /// Gets a single registered report from the repository
        /// </summary>
        /// <param name="id">The ID of the registered report</param>
        /// <returns>The matching registered report</returns>
        RegisteredReport GetRegisteredReport
        (
            Guid id
        );

        /// <summary>
        /// Gets all registered reports in the repository
        /// </summary>
        /// <returns>A collection of registered reports</returns>
        IEnumerable<RegisteredReport> GetAllRegisteredReports();

        /// <summary>
        /// Updates a single registered report in the repository
        /// </summary>
        /// <param name="report">The registered report to update</param>
        void UpdateRegisteredReport
        (
            RegisteredReport report
        );

        /// <summary>
        /// Removes a single registered report from the repository
        /// </summary>
        /// <param name="id">The ID of the registered report</param>
        void RemoveRegisteredReport
        (
            Guid id
        );
    }
}
