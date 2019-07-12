namespace Reportr.Globalization
{
    /// <summary>
    /// Defines a contract for a type that can be localized
    /// </summary>
    public interface ILocalizable
    {
        /// <summary>
        /// Translates the object to the language specified
        /// </summary>
        /// <param name="translator">The translation dictionary</param>
        /// <param name="language">The language to translate into</param>
        void Translate
        (
            PhraseTranslationDictionary translator,
            Language language
        );
    }
}
