namespace Reportr
{
    using System.Threading.Tasks;

    /// <summary>
    /// Defines a contract for a single report component
    /// </summary>
    public interface IReportComponent<TOutput> : IReportComponent
        where TOutput : IReportComponentOutput
    {
        /// <summary>
        /// Executes the component using the parameter values supplied
        /// </summary>
        /// <param name="parameterValues">The parameter values</param>
        /// <returns>The output generated</returns>
        new TOutput Execute
        (
            params ParameterValue[] parameterValues
        );

        /// <summary>
        /// Asynchronously executes the component using the parameter values supplied
        /// </summary>
        /// <param name="parameterValues">The parameter values</param>
        /// <returns>The output generated</returns>
        new Task<TOutput> ExecuteAsync
        (
            params ParameterValue[] parameterValues
        );
    }
}
