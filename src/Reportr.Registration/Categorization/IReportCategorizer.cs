namespace Reportr.Registration.Categorization
{
    using System;
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
        /// <param name="description">A description of the category</param>
        /// <returns>The category created</returns>
        ReportCategory CreateCategory
        (
            string name,
            string description
        );

        /// <summary>
        /// Creates a single report sub category
        /// </summary>
        /// <param name="parentCategoryId">The parent category ID</param>
        /// <param name="name">The category name</param>
        /// <param name="description">A description of the category</param>
        /// <returns>The category created</returns>
        ReportCategory CreateSubCategory
        (
            Guid parentCategoryId,
            string name,
            string description
        );

        /// <summary>
        /// Gets all root level report categories
        /// </summary>
        /// <returns>A collection of report categories</returns>
        IEnumerable<ReportCategory> GetAllCategories();

        /// <summary>
        /// Gets sub report categories from a parent category
        /// </summary>
        /// <param name="parentCategoryId">The parent category ID</param>
        /// <returns>A collection of report categories</returns>
        IEnumerable<ReportCategory> GetSubCategories
        (
            Guid parentCategoryId
        );

        /// <summary>
        /// Assigns a single report to a category
        /// </summary>
        /// <param name="categoryId">The category ID</param>
        /// <param name="reportName">The report name</param>
        void AssignToCategory
        (
            Guid categoryId,
            string reportName
        );

        /// <summary>
        /// Unassigns a single report from a category
        /// </summary>
        /// <param name="categoryId">The category ID</param>
        /// <param name="reportName">The report name</param>
        void UnassignFromCategory
        (
            Guid categoryId,
            string reportName
        );

        /// <summary>
        /// Determines if a report has been assigned to a category
        /// </summary>
        /// <param name="categoryId">The category ID</param>
        /// <param name="reportName">The report name</param>
        /// <returns>True, if the report has been assigned; otherwise false</returns>
        bool IsAssignedToCategory
        (
            Guid categoryId,
            string reportName
        );

        /// <summary>
        /// Deletes a single report category
        /// </summary>
        /// <param name="id">The category ID</param>
        void DeleteCategory
        (
            Guid id
        );
    }
}
