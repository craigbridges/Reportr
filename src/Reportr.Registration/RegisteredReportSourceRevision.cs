namespace Reportr.Registration
{
    using System;
    
    /// <summary>
    /// Represents a single registered report source revision
    /// </summary>
    public class RegisteredReportSourceRevision
    {
        /// <summary>
        /// Constructs the source revision with the report definition
        /// </summary>
        /// <param name="report">The report definition</param>
        internal RegisteredReportSourceRevision
            (
                RegisteredReport report
            )
        {
            Validate.IsNotNull(report);

            this.Report = report;
            this.Number = report.SourceRevisions.Count + 1;
            this.DateOriginallySpecified = report.DateSourceSpecified;
            this.DateRevised = DateTime.UtcNow;
            this.SourceType = report.SourceType;
            this.BuilderTypeName = report.BuilderTypeName;
            this.ScriptSourceCode = report.ScriptSourceCode;
        }

        /// <summary>
        /// Gets the source revision ID
        /// </summary>
        public Guid RevisionId { get; protected set; }

        /// <summary>
        /// Gets the associated registered report
        /// </summary>
        public virtual RegisteredReport Report { get; protected set; }

        /// <summary>
        /// Gets the registered report ID
        /// </summary>
        public Guid ReportId { get; protected set; }

        /// <summary>
        /// Gets the revision number
        /// </summary>
        public int Number { get; protected set; }

        /// <summary>
        /// Gets the date and time the source was originally specified
        /// </summary>
        public DateTime DateOriginallySpecified { get; protected set; }

        /// <summary>
        /// Gets the date and time the source was revised
        /// </summary>
        public DateTime DateRevised { get; protected set; }

        /// <summary>
        /// Gets the report definition source type
        /// </summary>
        public ReportDefinitionSourceType SourceType { get; protected set; }

        /// <summary>
        /// Gets the report definition builder type name
        /// </summary>
        public string BuilderTypeName { get; protected set; }

        /// <summary>
        /// Gets the source code of the report definition script
        /// </summary>
        public string ScriptSourceCode { get; protected set; }
    }
}
