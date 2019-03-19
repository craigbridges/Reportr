namespace Reportr.Registration.Entity.Configurations
{
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.ModelConfiguration;

    /// <summary>
    /// Represents an entity type configuration for a registered report source revision
    /// </summary>
    public class RegisteredReportSourceRevisionEntityConfiguration 
        : EntityTypeConfiguration<RegisteredReportSourceRevision>
    {
        public RegisteredReportSourceRevisionEntityConfiguration()
            : base()
        {
            HasKey
            (
                m => new
                {
                    m.RevisionId,
                    m.ReportId
                }
            );

            // Set up a foreign key reference for cascade deletes
            HasRequired(m => m.Report)
                .WithMany(m => m.SourceRevisions)
                .HasForeignKey
                (
                    m => new
                    {
                        m.ReportId
                    }
                )
                .WillCascadeOnDelete();

            Map
            (
                m =>
                {
                    m.ToTable("RegisteredReportSourceRevisions");
                }
            );
        }
    }
}
