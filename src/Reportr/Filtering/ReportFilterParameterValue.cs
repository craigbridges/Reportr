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
        /// <param name="sectionType">The section type</param>
        /// <param name="parameterInfo">The parameter info</param>
        /// <param name="value">The value</param>
        public ReportFilterParameterValue
            (
                ReportSectionType sectionType,
                ParameterInfo parameterInfo,
                object value
            )

            : base(parameterInfo, value)
        {
            this.SectionType = sectionType;
        }

        /// <summary>
        /// Gets the report section type
        /// </summary>
        public ReportSectionType SectionType { get; private set; }
    }
}
