namespace Reportr.Registration.Entity.Configurations
{
    using Humanizer;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Reportr.Registration.Globalization;

    /// <summary>
    /// Represents an entity type configuration for a report category assignment
    /// </summary>
    public class RegisteredPhraseTranslationEntityConfiguration
        : IEntityTypeConfiguration<RegisteredPhraseTranslation>
    {
        public void Configure
            (
                EntityTypeBuilder<RegisteredPhraseTranslation> builder
            )
        {
            builder.HasKey
            (
                m => new
                {
                    m.PhraseId,
                    m.TranslationId
                }
            );

            builder.HasOne(m => m.Phrase)
                .WithMany(m => m.Translations)
                .HasForeignKey
                (
                    m => new
                    {
                        m.PhraseId
                    }
                )
                .OnDelete(DeleteBehavior.Cascade);

            builder.ToTable
            (
                nameof(RegisteredPhraseTranslation).Pluralize()
            );
        }
    }
}
