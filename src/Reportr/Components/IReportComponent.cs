namespace Reportr.Components
{
    using System.Threading.Tasks;

    /// <summary>
    /// Defines a contract for a single report component
    /// </summary>
    public interface IReportComponent
    {
        /// <summary>
        /// Gets the name of the component
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets the report component type
        /// </summary>
        ReportComponentType ComponentType { get; }

        /// <summary>
        /// Gets an array of parameters accepted by the component
        /// </summary>
        ParameterInfo[] Parameters { get; }

        /// <summary>
        /// Executes the component using the parameter values supplied
        /// </summary>
        /// <param name="parameterValues">The parameter values</param>
        /// <returns>The output generated</returns>
        IReportComponentOutput Execute
        (
            params ParameterValue[] parameterValues
        );

        /// <summary>
        /// Asynchronously executes the component using the parameter values supplied
        /// </summary>
        /// <param name="parameterValues">The parameter values</param>
        /// <returns>The output generated</returns>
        Task<IReportComponentOutput> ExecuteAsync
        (
            params ParameterValue[] parameterValues
        );
    }
}
