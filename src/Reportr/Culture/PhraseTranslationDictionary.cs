namespace Reportr.Culture
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Represents a dictionary of phrase translations into other languages
    /// </summary>
    public class PhraseTranslationDictionary : Dictionary<string, Dictionary<Language, string>>
    {
        /// <summary>
        /// Sets a translation for a phrase and language
        /// </summary>
        /// <param name="originalPhrase">The original phrase</param>
        /// <param name="translatedLanguage">The translated language</param>
        /// <param name="translatedPhrase">The translated phrase</param>
        public void SetTranslation
            (
                string originalPhrase,
                Language translatedLanguage,
                string translatedPhrase
            )
        {
            var matchFound = this.Any
            (
                item => item.Key.Equals
                (
                    originalPhrase,
                    StringComparison.OrdinalIgnoreCase
                )
            );

            if (matchFound)
            {
                this[originalPhrase][translatedLanguage] = translatedPhrase;
            }
            else
            {
                Add
                (
                    originalPhrase,
                    new Dictionary<Language, string>()
                    {
                        { translatedLanguage, translatedPhrase }
                    }
                );
            }
        }

        /// <summary>
        /// Translates a phrase into the language specified
        /// </summary>
        /// <param name="phrase">The phrase to translated</param>
        /// <param name="language">The language to translate it into</param>
        /// <returns>The translated phrase</returns>
        /// <remarks>
        /// If no matching phrase or language can be found, 
        /// the same phrase is returned instead.
        /// </remarks>
        public string Translate
            (
                string phrase,
                Language language
            )
        {
            var phraseFound = this.Any
            (
                pair => pair.Key.Equals
                (
                    phrase,
                    StringComparison.OrdinalIgnoreCase
                )
            );

            if (phraseFound)
            {
                var languageFound = this[phrase].ContainsKey
                (
                    language
                );

                if (languageFound)
                {
                    return this[phrase][language];
                }
                else
                {
                    return phrase;
                }
            }
            else
            {
                return phrase;
            }
        }
    }
}
