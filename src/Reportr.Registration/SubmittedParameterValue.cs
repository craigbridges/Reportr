namespace Reportr.Registration
{
    /// <summary>
    /// Represents a single submitted parameter value
    /// </summary>
    public class SubmittedParameterValue
    {
        /// <summary>
        /// Gets or sets the parameter name
        /// </summary>
        public string ParameterName { get; set; }

        /// <summary>
        /// Gets or sets the parameter values
        /// </summary>
        public string[] Values { get; set; }
    }
}
