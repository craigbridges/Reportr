namespace Reportr.Filtering
{
    /// <summary>
    /// Represents a single report filter parameter value
    /// </summary>
    public sealed class ReportFilterParameterValue : ParameterValue
    {
        /// <summary>
        /// Constructs the parameter value with the details
        /// </summary>
        /// <param name="definition">The parameter definition</param>
        /// <param name="value">The value</param>
        public ReportFilterParameterValue
            (
                ReportParameterDefinition definition,
                object value
            )

            : base(definition.Parameter, value)
        {
            this.Definition = definition;
        }

        /// <summary>
        /// Gets the report parameter definition
        /// </summary>
        public ReportParameterDefinition Definition { get; private set; }

        /// <summary>
        /// Generates a new parameter value which is a clone of the current
        /// </summary>
        /// <returns>The parameter value that was generated</returns>
        public ReportFilterParameterValue Clone()
        {
            return new ReportFilterParameterValue
            (
                this.Definition,
                this.Value
            );
        }
    }
}
