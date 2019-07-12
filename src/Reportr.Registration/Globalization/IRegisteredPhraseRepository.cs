namespace Reportr.Registration.Globalization
{
    using System;
    using System.Collections.Generic;
    
    /// <summary>
    /// Defines a contract for a repository that manages registered phrases
    /// </summary>
    public interface IRegisteredPhraseRepository
    {
        /// <summary>
        /// Adds a phrase to the repository
        /// </summary>
        /// <param name="phrase">The phrase to add</param>
        void AddPhrase
        (
            RegisteredPhrase phrase
        );

        /// <summary>
        /// Gets a phrase from the repository
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
        /// Gets all phrases in the repository
        /// </summary>
        /// <returns>A collection of phrases</returns>
        IEnumerable<RegisteredPhrase> GetAllPhrases();

        /// <summary>
        /// Updates a phrase in the repository
        /// </summary>
        /// <param name="phrase">The phrase to update</param>
        void UpdatePhrase
        (
            RegisteredPhrase phrase
        );

        /// <summary>
        /// Removes a phrase from the repository
        /// </summary>
        /// <param name="id">The phrase ID</param>
        void RemovePhrase
        (
            Guid id
        );
    }
}
