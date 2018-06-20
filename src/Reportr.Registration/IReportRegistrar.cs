namespace Reportr.Registration
{
    using System.Collections.Generic;
    
    /// <summary>
    /// Defines a contract for a report registrar
    /// </summary>
    public interface IReportRegistrar
    {
        /// <summary>
        /// Registers a single report with a builder source
        /// </summary>
        /// <param name="name">The name</param>
        /// <param name="title">The title</param>
        /// <param name="description">The description</param>
        /// <param name="builder">The definition builder</param>
        void RegisterReport
        (
            string name,
            string title,
            string description,
            IReportDefinitionBuilder builder
        );

        /// <summary>
        /// Registers a single report with a script source
        /// </summary>
        /// <param name="name">The name</param>
        /// <param name="title">The title</param>
        /// <param name="description">The description</param>
        /// <param name="scriptSourceCode">The script source code</param>
        void RegisterReport
        (
            string name,
            string title,
            string description,
            string scriptSourceCode
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
        /// Gets all registered reports
        /// </summary>
        /// <param name="categoryName">The category name</param>
        /// <returns>A collection of registered reports</returns>
        IEnumerable<RegisteredReport> GetReportsByCategory
        (
            string categoryName
        );

        /// <summary>
        /// Specifies the report definition source as a builder
        /// </summary>
        /// <param name="name">The name of the report</param>
        /// <param name="builder">The definition builder</param>
        void SpecifySource
        (
            string name,
            IReportDefinitionBuilder builder
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
        /// De-registers a single report
        /// </summary>
        /// <param name="name">The name of the report</param>
        void DeregisterReport
        (
            string name
        );
    }
}
