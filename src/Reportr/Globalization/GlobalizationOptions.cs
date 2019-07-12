namespace Reportr.Globalization
{
    using System.Globalization;

    /// <summary>
    /// Represents options for report globalization
    /// </summary>
    public class GlobalizationOptions
    {
        /// <summary>
        /// Gets or sets the culture to use
        /// </summary>
        public CultureInfo Culture { get; set; }

        /// <summary>
        /// Gets or sets the preferred language
        /// </summary>
        public Language PreferredLanguage { get; set; }
    }
}
