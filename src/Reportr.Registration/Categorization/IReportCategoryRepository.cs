namespace Reportr.Registration.Categorization
{
    using System;
    using System.Collections.Generic;
    
    /// <summary>
    /// Defines a report for a repository that manages report categories
    /// </summary>
    public interface IReportCategoryRepository
    {
        /// <summary>
        /// Adds a single report category to the repository
        /// </summary>
        /// <param name="category">The category to add</param>
        void AddCategory
        (
            ReportCategory category
        );

        /// <summary>
        /// Gets a single report category from the repository
        /// </summary>
        /// <param name="id">The ID of the category to get</param>
        /// <returns>The matching category</returns>
        ReportCategory GetCategory
        (
            Guid id
        );

        /// <summary>
        /// Gets all report categories in the repository
        /// </summary>
        /// <returns>A collection of categories</returns>
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
        /// Updates a single report category to the repository
        /// </summary>
        /// <param name="category">The category to update</param>
        void UpdateCategory
        (
            ReportCategory category
        );

        /// <summary>
        /// Removes a single report category from the repository
        /// </summary>
        /// <param name="id">The ID of the category to remove</param>
        void RemoveCategory
        (
            Guid id
        );
    }
}
