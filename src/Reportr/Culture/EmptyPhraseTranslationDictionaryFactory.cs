namespace Reportr.Culture
{
    /// <summary>
    /// Represents an implementation of the factory that generates an empty dictionary
    /// </summary>
    public class EmptyPhraseTranslationDictionaryFactory : IPhraseTranslationDictionaryFactory
    {
        /// <summary>
        /// Gets a phrase translation dictionary instance
        /// </summary>
        /// <returns>The phrase translation dictionary</returns>
        public PhraseTranslationDictionary GetDictionary()
        {
            return new PhraseTranslationDictionary();
        }
    }
}
