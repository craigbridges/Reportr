namespace Reportr.Registration.Entity.Repositories
{
    using Reportr.Registration.Globalization;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    
    /// <summary>
    /// Represents an Entity Framework registered phrase repository
    /// </summary>
    public sealed class EfRegisteredPhraseRepository : IRegisteredPhraseRepository
    {
        private readonly ReportrDbContext _context;

        /// <summary>
        /// Constructs the repository with a database context
        /// </summary>
        /// <param name="context">The database context</param>
        public EfRegisteredPhraseRepository
            (
                ReportrDbContext context
            )
        {
            Validate.IsNotNull(context);

            _context = context;
        }

        /// <summary>
        /// Adds a single registered phrase to the repository
        /// </summary>
        /// <param name="phrase">The registered phrase</param>
        public void AddPhrase
            (
                RegisteredPhrase phrase
            )
        {
            Validate.IsNotNull(phrase);

            _context.Set<RegisteredPhrase>().Add
            (
                phrase
            );
        }

        /// <summary>
        /// Gets a registered phrase from the repository
        /// </summary>
        /// <param name="id">The phrase ID</param>
        /// <returns>The matching registered phrase</returns>
        public RegisteredPhrase GetPhrase
            (
                Guid id
            )
        {
            var set = _context.Set<RegisteredPhrase>();
            var phrase = set.FirstOrDefault(l => l.Id == id);

            if (phrase == null)
            {
                throw new KeyNotFoundException
                (
                    "The registered phrase was not found."
                );
            }

            return phrase;
        }

        /// <summary>
        /// Finds a registered phrase in the repository
        /// </summary>
        /// <param name="text">The phrase text</param>
        /// <returns>The registered phrase, if found; otherwise null</returns>
        public RegisteredPhrase FindPhrase
            (
                string text
            )
        {
            var set = _context.Set<RegisteredPhrase>();

            return set.FirstOrDefault
            (
                l => l.PhraseText.Equals
                (
                    text,
                    StringComparison.OrdinalIgnoreCase
                )
            );
        }

        /// <summary>
        /// Determines if a phrase has been registered
        /// </summary>
        /// <param name="phraseText">The phrase text</param>
        /// <returns>True, if a match was found; otherwise false</returns>
        public bool HasBeenRegistered
            (
                string phraseText
            )
        {
            var set = _context.Set<RegisteredPhrase>();

            return set.Any
            (
                l => l.PhraseText.Equals
                (
                    phraseText,
                    StringComparison.OrdinalIgnoreCase
                )
            );
        }

        /// <summary>
        /// Gets all registered phrases in the repository
        /// </summary>
        /// <returns>A collection of registered phrases</returns>
        public IEnumerable<RegisteredPhrase> GetAllPhrases()
        {
            var phrases = _context.Set<RegisteredPhrase>();

            return phrases.OrderBy
            (
                a => a.PhraseText
            );
        }

        /// <summary>
        /// Updates a single registered phrase in the repository
        /// </summary>
        /// <param name="phrase">The registered phrase to update</param>
        public void UpdatePhrase
            (
                RegisteredPhrase phrase
            )
        {
            Validate.IsNotNull(phrase);

            var entry = _context.Entry<RegisteredPhrase>
            (
                phrase
            );

            entry.State = EntityState.Modified;
        }

        /// <summary>
        /// Removes a single registered phrase from the repository
        /// </summary>
        /// <param name="name">The name of the phrase</param>
        public void RemovePhrase
            (
                Guid id
            )
        {
            var phrase = GetPhrase(id);

            var entry = _context.Entry<RegisteredPhrase>
            (
                phrase
            );

            // Ensure the entity has been attached to the object state manager
            if (entry.State == EntityState.Detached)
            {
                _context.Set<RegisteredPhrase>().Attach
                (
                    phrase
                );
            }

            _context.Set<RegisteredPhrase>().Remove
            (
                phrase
            );
        }
    }
}
