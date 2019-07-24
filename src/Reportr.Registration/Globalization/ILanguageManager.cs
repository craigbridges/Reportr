namespace Reportr.Registration.Globalization
{
    using System;
    using System.Collections.Generic;
    
    /// <summary>
    /// Defines a contract for a service that manages languages
    /// </summary>
    public interface ILanguageManager
    {
        /// <summary>
        /// Creates a registered language
        /// </summary>
        /// <param name="configuration">The language configuration</param>
        /// <returns>The language created</returns>
        RegisteredLanguage CreateLanguage
        (
            RegisteredLanguageConfiguration configuration
        );

        /// <summary>
        /// Auto registers the languages specified
        /// </summary>
        /// <param name="configurations">The language configurations</param>
        void AutoRegisterLanguages
        (
            params RegisteredLanguageConfiguration[] configurations
        );

        /// <summary>
        /// Gets a registered language
        /// </summary>
        /// <param name="id">The language ID</param>
        /// <returns>The matching registered language</returns>
        RegisteredLanguage GetLanguage
        (
            Guid id
        );

        /// <summary>
        /// Finds a registered language by ISO
        /// </summary>
        /// <param name="iso">The language ISO</param>
        /// <returns>The registered language, if found; otherwise null</returns>
        RegisteredLanguage FindLanguage
        (
            string iso
        );

        /// <summary>
        /// Gets the default registered language
        /// </summary>
        /// <returns>The registered language</returns>
        RegisteredLanguage GetDefaultLanguage();

        /// <summary>
        /// Gets all registered languages
        /// </summary>
        /// <returns>A collection of registered languages</returns>
        IEnumerable<RegisteredLanguage> GetAllLanguages();

        /// <summary>
        /// Configures a registered language
        /// </summary>
        /// <param name="id">The language ID</param>
        /// <param name="configuration">The language configuration</param>
        void ConfigureLanguage
        (
            Guid id,
            RegisteredLanguageConfiguration configuration
        );

        /// <summary>
        /// Sets the default language
        /// </summary>
        /// <param name="id">The language ID</param>
        void SetDefaultLanguage
        (
            Guid id
        );

        /// <summary>
        /// Deletes a registered language
        /// </summary>
        /// <param name="id">The language ID</param>
        void DeleteLanguage
        (
            Guid id
        );

        /// <summary>
        /// Creates a phrase
        /// </summary>
        /// <param name="configuration">The phrase configuration</param>
        /// <returns>The phrase created</returns>
        RegisteredPhrase CreatePhrase
        (
            RegisteredPhraseConfiguration configuration
        );

        /// <summary>
        /// Auto registers the phrases specified
        /// </summary>
        /// <param name="configurations">The phrase configurations</param>
        void AutoRegisterPhrases
        (
            params RegisteredPhraseConfiguration[] configurations
        );

        /// <summary>
        /// Gets a phrase
        /// </summary>
        /// <param name="id">The phrase ID</param>
        /// <returns>The phrase</returns>
        RegisteredPhrase GetPhrase
        (
            Guid id
        );

        /// <summary>
        /// Finds a registered phrase in the repository
        /// </summary>
        /// <param name="text">The phrase text</param>
        /// <returns>The registered phrase, if found; otherwise null</returns>
        RegisteredPhrase FindPhrase
        (
            string text
        );

        /// <summary>
        /// Gets all phrases
        /// </summary>
        /// <returns>A collection of phrases</returns>
        IEnumerable<RegisteredPhrase> GetAllPhrases();

        /// <summary>
        /// Configures a phrase
        /// </summary>
        /// <param name="id">The phrase ID</param>
        /// <param name="configuration">The phrase configuration</param>
        void ConfigurePhrase
        (
            Guid id,
            RegisteredPhraseConfiguration configuration
        );

        /// <summary>
        /// Deletes a phrase
        /// </summary>
        /// <param name="id">The phrase ID</param>
        void DeletePhrase
        (
            Guid id
        );
    }
}
