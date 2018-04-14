namespace Reportr.Categorization
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;

    /// <summary>
    /// Represents a single report category
    /// </summary>
    public class ReportCategory
    {
        /// <summary>
        /// Constructs the category with a name and description
        /// </summary>
        /// <param name="name">The name</param>
        /// <param name="description">The description</param>
        public ReportCategory
            (
                string name,
                string description = null
            )
        {
            Validate.IsNotEmpty(name);

            this.CategoryId = Guid.NewGuid();
            this.Name = name;
            this.Description = description;
            this.SubCategories = new Collection<ReportCategory>();
            this.AssignedReports = new Collection<ReportCategoryAssignment>();
        }

        /// <summary>
        /// Constructs the category with a parent, name and description
        /// </summary>
        /// <param name="parentCategory">The parent category</param>
        /// <param name="name">The name</param>
        /// <param name="description">The description</param>
        protected ReportCategory
            (
                ReportCategory parentCategory,
                string name,
                string description = null
            )

            : this(name, description)
        {
            Validate.IsNotNull(parentCategory);

            this.ParentCategory = parentCategory;
        }

        /// <summary>
        /// Gets the category ID
        /// </summary>
        public Guid CategoryId { get; protected set; }

        /// <summary>
        /// Gets the parent report category
        /// </summary>
        public virtual ReportCategory ParentCategory { get; protected set; }

        /// <summary>
        /// Gets the category name
        /// </summary>
        public string Name { get; protected set; }

        /// <summary>
        /// Gets the category description
        /// </summary>
        public string Description { get; protected set; }

        /// <summary>
        /// Gets a collection of report sub-categories
        /// </summary>
        public virtual ICollection<ReportCategory> SubCategories
        {
            get;
            protected set;
        }

        /// <summary>
        /// Creates a report sub category and against the category
        /// </summary>
        /// <param name="name">The category name</param>
        /// <param name="description">The category description</param>
        /// <returns>The category created</returns>
        public ReportCategory CreateSubCategory
            (
                string name,
                string description = null
            )
        {
            Validate.IsNotEmpty(name);

            var subCategory = new ReportCategory
            (
                this,
                name,
                description
            );

            this.SubCategories.Add(subCategory);

            return subCategory;
        }

        /// <summary>
        /// Gets a single sub category from the category
        /// </summary>
        /// <param name="id">The sub category ID</param>
        /// <returns>The sub category</returns>
        public ReportCategory GetSubCategory
            (
                Guid id
            )
        {
            var subCategory = this.SubCategories.FirstOrDefault
            (
                c => c.CategoryId == id
            );

            if (subCategory == null)
            {
                throw new KeyNotFoundException
                (
                    "The ID specified did not match any sub categories."
                );
            }

            return subCategory;
        }

        /// <summary>
        /// Removes a single sub category from the category
        /// </summary>
        /// <param name="id">The sub category ID</param>
        public void RemoveSubCategory
            (
                Guid id
            )
        {
            var subCategory = GetSubCategory(id);

            this.SubCategories.Remove
            (
                subCategory
            );
        }

        /// <summary>
        /// Gets a collection of reports assigned to the category
        /// </summary>
        public virtual ICollection<ReportCategoryAssignment> AssignedReports
        {
            get;
            protected set;
        }

        /// <summary>
        /// Assigns a single report to the category
        /// </summary>
        /// <param name="report">The report to assign</param>
        public void AssignReport
            (
                IReport report
            )
        {
            Validate.IsNotNull(report);

            var isAssigned = IsReportAssigned
            (
                report.Name
            );

            if (isAssigned)
            {
                var message = "The report '{0}' has already been assigned to '{1}'.";

                throw new InvalidOperationException
                (
                    String.Format
                    (
                        message,
                        report.Name,
                        this.Name
                    )
                );
            }

            var assignment = new ReportCategoryAssignment
            (
                report.Name,
                report.Title
            );

            this.AssignedReports.Add(assignment);
        }

        /// <summary>
        /// Determines if a report has been assigned to the category
        /// </summary>
        /// <param name="reportName">he report name</param>
        /// <returns>True, if the report has been assigned; otherwise false</returns>
        public bool IsReportAssigned
            (
                string reportName
            )
        {
            Validate.IsNotEmpty(reportName);

            return this.AssignedReports.Any
            (
                a => a.ReportName.ToLower() == reportName.ToLower()
            );
        }

        /// <summary>
        /// Unassigns a report from the category
        /// </summary>
        /// <param name="reportName">The report name</param>
        public void UnassignReport
            (
                string reportName
            )
        {
            Validate.IsNotEmpty(reportName);

            var assignment = this.AssignedReports.FirstOrDefault
            (
                a => a.ReportName.ToLower() == reportName.ToLower()
            );

            if (assignment == null)
            {
                var message = "The report '{0}' has not been assigned to '{1}'.";

                throw new KeyNotFoundException
                (
                    String.Format
                    (
                        message,
                        reportName,
                        this.Name
                    )
                );
            }

            this.AssignedReports.Remove(assignment);
        }
    }
}
