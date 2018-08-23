namespace Reportr.Conversion.Csv
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    
    /// <summary>
    /// Represents a report converter that converts to CSV format
    /// </summary>
    public class ReportCsvConverter : IReportConverter<string>
    {
        /// <summary>
        /// Converts a report CSV
        /// </summary>
        /// <param name="report">The report to convert</param>
        /// <returns>The output</returns>
        public string Convert
            (
                Report report
            )
        {
            Validate.IsNotNull(report);

            // TODO: find all components that are tables
            // TODO: create unique list of columns from all tables
            // TODO: for each table, add a row to the CSV against the correct columns
            // TODO: convert above to a CSV string

            throw new NotImplementedException();
        }
    }
}
