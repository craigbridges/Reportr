namespace Reportr.Registration.Culture
{
    using System;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Represents a registered phrase translation
    /// </summary>
    public class RegisteredPhraseTranslation
    {
        /// <summary>
        /// Constructs the phrase translation with its default configuration
        /// </summary>
        protected RegisteredPhraseTranslation() { }

        /// <summary>
        /// Constructs the language phrase translation with details
        /// </summary>
        /// <param name="phrase">The phrase</param>
        /// <param name="languageId">The language ID</param>
        /// <param name="translatedText">The translated phrase text</param>
        internal RegisteredPhraseTranslation
            (
                RegisteredPhrase phrase,
                Guid languageId,
                string translatedText
            )
        {
            Validate.IsNotNull(phrase);

            this.TranslationId = Guid.NewGuid();
            this.Phrase = phrase;
            this.LanguageId = languageId;
            this.TranslatedText = translatedText;
        }

        /// <summary>
        /// Gets the associated phrase
        /// </summary>
        public RegisteredPhrase Phrase { get; protected set; }

        /// <summary>
        /// Gets the unique ID of the translation
        /// </summary>
        [Key]
        public Guid TranslationId { get; protected set; }

        /// <summary>
        /// Gets the ID of the associated registered language
        /// </summary>
        public Guid LanguageId { get; protected set; }

        /// <summary>
        /// Gets the translated phrase text
        /// </summary>
        public string TranslatedText { get; protected set; }
    }
}
