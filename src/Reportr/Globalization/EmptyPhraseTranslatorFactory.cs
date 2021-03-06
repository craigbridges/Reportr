﻿namespace Reportr.Globalization
{
    /// <summary>
    /// Represents an implementation of the factory that generates an empty dictionary
    /// </summary>
    public class EmptyPhraseTranslatorFactory : IPhraseTranslatorFactory
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
