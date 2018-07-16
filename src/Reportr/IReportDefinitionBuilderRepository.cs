namespace Reportr
{
    using System;
    using System.Collections.Generic;
    
    /// <summary>
    /// Defines a contract for a repository that manages report definition builders
    /// </summary>
    public interface IReportDefinitionBuilderRepository
    {
        /// <summary>
        /// Adds a report builder to the repository
        /// </summary>
        /// <param name="builder">The report builder</param>
        void AddBuilder
        (
            IReportDefinitionBuilder builder
        );

        /// <summary>
        /// Gets a report builder for the type specified
        /// </summary>
        /// <typeparam name="T">The builder type</typeparam>
        /// <returns>The matching report builder</returns>
        IReportDefinitionBuilder GetBuilder<T>();

        /// <summary>
        /// Gets a report builder for the type specified
        /// </summary>
        /// <param name="builderType">The builder type</param>
        /// <returns>The matching report builder</returns>
        IReportDefinitionBuilder GetBuilder
        (
            Type builderType
        );

        /// <summary>
        /// Gets a report builder for the type specified
        /// </summary>
        /// <param name="fullTypeName">The builder full type name</param>
        /// <returns>The matching report builder</returns>
        IReportDefinitionBuilder GetBuilder
        (
            string fullTypeName
        );

        /// <summary>
        /// Gets all report builders in the repository
        /// </summary>
        /// <returns>A collection of report builders</returns>
        IEnumerable<IReportDefinitionBuilder> GetAllBuilders();
    }
}
