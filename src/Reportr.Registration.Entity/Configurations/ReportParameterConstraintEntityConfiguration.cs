namespace Reportr.Registration.Entity.Configurations
{
    using Humanizer;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Reportr.Registration.Authorization;
    
    /// <summary>
    /// Represents an entity type configuration for a report parameter constraint
    /// </summary>
    public class ReportParameterConstraintEntityConfiguration
        : IEntityTypeConfiguration<ReportParameterConstraint>
    {
        public void Configure
            (
                EntityTypeBuilder<ReportParameterConstraint> builder
            )
        {
            builder.HasKey
            (
                m => new
                {
                    m.Id,
                    m.AssignmentId
                }
            );

            builder.HasOne(m => m.Assignment)
                .WithMany(m => m.ParameterConstraints)
                .HasForeignKey
                (
                    m => new
                    {
                        m.AssignmentId
                    }
                )
                .OnDelete(DeleteBehavior.Cascade);

            builder.ToTable
            (
                nameof(ReportParameterConstraint).Pluralize()
            );
        }
    }
}
