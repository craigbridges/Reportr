namespace Reportr.Registration.Categorization
{
    using System.Collections.Generic;
    
    /// <summary>
    /// Defines a contract for a report categorizer
    /// </summary>
    public interface IReportCategorizer
    {
        /// <summary>
        /// Creates a single root level report category
        /// </summary>
        /// <param name="name">The category name</param>
        /// <param name="title">The category title</param>
        /// <param name="description">A description of the category</param>
        /// <returns>The category created</returns>
        ReportCategory CreateCategory
        (
            string name,
            string title,
            string description
        );

        /// <summary>
        /// Creates a single report sub category
        /// </summary>
        /// <param name="parentCategoryId">The parent category name</param>
        /// <param name="name">The category name</param>
        /// <param name="title">The category title</param>
        /// <param name="description">A description of the category</param>
        /// <returns>The category created</returns>
        ReportCategory CreateSubCategory
        (
            string parentCategoryId,
            string name,
            string title,
            string description
        );

        /// <summary>
        /// Determines if a category name is available
        /// </summary>
        /// <param name="categoryName">The category name</param>
        /// <returns>True, if the name is available; otherwise false</returns>
        bool IsNameAvailable
        (
            string categoryName
        );

        /// <summary>
        /// Gets a single report category
        /// </summary>
        /// <param name="name">The category name</param>
        /// <returns>The matching category</returns>
        ReportCategory GetCategory
        (
            string name
        );

        /// <summary>
        /// Gets all root level report categories
        /// </summary>
        /// <returns>A collection of report categories</returns>
        IEnumerable<ReportCategory> GetAllCategories();

        /// <summary>
        /// Gets sub report categories from a parent category
        /// </summary>
        /// <param name="parentCategoryName">The parent category name</param>
        /// <returns>A collection of report categories</returns>
        IEnumerable<ReportCategory> GetSubCategories
        (
            string parentCategoryName
        );

        /// <summary>
        /// Assigns a single report to a category
        /// </summary>
        /// <param name="categoryName">The category name</param>
        /// <param name="reportName">The report name</param>
        void AssignToCategory
        (
            string categoryName,
            string reportName
        );

        /// <summary>
        /// Unassigns a single report from a category
        /// </summary>
        /// <param name="categoryName">The category name</param>
        /// <param name="reportName">The report name</param>
        void UnassignFromCategory
        (
            string categoryName,
            string reportName
        );

        /// <summary>
        /// Determines if a report has been assigned to a category
        /// </summary>
        /// <param name="categoryName">The category name</param>
        /// <param name="reportName">The report name</param>
        /// <returns>True, if the report has been assigned; otherwise false</returns>
        bool IsAssignedToCategory
        (
            string categoryName,
            string reportName
        );

        /// <summary>
        /// Deletes a single report category
        /// </summary>
        /// <param name="name">The category name</param>
        void DeleteCategory
        (
            string name
        );
    }
}
