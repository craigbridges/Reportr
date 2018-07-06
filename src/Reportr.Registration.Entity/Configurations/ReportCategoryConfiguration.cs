namespace Reportr.Registration.Entity.Configurations
{
    using Reportr.Registration.Categorization;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.ModelConfiguration;

    /// <summary>
    /// Represents an entity type configuration for a report category
    /// </summary>
    public class ReportCategoryConfiguration : EntityTypeConfiguration<ReportCategory>
    {
        public ReportCategoryConfiguration()
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
                    m.ToTable("ReportCategories");
                }
            );
        }
    }
}
