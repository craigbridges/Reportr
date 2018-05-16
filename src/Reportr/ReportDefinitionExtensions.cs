namespace Reportr
{
    using System;
    
    /// <summary>
    /// Provides various extension methods for report definitions
    /// </summary>
    public static class ReportDefinitionExtensions
    {
        /// <summary>
        /// Generates a default filter for the report definition
        /// </summary>
        /// <param name="report">The report definition</param>
        /// <returns>The filter generated</returns>
        public static ReportFilter GenerateDefaultFilter
            (
                this ReportDefinition report
            )
        {
            Validate.IsNotNull(report);

            throw new NotImplementedException();
        }
    }
}
