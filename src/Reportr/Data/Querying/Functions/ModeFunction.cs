namespace Reportr.Data.Querying.Functions
{
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Represents a query aggregate function that calculates the mode
    /// </summary>
    public class ModeFunction : AggregateFunctionBase
    {
        /// <summary>
        /// Constructs the function with a query and binding
        /// </summary>
        /// <param name="binding">The data binding</param>
        public ModeFunction
            (
                DataBinding binding
            )
            : base(binding)
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
            var mode = numbers.GroupBy(n => n)
                              .OrderByDescending(g => g.Count())
                              .First()
                              .Key;

            return mode;
        }
    }
}
