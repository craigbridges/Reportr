namespace Reportr.Registration.Globalization
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Represents the default language manager implementation
    /// </summary>
    public sealed class LanguageManager
    {
        private readonly IRegisteredLanguageRepository _languageRepository;
        private readonly IRegisteredPhraseRepository _phraseRepository;
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// Constructs the language manager with required dependencies
        /// </summary>
        /// <param name="languageRepository">The language repository</param>
        /// <param name="phraseRepository">The phrase repository</param>
        /// <param name="unitOfWork">The unit of work</param>
        public LanguageManager
            (
                IRegisteredLanguageRepository languageRepository,
                IRegisteredPhraseRepository phraseRepository,
                IUnitOfWork unitOfWork
            )
        {
            Validate.IsNotNull(languageRepository);
            Validate.IsNotNull(phraseRepository);
            Validate.IsNotNull(unitOfWork);

            _languageRepository = languageRepository;
            _phraseRepository = phraseRepository;
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Creates a registered language
        /// </summary>
        /// <param name="configuration">The language configuration</param>
        /// <returns>The language created</returns>
        public RegisteredLanguage CreateLanguage
            (
                RegisteredLanguageConfiguration configuration
            )
        {
            EnsureLanguageAvailable(configuration);

            var language = new RegisteredLanguage
            (
                configuration
            );

            _languageRepository.AddLanguage(language);
            _unitOfWork.SaveChanges();

            RegisteredPhraseTranslatorFactory.ClearCache();

            return language;
        }

        /// <summary>
        /// Auto registers the languages specified
        /// </summary>
        /// <param name="configurations">The language configurations</param>
        public void AutoRegisterLanguages
            (
                params RegisteredLanguageConfiguration[] configurations
            )
        {
            Validate.IsNotNull(configurations);

            var changesMade = false;

            foreach (var configuration in configurations)
            {
                var registered = _languageRepository.HasBeenRegistered
                (
                    configuration.Iso
                );

                if (false == registered)
                {
                    var language = new RegisteredLanguage
                    (
                        configuration
                    );

                    _languageRepository.AddLanguage
                    (
                        language
                    );

                    changesMade = true;
                }
            }

            if (changesMade)
            {
                _unitOfWork.SaveChanges();
            }
        }

        /// <summary>
        /// Gets a registered language
        /// </summary>
        /// <param name="id">The language ID</param>
        /// <returns>The matching registered language</returns>
        public RegisteredLanguage GetLanguage
            (
                Guid id
            )
        {
            return _languageRepository.GetLanguage(id);
        }

        /// <summary>
        /// Gets the default registered language
        /// </summary>
        /// <returns>The registered language</returns>
        public RegisteredLanguage GetDefaultLanguage()
        {
            return _languageRepository.GetDefaultLanguage();
        }

        /// <summary>
        /// Gets all registered languages
        /// </summary>
        /// <returns>A collection of registered languages</returns>
        public IEnumerable<RegisteredLanguage> GetAllLanguages()
        {
            return _languageRepository.GetAllLanguages();
        }

        /// <summary>
        /// Configures a registered language
        /// </summary>
        /// <param name="id">The language ID</param>
        /// <param name="configuration">The language configuration</param>
        public void ConfigureLanguage
            (
                Guid id,
                RegisteredLanguageConfiguration configuration
            )
        {
            var language = _languageRepository.GetLanguage(id);

            language.Configure(configuration);

            _languageRepository.UpdateLanguage(language);
            _unitOfWork.SaveChanges();

            RegisteredPhraseTranslatorFactory.ClearCache();
        }

        /// <summary>
        /// Deletes a registered language
        /// </summary>
        /// <param name="id">The language ID</param>
        public void DeleteLanguage
            (
                Guid id
            )
        {
            _languageRepository.RemoveLanguage(id);
            _unitOfWork.SaveChanges();

            RegisteredPhraseTranslatorFactory.ClearCache();
        }

        /// <summary>
        /// Ensures a language is available to be registered
        /// </summary>
        /// <param name="configuration">The language configuration</param>
        private void EnsureLanguageAvailable
            (
                RegisteredLanguageConfiguration configuration
            )
        {
            Validate.IsNotNull(configuration);

            var iso = configuration.Iso;

            var language = _languageRepository.FindLanguage
            (
                iso
            );

            if (language != null)
            {
                throw new InvalidOperationException
                (
                    $"The language ISO '{iso}' is not available."
                );
            }
        }

        /// <summary>
        /// Creates a phrase
        /// </summary>
        /// <param name="configuration">The phrase configuration</param>
        /// <returns>The phrase created</returns>
        public RegisteredPhrase CreatePhrase
            (
                RegisteredPhraseConfiguration configuration
            )
        {
            EnsurePhraseAvailable(configuration);

            var phrase = new RegisteredPhrase
            (
                configuration
            );

            _phraseRepository.AddPhrase(phrase);
            _unitOfWork.SaveChanges();

            RegisteredPhraseTranslatorFactory.ClearCache();

            return phrase;
        }

        /// <summary>
        /// Gets a phrase
        /// </summary>
        /// <param name="id">The phrase ID</param>
        /// <returns>The phrase</returns>
        public RegisteredPhrase GetPhrase
            (
                Guid id
            )
        {
            return _phraseRepository.GetPhrase(id);
        }

        /// <summary>
        /// Gets all phrases
        /// </summary>
        /// <returns>A collection of phrases</returns>
        public IEnumerable<RegisteredPhrase> GetAllPhrases()
        {
            return _phraseRepository.GetAllPhrases();
        }

        /// <summary>
        /// Configures a phrase
        /// </summary>
        /// <param name="id">The phrase ID</param>
        /// <param name="configuration">The phrase configuration</param>
        public void ConfigurePhrase
            (
                Guid id,
                RegisteredPhraseConfiguration configuration
            )
        {
            var phrase = _phraseRepository.GetPhrase(id);

            phrase.Configure(configuration);

            _phraseRepository.UpdatePhrase(phrase);
            _unitOfWork.SaveChanges();

            RegisteredPhraseTranslatorFactory.ClearCache();
        }

        /// <summary>
        /// Deletes a phrase
        /// </summary>
        /// <param name="id">The phrase ID</param>
        public void DeletePhrase
            (
                Guid id
            )
        {
            _phraseRepository.RemovePhrase(id);
            _unitOfWork.SaveChanges();

            RegisteredPhraseTranslatorFactory.ClearCache();
        }

        /// <summary>
        /// Ensures a phrase is available to be registered
        /// </summary>
        /// <param name="configuration">The phrase configuration</param>
        private void EnsurePhraseAvailable
            (
                RegisteredPhraseConfiguration configuration
            )
        {
            Validate.IsNotNull(configuration);

            var text = configuration.PhraseText;

            var phrase = _phraseRepository.FindPhrase
            (
                text
            );

            if (phrase != null)
            {
                throw new InvalidOperationException
                (
                    $"The '{text}' has already been registered."
                );
            }
        }
    }
}
