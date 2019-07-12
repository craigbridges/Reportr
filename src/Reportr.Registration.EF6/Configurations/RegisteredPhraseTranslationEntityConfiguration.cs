namespace Reportr.Registration.Entity.Configurations
{
    using Reportr.Registration.Globalization;
    using System.Data.Entity.ModelConfiguration;

    /// <summary>
    /// Represents an entity type configuration for a registered phrase translation
    /// </summary>
    public class RegisteredPhraseTranslationEntityConfiguration
        : EntityTypeConfiguration<RegisteredPhraseTranslation>
    {
        public RegisteredPhraseTranslationEntityConfiguration()
            : base()
        {
            HasKey
            (
                m => new
                {
                    m.PhraseId,
                    m.TranslationId
                }
            );

            // Set up a foreign key reference for cascade deletes
            HasRequired(m => m.Phrase)
                .WithMany(m => m.Translations)
                .HasForeignKey
                (
                    m => new
                    {
                        m.PhraseId
                    }
                )
                .WillCascadeOnDelete();

            Map
            (
                m =>
                {
                    m.ToTable("RegisteredPhraseTranslations");
                }
            );
        }
    }
}
