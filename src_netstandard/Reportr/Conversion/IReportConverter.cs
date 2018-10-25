namespace Reportr.Conversion
{
    /// <summary>
    /// Defines a contract for a report converter
    /// </summary>
    /// <typeparam name="TOutput">The output type</typeparam>
    public interface IReportConverter<TOutput>
    {
        /// <summary>
        /// Converts a report to the output type
        /// </summary>
        /// <param name="report">The report to convert</param>
        /// <returns>The output</returns>
        TOutput Convert
        (
            Report report
        );
    }
}
