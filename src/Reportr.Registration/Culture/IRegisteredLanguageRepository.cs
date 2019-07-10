namespace Reportr.Registration.Culture
{
    using System;
    using System.Collections.Generic;
    
    /// <summary>
    /// Defines a contract for a repository that manages registered languages
    /// </summary>
    public interface IRegisteredLanguageRepository
    {
        /// <summary>
        /// Adds a registered language to the repository
        /// </summary>
        /// <param name="language">The language to add</param>
        void AddLanguage
        (
            RegisteredLanguage language
        );

        /// <summary>
        /// Gets a registered language from the repository
        /// </summary>
        /// <param name="id">The language ID</param>
        /// <returns>The matching registered language</returns>
        RegisteredLanguage GetLanguage
        (
            Guid id
        );

        /// <summary>
        /// Gets the default registered language from the repository
        /// </summary>
        /// <returns>The registered language</returns>
        RegisteredLanguage GetDefaultLanguage();

        /// <summary>
        /// Gets all registered languages in the repository
        /// </summary>
        /// <returns>A collection of registered languages</returns>
        IEnumerable<RegisteredLanguage> GetAllLanguages();

        /// <summary>
        /// Updates a registered language in the repository
        /// </summary>
        /// <param name="language">The language to update</param>
        void UpdateLanguage
        (
            RegisteredLanguage language
        );

        /// <summary>
        /// Removes a registered language from the repository
        /// </summary>
        /// <param name="id">The language ID</param>
        void RemoveLanguage
        (
            Guid id
        );
    }
}
