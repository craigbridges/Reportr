namespace Reportr.Registration.Culture
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Represents configuration details for a registered language
    /// </summary>
    public class PhraseConfiguration
    {
        /// <summary>
        /// Gets or sets the phrase text in the default language
        /// </summary>
        public string PhraseText { get; set; }

        /// <summary>
        /// Gets or sets a dictionary of language translations for the phrase
        /// </summary>
        /// <remarks>
        /// The item key represents the language ID and the value 
        /// represents the translated phrase text.
        /// </remarks>
        public Dictionary<Guid, string> Translations { get; set; }
    }
}
