namespace Reportr.Metrics
{
    using System.Collections.Generic;

    /// <summary>
    /// Defines a contract for a single statistic
    /// </summary>
    public interface IStatistic
    {
        /// <summary>
        /// Gets the name of the statistic
        /// </summary>
        string Name { get; }
        
        /// <summary>
        /// Gets the statistic type
        /// </summary>
        StatisticType StatisticType { get; }

        /// <summary>
        /// Gets an array of parameters accepted by the statistic
        /// </summary>
        ParameterInfo[] Parameters { get; }

        /// <summary>
        /// Calculates the statistic value
        /// </summary>
        /// <param name="parameterValues">The parameter values</param>
        /// <returns>The statistic result</returns>
        StatisticResult Calculate
        (
            Dictionary<string, object> parameterValues
        );
    }
}
