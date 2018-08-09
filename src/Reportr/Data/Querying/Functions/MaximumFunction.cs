namespace Reportr.Data.Querying.Functions
{
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Represents a query aggregate function that calculates the maximum
    /// </summary>
    public class MaximumFunction : AggregateFunctionBase
    {
        /// <summary>
        /// Constructs the function with a query and binding
        /// </summary>
        /// <param name="binding">The data binding</param>
        /// <param name="autoRoundResult">True, to auto round the result</param>
        public MaximumFunction
            (
                DataBinding binding,
                bool autoRoundResult = false
            )
            : base(binding, autoRoundResult)
        { }

        /// <summary>
        /// Computes the function value from the multiple values specified
        /// </summary>
        /// <param name="numbers">The numbers to use in the calculation</param>
        /// <returns>The computed value</returns>
        protected override double Compute
            (
                List<double> numbers
            )
        {
            return numbers.Max();
        }
    }
}
