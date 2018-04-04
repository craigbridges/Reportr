namespace Reportr
{
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
    }
}
