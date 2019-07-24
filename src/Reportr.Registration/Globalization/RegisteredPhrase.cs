namespace Reportr.Registration.Globalization
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Represents a registered phrase
    /// </summary>
    public class RegisteredPhrase : IAggregate
    {
        /// <summary>
        /// Constructs the phrase with its default configuration
        /// </summary>
        protected RegisteredPhrase() { }

        /// <summary>
        /// Constructs the phrase with the text
        /// </summary>
        /// <param name="configuration">The phrase configuration</param>
        internal RegisteredPhrase
            (
                RegisteredPhraseConfiguration configuration
            )
        {
            this.Id = Guid.NewGuid();
            this.DateCreated = DateTime.UtcNow;
            this.Translations = new Collection<RegisteredPhraseTranslation>();

            Configure(configuration);
        }

        /// <summary>
        /// Gets the unique ID of the registered report
        /// </summary>
        [Key]
        public Guid Id { get; protected set; }

        /// <summary>
        /// Gets the version number of the registered report
        /// </summary>
        public int Version { get; protected set; }

        /// <summary>
        /// Gets the date and time the registered report was created
        /// </summary>
        public DateTime DateCreated { get; protected set; }

        /// <summary>
        /// Gets the date and time the registered report was modified
        /// </summary>
        public DateTime DateModified { get; protected set; }

        /// <summary>
        /// Gets the phrase text in the default language
        /// </summary>
        public string PhraseText { get; protected set; }

        /// <summary>
        /// Gets a collection of phrase translations
        /// </summary>
        public virtual ICollection<RegisteredPhraseTranslation> Translations
        {
            get;
            protected set;
        }

        /// <summary>
        /// Configures the phrase
        /// </summary>
        /// <param name="configuration">The phrase configuration</param>
        public void Configure
            (
                RegisteredPhraseConfiguration configuration
            )
        {
            this.PhraseText = configuration.PhraseText;

            if (configuration.Translations != null)
            {
                this.Translations.Clear();

                foreach (var translation in configuration.Translations)
                {
                    this.Translations.Add
                    (
                        new RegisteredPhraseTranslation
                        (
                            this,
                            translation.Key,
                            translation.Value
                        )
                    );
                }
            }

            this.DateModified = DateTime.UtcNow;
            this.Version++;
        }
    }
}
