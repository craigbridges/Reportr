namespace Reportr.Registration.Entity.Repositories
{
    using Microsoft.EntityFrameworkCore;
    using Reportr.Registration.Categorization;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Represents an Entity Framework registered report category repository
    /// </summary>
    public sealed class EfReportCategoryRepository : IReportCategoryRepository
    {
        private readonly ReportrDbContext _context;

        /// <summary>
        /// Constructs the repository with a database context
        /// </summary>
        /// <param name="context">The database context</param>
        public EfReportCategoryRepository
            (
                ReportrDbContext context
            )
        {
            Validate.IsNotNull(context);

            _context = context;
        }

        /// <summary>
        /// Adds a single report category to the repository
        /// </summary>
        /// <param name="category">The category to add</param>
        public void AddCategory
            (
                ReportCategory category
            )
        {
            Validate.IsNotNull(category);

            _context.Set<ReportCategory>().Add
            (
                category
            );
        }

        /// <summary>
        /// Determines if a category name is available
        /// </summary>
        /// <param name="categoryName">The category name</param>
        /// <returns>True, if the name is available; otherwise false</returns>
        public bool IsNameAvailable
            (
                string categoryName
            )
        {
            Validate.IsNotEmpty(categoryName);

            var set = _context.Set<ReportCategory>();

            var nameUsed = set.Any
            (
                c => c.Name.Equals(categoryName, StringComparison.OrdinalIgnoreCase)
            );

            return false == nameUsed;
        }

        /// <summary>
        /// Gets a single report category from the repository
        /// </summary>
        /// <param name="id">The ID of the category to get</param>
        /// <returns>The matching category</returns>
        public ReportCategory GetCategory
            (
                Guid id
            )
        {
            return GetCategory
            (
                category => category.Id == id
            );
        }

        /// <summary>
        /// Gets a single report category from the repository
        /// </summary>
        /// <param name="name">The category name</param>
        /// <returns>The matching category</returns>
        public ReportCategory GetCategory
            (
                string name
            )
        {
            Validate.IsNotEmpty(name);

            return GetCategory
            (
                category => category.Name.Equals(name, StringComparison.OrdinalIgnoreCase)
            );
        }

        /// <summary>
        /// Gets a report category using the predicate specified
        /// </summary>
        /// <param name="predicate">The predicate</param>
        /// <returns>The matching category</returns>
        private ReportCategory GetCategory
            (
                Func<ReportCategory, bool> predicate
            )
        {
            var set = _context.Set<ReportCategory>();

            var category = set.FirstOrDefault
            (
                predicate
            );

            if (category == null)
            {
                throw new KeyNotFoundException
                (
                    "No category was found matching the key specified."
                );
            }

            return category;
        }

        /// <summary>
        /// Gets all report categories in the repository
        /// </summary>
        /// <returns>A collection of categories</returns>
        public IEnumerable<ReportCategory> GetAllCategories()
        {
            var categories = _context.Set<ReportCategory>();

            return categories.OrderBy
            (
                a => a.Name
            );
        }

        /// <summary>
        /// Gets sub report categories from a parent category
        /// </summary>
        /// <param name="parentCategoryId">The parent category ID</param>
        /// <returns>A collection of report categories</returns>
        public IEnumerable<ReportCategory> GetSubCategories
            (
                Guid parentCategoryId
            )
        {
            var set = _context.Set<ReportCategory>();

            var categories = set.Where
            (
                c => c.ParentCategoryId == parentCategoryId
            );

            return categories.OrderBy
            (
                a => a.Name
            );
        }

        /// <summary>
        /// Gets sub report categories from a parent category
        /// </summary>
        /// <param name="parentCategoryName">The parent category name</param>
        /// <returns>A collection of report categories</returns>
        public IEnumerable<ReportCategory> GetSubCategories
            (
                string parentCategoryName
            )
        {
            Validate.IsNotEmpty(parentCategoryName);

            var set = _context.Set<ReportCategory>();

            var categories = set.Where
            (
                c => c.ParentCategory.Name.Equals(parentCategoryName, StringComparison.OrdinalIgnoreCase)
            );

            return categories.OrderBy
            (
                a => a.Name
            );
        }

        /// <summary>
        /// Gets a collection of category assignments for a report
        /// </summary>
        /// <param name="reportName">The report name</param>
        /// <returns>A collection of report category assignments</returns>
        public IEnumerable<ReportCategoryAssignment> GetCategoryAssignments
            (
                string reportName
            )
        {
            Validate.IsNotEmpty(reportName);

            var set = _context.Set<ReportCategoryAssignment>();

            var assignments = set.Where
            (
                a => a.ReportName.Equals
                (
                    reportName,
                    StringComparison.OrdinalIgnoreCase
                )
            );

            return assignments.OrderBy
            (
                a => a.Category.Name
            );
        }

        /// <summary>
        /// Updates a single report category to the repository
        /// </summary>
        /// <param name="category">The category to update</param>
        public void UpdateCategory
            (
                ReportCategory category
            )
        {
            Validate.IsNotNull(category);

            var entry = _context.Entry<ReportCategory>
            (
                category
            );

            entry.State = EntityState.Modified;
        }

        /// <summary>
        /// Removes a single report category from the repository
        /// </summary>
        /// <param name="id">The ID of the category to remove</param>
        public void RemoveCategory
            (
                Guid id
            )
        {
            var category = GetCategory(id);

            RemoveCategory(category);
        }

        /// <summary>
        /// Removes a single report category from the repository
        /// </summary>
        /// <param name="name">The category name</param>
        public void RemoveCategory
            (
                string name
            )
        {
            var category = GetCategory(name);

            RemoveCategory(category);
        }

        /// <summary>
        /// Removes a single report category from the repository
        /// </summary>
        /// <param name="category">The category to remove</param>
        public void RemoveCategory
            (
                ReportCategory category
            )
        {
            var entry = _context.Entry<ReportCategory>
            (
                category
            );

            // Ensure the entity has been attached to the object state manager
            if (entry.State == EntityState.Detached)
            {
                _context.Set<ReportCategory>().Attach
                (
                    category
                );
            }

            _context.Set<ReportCategory>().Remove
            (
                category
            );
        }
    }
}
