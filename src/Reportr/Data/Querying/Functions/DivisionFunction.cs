namespace Reportr.Data.Querying.Functions
{
    using Reportr.Data;
    using Reportr.Data.Querying;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Represents a query aggregate function that calculates divides two sums
    /// </summary>
    public class DivisionFunction : AggregateFunctionBase
    {
        /// <summary>
        /// Constructs the function with a data binding
        /// </summary>
        /// <param name="leftNumberBinding">The left number binding</param>
        /// <param name="rightNumberBinding">The right number binding</param>
        /// <param name="autoRoundResult">True, to auto round the result</param>
        public DivisionFunction
            (
                DataBinding leftNumberBinding,
                DataBinding rightNumberBinding,
                bool autoRoundResult = false
            )

            : base(leftNumberBinding, autoRoundResult)
        {
            Validate.IsNotNull(rightNumberBinding);

            this.DivisorBinding = rightNumberBinding;
        }

        /// <summary>
        /// Gets the divisor (or second number) data binding
        /// </summary>
        public DataBinding DivisorBinding { get; private set; }

        /// <summary>
        /// Executes the aggregate function and computes the result
        /// </summary>
        /// <param name="rows">The rows to perform the computation on</param>
        /// <returns>The result computed</returns>
        public override double Execute
            (
                params QueryRow[] rows
            )
        {
            var leftBinding = this.Binding;
            var rightBinding = this.DivisorBinding;

            var leftNumber = Execute
            (
                leftBinding,
                rows
            );

            var rightNumber = Execute
            (
                rightBinding,
                rows
            );

            var result = default(double);

            if (leftNumber > 0 && rightNumber > 0)
            {
                result = leftNumber / rightNumber;
            }

            if (this.AutoRoundResult)
            {
                return Math.Round(result, 2);
            }
            else
            {
                return result;
            }
        }

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
