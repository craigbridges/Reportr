namespace Reportr.Categorization
{
    using System;
    using System.Collections.Generic;
    
    /// <summary>
    /// Defines a report for a repository that manages report categories
    /// </summary>
    public interface IReportCategoryRepository
    {
        /// <summary>
        /// Adds a category to the repository
        /// </summary>
        /// <param name="category">The category to add</param>
        void AddCategory
        (
            ReportCategory category
        );

        /// <summary>
        /// Gets a category from the repository
        /// </summary>
        /// <param name="id">The ID of the category to get</param>
        /// <returns>The matching category</returns>
        ReportCategory GetCategory
        (
            Guid id
        );

        /// <summary>
        /// Gets all categories in the repository
        /// </summary>
        /// <returns>A collection of categories</returns>
        IEnumerable<ReportCategory> GetAllCategories();

        /// <summary>
        /// Removes a category from the repository
        /// </summary>
        /// <param name="id">The ID of the category to remove</param>
        void RemoveCategory
        (
            Guid id
        );
    }
}
