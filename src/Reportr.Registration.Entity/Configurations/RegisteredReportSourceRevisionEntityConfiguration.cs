namespace Reportr.Registration.Entity.Configurations
{
    using Humanizer;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    
    /// <summary>
    /// Represents an entity type configuration for a registered report source revision
    /// </summary>
    public class RegisteredReportSourceRevisionEntityConfiguration
        : IEntityTypeConfiguration<RegisteredReportSourceRevision>
    {
        public void Configure
            (
                EntityTypeBuilder<RegisteredReportSourceRevision> builder
            )
        {
            builder.HasKey
            (
                m => new
                {
                    m.RevisionId,
                    m.ReportId
                }
            );

            builder.HasOne(m => m.Report)
                .WithMany(m => m.SourceRevisions)
                .HasForeignKey
                (
                    m => new
                    {
                        m.ReportId
                    }
                )
                .OnDelete(DeleteBehavior.Cascade);

            builder.ToTable
            (
                nameof(RegisteredReportSourceRevision).Pluralize()
            );
        }
    }
}
