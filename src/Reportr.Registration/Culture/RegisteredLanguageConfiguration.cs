namespace Reportr.Registration.Culture
{
    using System.Collections.Generic;

    /// <summary>
    /// Represents configuration details for a registered language
    /// </summary>
    public class RegisteredLanguageConfiguration
    {
        /// <summary>
        /// Gets or sets the report name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the language ISO
        /// </summary>
        public string Iso { get; set; }

        /// <summary>
        /// Gets or sets a flag indicating if the language is the default
        /// </summary>
        public bool Default { get; set; }
    }
}
