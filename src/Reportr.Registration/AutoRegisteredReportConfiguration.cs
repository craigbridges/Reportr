namespace Reportr.Registration
{
    using System;

    /// <summary>
    /// Represents configuration details for an auto registered report
    /// </summary>
    public class AutoRegisteredReportConfiguration : RegisteredReportConfiguration
    {
        /// <summary>
        /// Gets or sets the report builder type
        /// </summary>
        public Type BuilderType { get; set; }
    }
}
