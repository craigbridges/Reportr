namespace Reportr.Culture
{
    /// <summary>
    /// Defines a contract for a factory that resolves phrase translation dictionaries
    /// </summary>
    public interface IPhraseTranslatorFactory
    {
        /// <summary>
        /// Gets a phrase translation dictionary instance
        /// </summary>
        /// <returns>The phrase translation dictionary</returns>
        PhraseTranslationDictionary GetDictionary();
    }
}
