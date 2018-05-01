namespace Reportr.Components
{
    using Reportr.Data.Querying;

    /// <summary>
    /// Defines a contract for a report component output
    /// </summary>
    public interface IReportComponentOutput
    {
        /// <summary>
        /// Gets the component that generated the output
        /// </summary>
        IReportComponent Component { get; }

        /// <summary>
        /// Gets the query results used to generate the output
        /// </summary>
        QueryResults Results { get; }
    }
}
