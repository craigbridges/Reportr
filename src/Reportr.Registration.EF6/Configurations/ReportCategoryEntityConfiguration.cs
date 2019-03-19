namespace Reportr.Registration.Entity.Configurations
{
    using Reportr.Registration.Categorization;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.ModelConfiguration;

    /// <summary>
    /// Represents an entity type configuration for a report category
    /// </summary>
    public class ReportCategoryEntityConfiguration : EntityTypeConfiguration<ReportCategory>
    {
        public ReportCategoryEntityConfiguration()
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

            // Set up a foreign key reference for cascade deletes
            HasOptional(m => m.ParentCategory)
                .WithMany(m => m.SubCategories)
                .HasForeignKey
                (
                    m => new
                    {
                        m.ParentCategoryId
                    }
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
