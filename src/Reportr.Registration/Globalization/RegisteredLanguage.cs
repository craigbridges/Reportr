namespace Reportr.Registration.Globalization
{
    using Reportr.Globalization;
    using System;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Represents a single registered language
    /// </summary>
    public class RegisteredLanguage : IAggregate
    {
        /// <summary>
        /// Constructs the registered report with its default configuration
        /// </summary>
        protected RegisteredLanguage() { }

        /// <summary>
        /// Constructs the registered language with basic details
        /// </summary>
        /// <param name="configuration">The language configuration</param>
        public RegisteredLanguage
            (
                RegisteredLanguageConfiguration configuration
            )
        {
            this.Id = Guid.NewGuid();
            this.DateCreated = DateTime.UtcNow;

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
        /// Gets the name of the language
        /// </summary>
        public string Name { get; protected set; }

        /// <summary>
        /// Gets the language ISO
        /// </summary>
        public string Iso { get; protected set; }

        /// <summary>
        /// Gets a flag indicating if the language is the default
        /// </summary>
        public bool Default { get; protected set; }

        /// <summary>
        /// Configures the registered language
        /// </summary>
        /// <param name="configuration">The language configuration</param>
        public void Configure
            (
                RegisteredLanguageConfiguration configuration
            )
        {
            Validate.IsNotNull(configuration);

            if (String.IsNullOrWhiteSpace(configuration.Name))
            {
                throw new ArgumentException
                (
                    "The language name is required."
                );
            }

            if (String.IsNullOrWhiteSpace(configuration.Iso))
            {
                throw new ArgumentException
                (
                    "The language ISO is required."
                );
            }

            this.Name = configuration.Name;
            this.Iso = configuration.Iso;
            this.Default = configuration.Default;

            this.DateModified = DateTime.UtcNow;
            this.Version++;
        }

        /// <summary>
        /// Converts the registered language to a language instance
        /// </summary>
        /// <returns>The language generated</returns>
        public Language ToLanguage()
        {
            return new Language
            (
                this.Name,
                this.Iso,
                this.Default
            );
        }

        /// <summary>
        /// Provides a descriptor for the objects current state
        /// </summary>
        /// <returns>The language name</returns>
        public override string ToString()
        {
            return $"{this.Name}";
        }
    }
}
