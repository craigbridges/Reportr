namespace Reportr.Registration
{
    using Reportr.Registration.Authorization;
    using System;
    using System.Collections.Generic;
    
    /// <summary>
    /// Defines a contract for a report registrar
    /// </summary>
    public interface IReportRegistrar
    {
        /// <summary>
        /// Registers a single report with a builder source
        /// </summary>
        /// <typeparam name="TBuilder">The builder type</typeparam>
        /// <param name="configuration">The report configuration</param>
        void RegisterReport<TBuilder>
        (
            RegisteredReportConfiguration configuration
        )
        where TBuilder : IReportDefinitionBuilder;

        /// <summary>
        /// Registers a single report with a builder source
        /// </summary>
        /// <param name="configuration">The report configuration</param>
        /// <param name="builderType">The report builder type</param>
        void RegisterReport
        (
            RegisteredReportConfiguration configuration,
            Type builderType
        );

        /// <summary>
        /// Registers a single report with a script source
        /// </summary>
        /// <param name="configuration">The report configuration</param>
        /// <param name="scriptSourceCode">The script source code</param>
        void RegisterReport
        (
            RegisteredReportConfiguration configuration,
            string scriptSourceCode
        );

        /// <summary>
        /// Auto registers multiple reports
        /// </summary>
        /// <param name="configurations">The report configurations</param>
        void AutoRegisterReports
        (
            params AutoRegisteredReportConfiguration[] configurations
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
        /// Gets a single registered report
        /// </summary>
        /// <param name="name">The name of the report</param>
        /// <returns>The matching registered report</returns>
        RegisteredReport GetReport
        (
            string name
        );

        /// <summary>
        /// Gets all registered reports
        /// </summary>
        /// <returns>A collection of registered reports</returns>
        IEnumerable<RegisteredReport> GetAllReports();

        /// <summary>
        /// Gets all registered reports in a category
        /// </summary>
        /// <param name="categoryName">The category name</param>
        /// <returns>A collection of registered reports</returns>
        IEnumerable<RegisteredReport> GetReportsByCategory
        (
            string categoryName
        );

        /// <summary>
        /// Gets all registered reports for a single user
        /// </summary>
        /// <param name="userInfo">The user information</param>
        /// <returns>A collection of registered reports</returns>
        IEnumerable<RegisteredReport> GetReportsForUser
        (
            ReportUserInfo userInfo
        );

        /// <summary>
        /// Gets all registered reports for a user and category
        /// </summary>
        /// <param name="userInfo">The user information</param>
        /// <param name="categoryName">The category string</param>
        /// <returns>A collection of registered reports</returns>
        IEnumerable<RegisteredReport> GetReportsForUser
        (
            ReportUserInfo userInfo,
            string categoryName
        );

        /// <summary>
        /// Specifies the report definition source as a builder
        /// </summary>
        /// <typeparam name="TBuilder">The builder type</typeparam>
        /// <param name="name">The name of the report</param>
        void SpecifyBuilder<TBuilder>
        (
            string name
        )
        where TBuilder : IReportDefinitionBuilder;

        /// <summary>
        /// Specifies the report definition source as a builder
        /// </summary>
        /// <param name="name">The name of the report</param>
        /// <param name="builderType">The report builder type</param>
        void SpecifyBuilder
        (
            string name,
            Type builderType
        );

        /// <summary>
        /// Specifies the report definition source as a script
        /// </summary>
        /// <param name="name">The name of the report</param>
        /// <param name="scriptSourceCode">The script source code</param>
        void SpecifySource
        (
            string name,
            string scriptSourceCode
        );

        /// <summary>
        /// Disables a single registered report
        /// </summary>
        /// <param name="name">The name of the report</param>
        void DisableReport
        (
            string name
        );

        /// <summary>
        /// Automatically disables multiple registered reports
        /// </summary>
        /// <param name="reportNames">The report names</param>
        void AutoDisableReports
        (
            params string[] reportNames
        );

        /// <summary>
        /// Enables a single registered report
        /// </summary>
        /// <param name="name">The name of the report</param>
        void EnableReport
        (
            string name
        );

        /// <summary>
        /// Automatically enables multiple registered reports
        /// </summary>
        /// <param name="reportNames">The report names</param>
        void AutoEnableReports
        (
            params string[] reportNames
        );

        /// <summary>
        /// De-registers a single report
        /// </summary>
        /// <param name="name">The name of the report</param>
        void DeregisterReport
        (
            string name
        );
    }
}
