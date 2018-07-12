namespace Reportr.Registration.Authorization
{
    /// <summary>
    /// Represents configuration details for a report parameter constraint
    /// </summary>
    public class ReportParameterConstraintConfiguration
    {
        /// <summary>
        /// Gets or sets the name of the report parameter
        /// </summary>
        public string ParameterName { get; set; }

        /// <summary>
        /// Gets or sets the report parameter mapping type
        /// </summary>
        public ReportParameterMappingType MappingType { get; set; }

        /// <summary>
        /// Gets or sets the report parameter mapping value
        /// </summary>
        public object MappingValue { get; set; }
    }
}
