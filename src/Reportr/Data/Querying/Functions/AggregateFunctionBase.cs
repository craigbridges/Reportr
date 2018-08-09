namespace Reportr.Data.Querying.Functions
{
    using Nito.AsyncEx.Synchronous;
    using Reportr.Filtering;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    /// <summary>
    /// Represents a base class for all aggregate functions
    /// </summary>
    public abstract class AggregateFunctionBase : IAggregateFunction
    {
        /// <summary>
        /// Constructs the function with a query and binding
        /// </summary>
        /// <param name="binding">The data binding</param>
        /// <param name="autoRoundResult">True, to auto round the result</param>
        public AggregateFunctionBase
            (
                DataBinding binding,
                bool autoRoundResult = false
            )
        {
            Validate.IsNotNull(binding);
            
            this.Binding = binding;
            this.AutoRoundResult = autoRoundResult;
        }

        /// <summary>
        /// Gets the query field binding for the operation
        /// </summary>
        public DataBinding Binding { get; private set; }

        /// <summary>
        /// Gets a flag indicating of the result should automatically be rounded
        /// </summary>
        /// <remarks>
        /// If true, the result is rounded to two decimal places.
        /// </remarks>
        protected bool AutoRoundResult { get; private set; }

        /// <summary>
        /// Executes the aggregate query and computes the result
        /// </summary>
        /// <param name="query">The query to execute</param>
        /// <param name="parameters">The parameter values</param>
        /// <returns>The result computed</returns>
        public virtual double Execute
            (
                IQuery query,
                params ParameterValue[] parameters
            )
        {
            var task = ExecuteAsync
            (
                query,
                parameters
            );

            return task.WaitAndUnwrapException();
        }

        /// <summary>
        /// Asynchronously executes the aggregate query and computes the result
        /// </summary>
        /// <param name="query">The query to execute</param>
        /// <param name="parameters">The parameter values</param>
        /// <returns>The result computed</returns>
        public virtual async Task<double> ExecuteAsync
            (
                IQuery query,
                params ParameterValue[] parameters
            )
        {
            Validate.IsNotNull(query);

            var queryTask = query.ExecuteAsync
            (
                parameters
            );

            var results = await queryTask.ConfigureAwait
            (
                false
            );

            return Execute
            (
                results.AllRows
            );
        }

        /// <summary>
        /// Executes the aggregate function and computes the result
        /// </summary>
        /// <param name="rows">The rows to perform the computation on</param>
        /// <returns>The result computed</returns>
        public virtual double Execute
            (
                params QueryRow[] rows
            )
        {
            return Execute
            (
                this.Binding,
                rows
            );
        }

        /// <summary>
        /// Executes the aggregate function and computes the result
        /// </summary>
        /// <param name="binding">The data binding</param>
        /// <param name="rows">The rows to perform the computation on</param>
        /// <returns>The result computed</returns>
        protected virtual double Execute
            (
                DataBinding binding,
                params QueryRow[] rows
            )
        {
            Validate.IsNotNull(binding);
            Validate.IsNotNull(rows);

            if (rows.Any())
            {
                var numbers = new List<double>();

                foreach (var row in rows)
                {
                    var number = binding.Resolve<double>
                    (
                        row
                    );

                    numbers.Add(number);
                }

                var result = Compute(numbers);

                if (this.AutoRoundResult)
                {
                    return Math.Round(result, 2);
                }
                else
                {
                    return result;
                }
            }
            else
            {
                return default(double);
            }
        }
        
        /// <summary>
        /// Computes the function value from the multiple values specified
        /// </summary>
        /// <param name="numbers">The numbers to use in the calculation</param>
        /// <returns>The computed value</returns>
        protected abstract double Compute
        (
            List<double> numbers
        );
    }
}
