namespace Reportr.Data.Querying.Functions
{
    using Nito.AsyncEx.Synchronous;
    using Reportr.Filtering;
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
        public AggregateFunctionBase
            (
                DataBinding binding
            )
        {
            Validate.IsNotNull(binding);
            
            this.Binding = binding;
        }

        /// <summary>
        /// Gets the query field binding for the operation
        /// </summary>
        public DataBinding Binding { get; private set; }

        /// <summary>
        /// Executes the aggregate query and computes the result
        /// </summary>
        /// <param name="query">The query to execute</param>
        /// <param name="parameters">The parameter values</param>
        /// <returns>The result computed</returns>
        public double Execute
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
        public async Task<double> ExecuteAsync
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
        public double Execute
            (
                params QueryRow[] rows
            )
        {
            Validate.IsNotNull(rows);
            
            if (rows.Any())
            {
                var numbers = new List<double>();

                foreach (var row in rows)
                {
                    var number = ResolveRowValue
                    (
                        row
                    );

                    numbers.Add(number);
                }

                return Compute(numbers);
            }
            else
            {
                return default(double);
            }
        }

        /// <summary>
        /// Resolves the double value of a single query row
        /// </summary>
        /// <param name="row">The query row</param>
        /// <returns>The double value resolved</returns>
        protected virtual double ResolveRowValue
            (
                QueryRow row
            )
        {
            Validate.IsNotNull(row);

            return this.Binding.Resolve<double>
            (
                row
            );
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
