namespace Reportr.Registration.Culture
{
    using System;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Represents a single language phrase translation
    /// </summary>
    public class LanguagePhraseTranslation
    {
        /// <summary>
        /// Constructs the phrase translation with its default configuration
        /// </summary>
        protected LanguagePhraseTranslation() { }

        /// <summary>
        /// Constructs the language phrase translation with details
        /// </summary>
        /// <param name="language">The registered language</param>
        /// <param name="originalText">The original phrase text</param>
        /// <param name="translatedText">The translated phrase text</param>
        internal LanguagePhraseTranslation
            (
                RegisteredLanguage language,
                string originalText,
                string translatedText
            )
        {
            Validate.IsNotNull(language);

            this.Language = language;
            this.PhraseId = Guid.NewGuid();

            SetPhraseText(originalText, translatedText);
        }

        /// <summary>
        /// Gets the associated registered language
        /// </summary>
        public virtual RegisteredLanguage Language { get; protected set; }

        /// <summary>
        /// Gets the ID of the associated registered language
        /// </summary>
        public Guid CategoryId { get; protected set; }

        /// <summary>
        /// Gets the ID of the phrase
        /// </summary>
        [Key]
        public Guid PhraseId { get; protected set; }

        /// <summary>
        /// Gets the original phrase text
        /// </summary>
        public string OriginalText { get; protected set; }

        /// <summary>
        /// Gets the translated phrase text
        /// </summary>
        public string TranslatedText { get; protected set; }

        /// <summary>
        /// Sets the phrase text
        /// </summary>
        /// <param name="originalText">The original phrase text</param>
        /// <param name="translatedText">The translated phrase text</param>
        public void SetPhraseText
            (
                string originalText,
                string translatedText
            )
        {
            this.OriginalText = originalText;
            this.TranslatedText = translatedText;
        }
    }
}
