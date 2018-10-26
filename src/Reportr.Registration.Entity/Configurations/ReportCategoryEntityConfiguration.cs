namespace Reportr.Registration.Entity.Configurations
{
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Reportr.Registration.Categorization;
    
    /// <summary>
    /// Represents an entity type configuration for a report category
    /// </summary>
    public class ReportCategoryEntityConfiguration : AggregateEntityTypeConfiguration<ReportCategory>
    {
        protected override void ApplyCustomConfiguration
            (
                EntityTypeBuilder<ReportCategory> builder
            )
        {
            builder.HasOne(m => m.ParentCategory)
                .WithMany(m => m.SubCategories)
                .HasForeignKey
                (
                    m => new
                    {
                        m.ParentCategoryId
                    }
                );
        }
    }
}
