namespace Reportr.Registration
{
    using Reportr.Data.Querying;
    
    /// <summary>
    /// Defines a contract for a registered report definition builder
    /// </summary>
    public interface IRegisteredReportDefinitionBuilder
    {
        /// <summary>
        /// Builds the report definition
        /// </summary>
        /// <param name="registeredReport">The registered report</param>
        /// <param name="queryRepository">The query repository</param>
        /// <returns>The report definition generated</returns>
        ReportDefinition Build
        (
            RegisteredReport registeredReport,
            IQueryRepository queryRepository
        );
    }
}
