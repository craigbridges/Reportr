namespace Reportr.Registration.Entity.Configurations
{
    using Reportr.Registration.Globalization;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.ModelConfiguration;

    /// <summary>
    /// Represents an entity type configuration for a registered language
    /// </summary>
    public class RegisteredLanguageEntityConfiguration : EntityTypeConfiguration<RegisteredLanguage>
    {
        public RegisteredLanguageEntityConfiguration()
            : base()
        {
            var indexAttribute = new IndexAttribute()
            {
                IsUnique = true
            };

            HasKey
            (
                m => new { m.Id }
            );

            // Configure the primary key constraints
            Property(m => m.Id)
                .HasColumnAnnotation
                (
                    "Index",
                    new IndexAnnotation(indexAttribute)
                );

            Map
            (
                m =>
                {
                    m.ToTable("RegisteredLanguages");
                }
            );
        }
    }
}
