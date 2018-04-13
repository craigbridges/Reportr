namespace Reportr
{
    using System.Globalization;
    
    /// <summary>
    /// Defines a contract for the output of a report
    /// </summary>
    public interface IReportOutput : IReportResult
    {
        /// <summary>
        /// Gets the reports name
        /// </summary>
        string ReportName { get; }

        /// <summary>
        /// Gets the reports title
        /// </summary>
        string ReportTitle { get; }

        /// <summary>
        /// Gets the reports description
        /// </summary>
        string ReportDescription { get; }

        /// <summary>
        /// Gets the culture used by the report
        /// </summary>
        CultureInfo Culture { get; }
        
        /// <summary>
        /// Gets the filter that was used to generate the report
        /// </summary>
        IReportFilter FilterUsed { get; }

        /// <summary>
        /// Gets an array of the reports section outputs
        /// </summary>
        IReportComponentOutput[] SectionOutputs { get; }
    }
}
