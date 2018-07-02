namespace Reportr.Registration.Entity.Repositories
{
    using Reportr.Registration.Categorization;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
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
        /// Gets a single report category from the repository
        /// </summary>
        /// <param name="id">The ID of the category to get</param>
        /// <returns>The matching category</returns>
        public ReportCategory GetCategory
            (
                Guid id
            )
        {
            var set = _context.Set<ReportCategory>();

            var category = set.FirstOrDefault
            (
                r => r.CategoryId == id
            );

            if (category == null)
            {
                var message = "No category was found matching the ID '{0}'.";

                throw new KeyNotFoundException
                (
                    String.Format
                    (
                        message,
                        id.ToString()
                    )
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
