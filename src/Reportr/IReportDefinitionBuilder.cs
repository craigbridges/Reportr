namespace Reportr
{
    /// <summary>
    /// Defines a contract for a service that builds report definitions
    /// </summary>
    public interface IReportDefinitionBuilder
    {
        /// <summary>
        /// Builds the report definition
        /// </summary>
        /// <returns>The report definition generated</returns>
        ReportDefinition Build();
    }
}
