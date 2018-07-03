namespace Reportr.Registration.Entity.Configurations
{
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.ModelConfiguration;

    /// <summary>
    /// Represents an entity type configuration for a registered report
    /// </summary>
    public class RegisteredReportConfiguration : EntityTypeConfiguration<RegisteredReport>
    {
        public RegisteredReportConfiguration()
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

            // Configure the primary key contraints
            Property(m => m.Id)
                .HasDatabaseGeneratedOption
                (
                    DatabaseGeneratedOption.Identity
                )
                .HasColumnAnnotation
                (
                    "Index",
                    new IndexAnnotation(indexAttribute)
                );

            Map
            (
                m =>
                {
                    m.ToTable("RegisteredReports");
                }
            );
        }
    }
}
