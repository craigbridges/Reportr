namespace Genesis.Reporting.Querying.Functions
{
    using Reportr;
    using Reportr.Data;
    using Reportr.Data.Querying;
    using Reportr.Data.Querying.Functions;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Represents a query aggregate function that calculates divides two sums
    /// </summary>
    public class DivisionFunction : AggregateFunctionBase
    {
        /// <summary>
        /// Constructs the function with a query and binding
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
            var leftNumber = Execute
            (
                this.Binding,
                rows
            );

            var rightNumber = Execute
            (
                this.DivisorBinding,
                rows
            );

            var result = leftNumber / rightNumber;

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
