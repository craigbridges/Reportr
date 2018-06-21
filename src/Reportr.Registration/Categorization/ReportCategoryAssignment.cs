namespace Reportr.Registration.Categorization
{
    using System;

    /// <summary>
    /// Represents a single report category assignment
    /// </summary>
    public class ReportCategoryAssignment
    {
        /// <summary>
        /// Constructs the assignment with the report name
        /// </summary>
        /// <param name="reportName">The report name</param>
        internal ReportCategoryAssignment
            (
                string reportName
            )
        {
            Validate.IsNotEmpty(reportName);
            
            this.AssignmentId = Guid.NewGuid();
            this.ReportName = reportName;
        }

        /// <summary>
        /// Gets the ID of the assignment
        /// </summary>
        public Guid AssignmentId { get; protected set; }

        /// <summary>
        /// Gets the report name
        /// </summary>
        public string ReportName { get; protected set; }
    }
}
