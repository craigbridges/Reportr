namespace Reportr.Registration.Entity.Configurations
{
    using Reportr.Registration.Categorization;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.ModelConfiguration;

    /// <summary>
    /// Represents an entity type configuration for a report category assignment
    /// </summary>
    public class ReportCategoryAssignmentConfiguration
        : EntityTypeConfiguration<ReportCategoryAssignment>
    {
        public ReportCategoryAssignmentConfiguration()
            : base()
        {
            HasKey
            (
                m => new
                {
                    m.AssignmentId,
                    m.CategoryId
                }
            );

            // Configure the primary key constraints
            Property(m => m.AssignmentId)
                .HasDatabaseGeneratedOption
                (
                    DatabaseGeneratedOption.Identity
                );

            // Set up a foreign key reference for cascade deletes
            HasRequired(m => m.Category)
                .WithMany(m => m.AssignedReports)
                .HasForeignKey
                (
                    m => new
                    {
                        m.AssignmentId,
                        m.CategoryId
                    }
                )
                .WillCascadeOnDelete();

            Map
            (
                m =>
                {
                    m.ToTable("ReportCategoryAssignments");
                }
            );
        }
    }
}
