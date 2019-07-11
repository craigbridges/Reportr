namespace Reportr.Registration.Culture
{
    using Reportr.Culture;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Represents an implementation of the factory for registered languages
    /// </summary>
    public class RegisteredPhraseTranslatorFactory : IPhraseTranslatorFactory
    {
        private readonly IRegisteredLanguageRepository _languageRepository;
        private readonly IRegisteredPhraseRepository _phraseRepository;

        /// <summary>
        /// Constructs the factory with required dependencies
        /// </summary>
        /// <param name="languageRepository">The language repository</param>
        /// <param name="phraseRepository">The phrase repository</param>
        public RegisteredPhraseTranslatorFactory
            (
                IRegisteredLanguageRepository languageRepository,
                IRegisteredPhraseRepository phraseRepository
            )
        {
            Validate.IsNotNull(languageRepository);
            Validate.IsNotNull(phraseRepository);

            _languageRepository = languageRepository;
            _phraseRepository = phraseRepository;
        }

        /// <summary>
        /// Gets the cached dictionary that has already been generated
        /// </summary>
        internal static PhraseTranslationDictionary CachedDictionary
        {
            get;
            private set;
        }

        /// <summary>
        /// Clears the factory cache
        /// </summary>
        internal static void ClearCache()
        {
            RegisteredPhraseTranslatorFactory.CachedDictionary = null;
        }

        /// <summary>
        /// Gets a phrase translation dictionary instance
        /// </summary>
        /// <returns>The phrase translation dictionary</returns>
        public PhraseTranslationDictionary GetDictionary()
        {
            if (RegisteredPhraseTranslatorFactory.CachedDictionary != null)
            {
                return RegisteredPhraseTranslatorFactory.CachedDictionary;
            }
            else
            {
                var translator = new PhraseTranslationDictionary();
                var languages = _languageRepository.GetAllLanguages().ToList();
                var phrases = _phraseRepository.GetAllPhrases().ToList();

                foreach (var phrase in phrases)
                {
                    var translations = new Dictionary<Language, string>();

                    foreach (var translation in phrase.Translations.ToList())
                    {
                        var language = ToLanguage
                        (
                            translation.LanguageId
                        );

                        translations.Add
                        (
                            language,
                            translation.TranslatedText
                        );
                    }

                    translator.Add
                    (
                        phrase.PhraseText,
                        translations
                    );
                }

                Language ToLanguage(Guid languageId)
                {
                    var registeredLanguage = languages.FirstOrDefault
                    (
                        l => l.Id == languageId
                    );

                    if (registeredLanguage == null)
                    {
                        throw new KeyNotFoundException
                        (
                            "The registered language was not found."
                        );
                    }

                    return registeredLanguage.ToLanguage();
                }

                RegisteredPhraseTranslatorFactory.CachedDictionary = translator;

                return translator;
            }
        }
    }
}
