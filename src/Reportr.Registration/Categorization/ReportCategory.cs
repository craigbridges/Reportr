namespace Reportr.Registration.Categorization
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;

    /// <summary>
    /// Represents a single report category
    /// </summary>
    public class ReportCategory : IAggregate
    {
        /// <summary>
        /// Constructs the report category with its default configuration
        /// </summary>
        protected ReportCategory() { }

        /// <summary>
        /// Constructs the category with a name and description
        /// </summary>
        /// <param name="configuration">The category configuration</param>
        public ReportCategory
            (
                ReportCategoryConfiguration configuration
            )
        {
            this.Id = Guid.NewGuid();
            this.DateCreated = DateTime.UtcNow;

            this.SubCategories = new Collection<ReportCategory>();
            this.AssignedReports = new Collection<ReportCategoryAssignment>();

            Configure(configuration);
        }

        /// <summary>
        /// Constructs the category with a parent, name and description
        /// </summary>
        /// <param name="parentCategory">The parent category</param>
        /// <param name="configuration">The category configuration</param>
        protected ReportCategory
            (
                ReportCategory parentCategory,
                ReportCategoryConfiguration configuration
            )

            : this(configuration)
        {
            Validate.IsNotNull(parentCategory);

            this.ParentCategory = parentCategory;
        }

        /// <summary>
        /// Gets the category ID
        /// </summary>
        public Guid Id { get; protected set; }

        /// <summary>
        /// Gets the version number of the report category
        /// </summary>
        public int Version { get; protected set; }

        /// <summary>
        /// Gets the date and time the report category was created
        /// </summary>
        public DateTime DateCreated { get; protected set; }

        /// <summary>
        /// Gets the date and time the report category was modified
        /// </summary>
        public DateTime DateModified { get; protected set; }

        /// <summary>
        /// Gets the parent report category
        /// </summary>
        public virtual ReportCategory ParentCategory { get; protected set; }

        /// <summary>
        /// Gets the parent report category ID
        /// </summary>
        public Guid? ParentCategoryId { get; protected set; }

        /// <summary>
        /// Gets the category name
        /// </summary>
        public string Name { get; protected set; }

        /// <summary>
        /// Gets the category title
        /// </summary>
        public string Title { get; protected set; }

        /// <summary>
        /// Gets the category description
        /// </summary>
        public string Description { get; protected set; }

        /// <summary>
        /// Configures the report category
        /// </summary>
        /// <param name="configuration">The category configuration</param>
        public void Configure
            (
                ReportCategoryConfiguration configuration
            )
        {
            Validate.IsNotNull(configuration);

            if (String.IsNullOrWhiteSpace(configuration.Name))
            {
                throw new ArgumentException
                (
                    "The report category name is required."
                );
            }

            if (String.IsNullOrWhiteSpace(configuration.Title))
            {
                throw new ArgumentException
                (
                    "The report category title is required."
                );
            }

            this.Name = configuration.Name;
            this.Title = configuration.Title;
            this.Description = configuration.Description;

            this.DateModified = DateTime.UtcNow;
            this.Version++;
        }

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
        /// <param name="configuration">The category configuration</param>
        /// <returns>The category created</returns>
        public ReportCategory CreateSubCategory
            (
                ReportCategoryConfiguration configuration
            )
        {
            var subCategory = new ReportCategory
            (
                this,
                configuration
            );

            this.SubCategories.Add(subCategory);

            this.DateModified = DateTime.UtcNow;
            this.Version++;

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
                c => c.Id == id
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

            this.DateModified = DateTime.UtcNow;
            this.Version++;
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
        /// <param name="reportName">The report name</param>
        public void AssignReport
            (
                string reportName
            )
        {
            Validate.IsNotEmpty(reportName);

            var isAssigned = IsReportAssigned
            (
                reportName
            );

            if (isAssigned)
            {
                var message = "The report '{0}' has already been assigned to '{1}'.";

                throw new InvalidOperationException
                (
                    String.Format
                    (
                        message,
                        reportName,
                        this.Name
                    )
                );
            }

            var assignment = new ReportCategoryAssignment
            (
                this,
                reportName
            );

            this.AssignedReports.Add
            (
                assignment
            );

            this.DateModified = DateTime.UtcNow;
            this.Version++;
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

            this.AssignedReports.Remove
            (
                assignment
            );

            this.DateModified = DateTime.UtcNow;
            this.Version++;
        }
    }
}
