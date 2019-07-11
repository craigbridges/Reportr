namespace Reportr.Registration.Culture
{
    using System;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Represents a single language phrase translation
    /// </summary>
    public class PhraseTranslation
    {
        /// <summary>
        /// Constructs the phrase translation with its default configuration
        /// </summary>
        protected PhraseTranslation() { }

        /// <summary>
        /// Constructs the language phrase translation with details
        /// </summary>
        /// <param name="phrase">The phrase</param>
        /// <param name="languageId">The language ID</param>
        /// <param name="translatedText">The translated phrase text</param>
        internal PhraseTranslation
            (
                Phrase phrase,
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
        public Phrase Phrase { get; protected set; }

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
