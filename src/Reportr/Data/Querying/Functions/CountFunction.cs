namespace Reportr.Data.Querying.Functions
{
    using System.Collections.Generic;

    /// <summary>
    /// Represents a query aggregate function that calculates the count
    /// </summary>
    public class CountFunction : AggregateFunctionBase
    {
        /// <summary>
        /// Constructs the function without a data binding
        /// </summary>
        /// <param name="autoRoundResult">True, to auto round the result</param>
        public CountFunction
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
        public CountFunction
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
            return numbers.Count;
        }
    }
}
