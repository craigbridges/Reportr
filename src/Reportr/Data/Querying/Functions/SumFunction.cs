namespace Reportr.Data.Querying.Functions
{
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Represents a query aggregate function that calculates the sum
    /// </summary>
    public class SumFunction : AggregateFunctionBase
    {
        /// <summary>
        /// Constructs the function without a data binding
        /// </summary>
        /// <param name="autoRoundResult">True, to auto round the result</param>
        public SumFunction
            (
                bool autoRoundResult = false
            )
            : base(autoRoundResult)
        { }

        /// <summary>
        /// Constructs the function with a data binding
        /// </summary>
        /// <param name="binding">The data binding</param>
        /// <param name="autoRoundResult">True, to auto round the result</param>
        public SumFunction
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
            return numbers.Sum();
        }
    }
}
