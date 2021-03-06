﻿namespace Reportr.Registration.Globalization
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
        /// Finds a registered language in the repository
        /// </summary>
        /// <param name="iso">The language ISO</param>
        /// <returns>The registered language, if found; otherwise null</returns>
        RegisteredLanguage FindLanguage
        (
            string iso
        );

        /// <summary>
        /// Determines if a language has been registered
        /// </summary>
        /// <param name="iso">The language ISO</param>
        /// <returns>True, if a match was found; otherwise false</returns>
        bool HasBeenRegistered
        (
            string iso
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
