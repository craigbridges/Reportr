namespace Reportr.Registration.Entity.Configurations
{
    using Reportr.Registration.Authorization;
    using System.Data.Entity.ModelConfiguration;

    /// <summary>
    /// Represents an entity type configuration for a report parameter constraint
    /// </summary>
    public class ReportParameterConstraintEntityConfiguration
        : EntityTypeConfiguration<ReportParameterConstraint>
    {
        public ReportParameterConstraintEntityConfiguration()
            : base()
        {
            HasKey
            (
                m => new
                {
                    m.Id,
                    m.AssignmentId
                }
            );

            // Set up a foreign key reference for cascade deletes
            HasRequired(m => m.Assignment)
                .WithMany(m => m.ParameterConstraints)
                .HasForeignKey
                (
                    m => new
                    {
                        m.AssignmentId
                    }
                )
                .WillCascadeOnDelete();

            Map
            (
                m =>
                {
                    m.ToTable("ReportParameterConstraints");
                }
            );
        }
    }
}
