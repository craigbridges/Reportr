namespace Reportr
{
    using System;
    
    /// <summary>
    /// Provides various extension methods for reports
    /// </summary>
    public static class ReportExtensions
    {
        /// <summary>
        /// Generates a default filter for the report
        /// </summary>
        /// <param name="report">The report</param>
        /// <returns>The filter generated</returns>
        public static IReportFilter GenerateDefaultFilter
            (
                this IReport report
            )
        {
            Validate.IsNotNull(report);

            throw new NotImplementedException();
        }
    }
}
