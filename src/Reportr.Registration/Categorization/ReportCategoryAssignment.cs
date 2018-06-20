namespace Reportr.Registration.Categorization
{
    using System;

    /// <summary>
    /// Represents a single report category assignment
    /// </summary>
    public class ReportCategoryAssignment
    {
        /// <summary>
        /// Constructs the assignment with the report details
        /// </summary>
        /// <param name="reportName">The report name</param>
        /// <param name="reportTitle">The report title</param>
        internal ReportCategoryAssignment
            (
                string reportName,
                string reportTitle
            )
        {
            Validate.IsNotEmpty(reportName);
            Validate.IsNotEmpty(reportTitle);

            this.AssignmentId = Guid.NewGuid();
            this.ReportName = reportName;
            this.ReportTitle = reportTitle;
        }

        /// <summary>
        /// Gets the ID of the assignment
        /// </summary>
        public Guid AssignmentId { get; protected set; }

        /// <summary>
        /// Gets the report name
        /// </summary>
        public string ReportName { get; protected set; }

        /// <summary>
        /// Gets the report title
        /// </summary>
        public string ReportTitle { get; protected set; }
    }
}
