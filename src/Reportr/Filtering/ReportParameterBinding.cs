namespace Reportr.Filtering
{
    using System;
    using Reportr.Data;
    
    /// <summary>
    /// Represents a data binding for a single report parameter value
    /// </summary>
    public class ReportParameterBinding
    {
        /// <summary>
        /// Constructs the parameter binding with the details
        /// </summary>
        /// <param name="parameterName">The parameter name</param>
        /// <param name="binding">The data binding</param>
        public ReportParameterBinding
            (
                string parameterName,
                DataBinding binding
            )
        {
            Validate.IsNotEmpty(parameterName);
            Validate.IsNotNull(binding);

            this.ParameterName = parameterName;
            this.Binding = binding;
        }

        /// <summary>
        /// Gets the parameter name
        /// </summary>
        public string ParameterName { get; protected set; }

        /// <summary>
        /// Gets the data binding
        /// </summary>
        public DataBinding Binding { get; protected set; }
    }
}
