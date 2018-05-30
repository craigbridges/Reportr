namespace Reportr.Filtering
{
    /// <summary>
    /// Represents the definition of a single report parameter
    /// </summary>
    public class ReportParameterDefinition
    {
        /// <summary>
        /// Constructs the report parameter definition with the details
        /// </summary>
        /// <param name="parameter">The parameter information</param>
        /// <param name="targetType">The target type</param>
        /// <param name="targetName">The target name</param>
        /// <param name="targetValue">The target value (optional)</param>
        internal ReportParameterDefinition
            (
                ParameterInfo parameter,
                ReportParameterTargetType targetType,
                string targetName,
                object targetValue = null
            )
        {
            Validate.IsNotNull(parameter);
            Validate.IsNotEmpty(targetName);

            this.Parameter = parameter;
            this.TargetType = targetType;
            this.TargetName = targetName;
            this.TargetValue = targetValue;
        }

        /// <summary>
        /// Gets the parameter information
        /// </summary>
        public ParameterInfo Parameter { get; protected set; }

        /// <summary>
        /// Gets the report parameter target type
        /// </summary>
        public ReportParameterTargetType TargetType { get; protected set; }

        /// <summary>
        /// Gets the name of the target
        /// </summary>
        public string TargetName { get; protected set; }

        /// <summary>
        /// Gets the target value
        /// </summary>
        public object TargetValue { get; protected set; }
    }
}
