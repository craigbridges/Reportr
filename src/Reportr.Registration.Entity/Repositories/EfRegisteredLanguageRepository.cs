namespace Reportr.Registration.Entity.Repositories
{
    using Microsoft.EntityFrameworkCore;
    using Reportr.Registration.Globalization;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    
    /// <summary>
    /// Represents an Entity Framework registered language repository
    /// </summary>
    public sealed class EfRegisteredLanguageRepository : IRegisteredLanguageRepository
    {
        private readonly ReportrDbContext _context;

        /// <summary>
        /// Constructs the repository with a database context
        /// </summary>
        /// <param name="context">The database context</param>
        public EfRegisteredLanguageRepository
            (
                ReportrDbContext context
            )
        {
            Validate.IsNotNull(context);

            _context = context;
        }

        /// <summary>
        /// Adds a single registered language to the repository
        /// </summary>
        /// <param name="language">The registered language</param>
        public void AddLanguage
            (
                RegisteredLanguage language
            )
        {
            Validate.IsNotNull(language);

            _context.Set<RegisteredLanguage>().Add
            (
                language
            );
        }

        /// <summary>
        /// Gets a registered language from the repository
        /// </summary>
        /// <param name="id">The language ID</param>
        /// <returns>The matching registered language</returns>
        public RegisteredLanguage GetLanguage
            (
                Guid id
            )
        {
            var set = _context.Set<RegisteredLanguage>();
            var language = set.FirstOrDefault(l => l.Id == id);

            if (language == null)
            {
                throw new KeyNotFoundException
                (
                    "The registered language was not found."
                );
            }

            return language;
        }

        /// <summary>
        /// Finds a registered language in the repository
        /// </summary>
        /// <param name="iso">The language ISO</param>
        /// <returns>The registered language, if found; otherwise null</returns>
        public RegisteredLanguage FindLanguage
            (
                string iso
            )
        {
            var set = _context.Set<RegisteredLanguage>();

            return set.FirstOrDefault
            (
                l => l.Iso.Equals(iso, StringComparison.OrdinalIgnoreCase)
            );
        }

        /// <summary>
        /// Determines if a language has been registered
        /// </summary>
        /// <param name="iso">The language ISO</param>
        /// <returns>True, if a match was found; otherwise false</returns>
        public bool HasBeenRegistered
            (
                string iso
            )
        {
            Validate.IsNotEmpty(iso);

            var set = _context.Set<RegisteredLanguage>();

            return set.Any
            (
                l => l.Iso.Equals(iso, StringComparison.OrdinalIgnoreCase)
            );
        }

        /// <summary>
        /// Gets the default registered language from the repository
        /// </summary>
        /// <returns>The registered language</returns>
        public RegisteredLanguage GetDefaultLanguage()
        {
            var set = _context.Set<RegisteredLanguage>();
            var language = set.FirstOrDefault(l => l.Default);

            if (language == null)
            {
                language = set.FirstOrDefault();

                if (language == null)
                {
                    throw new KeyNotFoundException
                    (
                        "No languages have been registered."
                    );
                }
            }

            return language;
        }

        /// <summary>
        /// Gets all registered languages in the repository
        /// </summary>
        /// <returns>A collection of registered languages</returns>
        public IEnumerable<RegisteredLanguage> GetAllLanguages()
        {
            var languages = _context.Set<RegisteredLanguage>();

            return languages.OrderBy
            (
                a => a.Name
            );
        }

        /// <summary>
        /// Updates a single registered language in the repository
        /// </summary>
        /// <param name="language">The registered language to update</param>
        public void UpdateLanguage
            (
                RegisteredLanguage language
            )
        {
            Validate.IsNotNull(language);

            var entry = _context.Entry<RegisteredLanguage>
            (
                language
            );

            entry.State = EntityState.Modified;
        }

        /// <summary>
        /// Removes a single registered language from the repository
        /// </summary>
        /// <param name="name">The name of the language</param>
        public void RemoveLanguage
            (
                Guid id
            )
        {
            var language = GetLanguage(id);

            var entry = _context.Entry<RegisteredLanguage>
            (
                language
            );

            // Ensure the entity has been attached to the object state manager
            if (entry.State == EntityState.Detached)
            {
                _context.Set<RegisteredLanguage>().Attach
                (
                    language
                );
            }

            _context.Set<RegisteredLanguage>().Remove
            (
                language
            );
        }
    }
}
