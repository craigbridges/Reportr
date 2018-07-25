namespace Reportr.Data.Querying.Functions
{
    using Newtonsoft.Json;
    using Nito.AsyncEx.Synchronous;
    using Reportr.Filtering;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    /// <summary>
    /// Represents a base class for all query aggregate functions
    /// </summary>
    public abstract class QueryAggregateFunctionBase : IQueryAggregateFunction
    {
        /// <summary>
        /// Constructs the function with a query and binding
        /// </summary>
        /// <param name="query">The query</param>
        /// <param name="binding">The data binding</param>
        public QueryAggregateFunctionBase
            (
                IQuery query,
                DataBinding binding
            )
        {
            Validate.IsNotNull(query);
            Validate.IsNotNull(binding);

            this.Query = query;
            this.Binding = binding;
        }

        /// <summary>
        /// Gets the query to execute
        /// </summary>
        [JsonIgnore]
        public IQuery Query { get; private set; }

        /// <summary>
        /// Gets the query field binding for the operation
        /// </summary>
        public DataBinding Binding { get; private set; }

        /// <summary>
        /// Executes the aggregate query and computes the result
        /// </summary>
        /// <param name="parameters">The parameter values</param>
        /// <returns>The result computed</returns>
        public double Execute
            (
                params ParameterValue[] parameters
            )
        {
            var task = ExecuteAsync(parameters);

            return task.WaitAndUnwrapException();
        }

        /// <summary>
        /// Asynchronously executes the aggregate query and computes the result
        /// </summary>
        /// <param name="parameters">The parameter values</param>
        /// <returns>The result computed</returns>
        public async Task<double> ExecuteAsync
            (
                params ParameterValue[] parameters
            )
        {
            var queryTask = this.Query.ExecuteAsync
            (
                parameters
            );

            var results = await queryTask.ConfigureAwait
            (
                false
            );

            var numbers = new List<double>();

            foreach (var row in results.AllRows)
            {
                var number = ResolveRowValue
                (
                    row
                );

                numbers.Add(number);
            }

            return Compute(numbers);
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
