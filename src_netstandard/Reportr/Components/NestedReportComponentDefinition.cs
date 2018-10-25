namespace Reportr.Components
{
    using Reportr.Data.Querying;
    using Reportr.Filtering;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;

    /// <summary>
    /// Represents a nested report component definition
    /// </summary>
    public class NestedReportComponentDefinition
    {
        /// <summary>
        /// Constructs the nested component definition with the details
        /// </summary>
        /// <param name="definition">The component definition</param>
        /// <param name="parameterBindings">The parameter bindings</param>
        public NestedReportComponentDefinition
            (
                IReportComponentDefinition definition,
                params ReportParameterBinding[] parameterBindings
            )
        {
            Validate.IsNotNull(definition);

            this.Definition = definition;
            this.ParameterBindings = new Collection<ReportParameterBinding>();

            if (parameterBindings != null)
            {
                foreach (var binding in parameterBindings)
                {
                    this.ParameterBindings.Add(binding);
                }
            }
        }

        /// <summary>
        /// Gets the component definition
        /// </summary>
        public IReportComponentDefinition Definition
        {
            get;
            protected set;
        }

        /// <summary>
        /// Gets a collection of parameter value bindings
        /// </summary>
        public ICollection<ReportParameterBinding> ParameterBindings
        {
            get;
            protected set;
        }

        /// <summary>
        /// Compiles the parameter values for the parameter bindings
        /// </summary>
        /// <param name="filter">The report filter</param>
        /// <param name="row">The query row</param>
        /// <returns>A dictionary of parameter values</returns>
        protected Dictionary<string , object> CompileParameterValues
            (
                ReportFilter filter,
                QueryRow row
            )
        {
            Validate.IsNotNull(filter);
            Validate.IsNotNull(row);

            var parameterOverrides = new Dictionary<string, object>();

            foreach (var parameterBinding in this.ParameterBindings)
            {
                var value = parameterBinding.Binding.Resolve
                (
                    row
                );
                
                parameterOverrides.Add
                (
                    parameterBinding.ParameterName,
                    value
                );
            }

            return parameterOverrides;
        }

        /// <summary>
        /// Generates a nested report filter for a query row
        /// </summary>
        /// <param name="parentFilter">The parent report filter</param>
        /// <param name="row">The query row</param>
        /// <returns>The report filter generated</returns>
        public ReportFilter GenerateNestedFilter
            (
                ReportFilter parentFilter,
                QueryRow row
            )
        {
            Validate.IsNotNull(parentFilter);
            Validate.IsNotNull(row);

            var parameterOverrides = CompileParameterValues
            (
                parentFilter,
                row
            );

            var nestedFilter = parentFilter.Clone();

            nestedFilter.SetParameterValues
            (
                parameterOverrides
            );

            return nestedFilter;
        }
    }
}
