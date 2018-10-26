namespace Reportr.Registration.Entity.Configurations
{
    using Humanizer;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Reportr.Registration.Categorization;
    
    /// <summary>
    /// Represents an entity type configuration for a report category assignment
    /// </summary>
    public class ReportCategoryAssignmentEntityConfiguration
        : IEntityTypeConfiguration<ReportCategoryAssignment>
    {
        public void Configure
            (
                EntityTypeBuilder<ReportCategoryAssignment> builder
            )
        {
            builder.HasKey
            (
                m => new
                {
                    m.AssignmentId,
                    m.CategoryId
                }
            );

            builder.HasOne(m => m.Category)
                .WithMany(m => m.AssignedReports)
                .HasForeignKey
                (
                    m => new
                    {
                        m.CategoryId
                    }
                )
                .OnDelete(DeleteBehavior.Cascade);

            builder.ToTable
            (
                nameof(ReportCategoryAssignment).Pluralize()
            );
        }
    }
}
