namespace Reportr.Registration.Categorization
{
    using System;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Represents a single report category assignment
    /// </summary>
    public class ReportCategoryAssignment
    {
        /// <summary>
        /// Constructs the assignment with its default configuration
        /// </summary>
        protected ReportCategoryAssignment() { }

        /// <summary>
        /// Constructs the assignment with the report name
        /// </summary>
        /// <param name="category">The category</param>
        /// <param name="reportName">The report name</param>
        internal ReportCategoryAssignment
            (
                ReportCategory category,
                string reportName
            )
        {
            Validate.IsNotNull(category);
            Validate.IsNotEmpty(reportName);

            this.Category = category;
            this.AssignmentId = Guid.NewGuid();
            this.ReportName = reportName;
        }

        /// <summary>
        /// Gets the associated category
        /// </summary>
        public virtual ReportCategory Category { get; protected set; }

        /// <summary>
        /// Gets the ID of the associated category
        /// </summary>
        public Guid CategoryId { get; protected set; }

        /// <summary>
        /// Gets the ID of the assignment
        /// </summary>
        [Key]
        public Guid AssignmentId { get; protected set; }

        /// <summary>
        /// Gets the report name
        /// </summary>
        public string ReportName { get; protected set; }
    }
}
