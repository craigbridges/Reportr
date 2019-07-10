namespace Reportr
{
    using Reportr.Culture;
    using System.Globalization;
    
    /// <summary>
    /// Represents options for generating a report
    /// </summary>
    public class ReportGenerationOptions
    {
        /// <summary>
        /// Gets or sets the culture to use as default
        /// </summary>
        public CultureInfo DefaultCulture { get; set; }

        /// <summary>
        /// Gets or sets the preferred language
        /// </summary>
        public Language PreferredLanguage { get; set; }
    }
}
