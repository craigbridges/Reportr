namespace Reportr
{
    using Reportr.Templates;
    using System.Collections.Generic;
    using System.Globalization;
    
    /// <summary>
    /// Defines a contract for the output of a report
    /// </summary>
    public interface IReportOutput
    {
        /// <summary>
        /// Gets the reports name
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets the reports title
        /// </summary>
        string Title { get; }

        /// <summary>
        /// Gets the reports description
        /// </summary>
        string Description { get; }

        /// <summary>
        /// Gets the culture used by the report
        /// </summary>
        CultureInfo Culture { get; }

        /// <summary>
        /// Gets the number of columns in the report
        /// </summary>
        /// <remarks>
        /// The column count is used to determine how the report
        /// is laid out. A column count of 1 indicates that each
        /// section will appear on a separate row.
        /// 
        /// Reports with column counts greater than 1 will 
        /// automatically push sections onto new rows once the
        /// column count has been reached.
        /// </remarks>
        int? ColumnCount { get; }

        /// <summary>
        /// Gets an array of the reports section outputs
        /// </summary>
        IReportComponentOutput[] SectionOutputs { get; }

        /// <summary>
        /// Gets the reports rendered content
        /// </summary>
        string RenderedContent { get; }

        /// <summary>
        /// Gets the reports rendered content type
        /// </summary>
        TemplateOutputType? RenderedContentType { get; }

        /// <summary>
        /// Gets a flag indicating if then rendered content has been set
        /// </summary>
        bool HasRenderedContent { get; }

        /// <summary>
        /// Gets a flag indicating if the report ran successfully
        /// </summary>
        bool Success { get; }

        /// <summary>
        /// Gets the reports execution time
        /// </summary>
        int ExecutionTime { get; }

        /// <summary>
        /// Gets any error messages that were generated
        /// </summary>
        /// <remarks>
        /// The error messages are grouped by component name.
        /// </remarks>
        Dictionary<string, string> ErrorMessages { get; }
    }
}
