namespace Reportr.Metrics
{
    using System.Collections.Generic;

    /// <summary>
    /// Defines a contract for a single two-dimensional chart
    /// </summary>
    public interface IChart
    {
        /// <summary>
        /// Gets the name of the chart
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets the intended chart type
        /// </summary>
        ChartType IntendedType { get; }
        
        /// <summary>
        /// Gets a flag indicating if decimal places should be allowed
        /// </summary>
        bool AllowDecimals { get; }
        
        /// <summary>
        /// Gets an array of parameters accepted by the chart
        /// </summary>
        ParameterInfo[] Parameters { get; }

        /// <summary>
        /// Generates a single chart
        /// </summary>
        /// <param name="parameterValues">The parameter values</param>
        /// <returns>The chart generated</returns>
        ChartResult Generate
        (
            Dictionary<string, object> parameterValues
        );
    }
}
