namespace Reportr
{
    using Reportr.Data.Querying;

    /// <summary>
    /// Defines a contract for a service that builds report definitions
    /// </summary>
    public interface IReportDefinitionBuilder
    {
        /// <summary>
        /// Builds the report definition
        /// </summary>
        /// <param name="queryRepository">The query repository</param>
        /// <returns>The report definition generated</returns>
        ReportDefinition Build
        (
            IQueryRepository queryRepository
        );
    }
}
