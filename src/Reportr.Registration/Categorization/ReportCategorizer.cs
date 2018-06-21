namespace Reportr.Registration.Categorization
{
    using System;
    using System.Collections.Generic;
    
    /// <summary>
    /// Represents the default report categorizer implementation
    /// </summary>
    public sealed class ReportCategorizer : IReportCategorizer
    {
        private readonly IReportCategoryRepository _categoryRepository;
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// Constructs the report categorizer with required dependencies
        /// </summary>
        /// <param name="categoryRepository">The category repository</param>
        /// <param name="unitOfWork">The unit of work</param>
        public ReportCategorizer
            (
                IReportCategoryRepository categoryRepository,
                IUnitOfWork unitOfWork
            )
        {
            Validate.IsNotNull(categoryRepository);
            Validate.IsNotNull(unitOfWork);

            _categoryRepository = categoryRepository;
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Creates a single root level report category
        /// </summary>
        /// <param name="name">The category name</param>
        /// <param name="description">A description of the category</param>
        /// <returns>The category created</returns>
        public ReportCategory CreateCategory
            (
                string name,
                string description
            )
        {
            var category = new ReportCategory
            (
                name,
                description
            );

            _categoryRepository.AddCategory(category);
            _unitOfWork.SaveChanges();

            return category;
        }

        /// <summary>
        /// Creates a single report sub category
        /// </summary>
        /// <param name="parentCategoryId">The parent category ID</param>
        /// <param name="name">The category name</param>
        /// <param name="description">A description of the category</param>
        /// <returns>The category created</returns>
        public ReportCategory CreateSubCategory
            (
                Guid parentCategoryId,
                string name,
                string description
            )
        {
            var parentCategory = _categoryRepository.GetCategory
            (
                parentCategoryId
            );

            var subCategory = parentCategory.CreateSubCategory
            (
                name,
                description
            );

            _categoryRepository.AddCategory(subCategory);
            _unitOfWork.SaveChanges();

            return subCategory;
        }

        /// <summary>
        /// Gets all root level report categories
        /// </summary>
        /// <returns>A collection of report categories</returns>
        public IEnumerable<ReportCategory> GetAllCategories()
        {
            return _categoryRepository.GetAllCategories();
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
            return _categoryRepository.GetSubCategories
            (
                parentCategoryId
            );
        }

        /// <summary>
        /// Assigns a single report to a category
        /// </summary>
        /// <param name="categoryId">The category ID</param>
        /// <param name="reportName">The report name</param>
        public void AssignToCategory
            (
                Guid categoryId,
                string reportName
            )
        {
            var category = _categoryRepository.GetCategory
            (
                categoryId
            );

            category.AssignReport(reportName);

            _categoryRepository.UpdateCategory(category);
            _unitOfWork.SaveChanges();
        }

        /// <summary>
        /// Unassigns a single report from a category
        /// </summary>
        /// <param name="categoryId">The category ID</param>
        /// <param name="reportName">The report name</param>
        public void UnassignFromCategory
            (
                Guid categoryId,
                string reportName
            )
        {
            var category = _categoryRepository.GetCategory
            (
                categoryId
            );

            category.UnassignReport(reportName);

            _categoryRepository.UpdateCategory(category);
            _unitOfWork.SaveChanges();
        }

        /// <summary>
        /// Determines if a report has been assigned to a category
        /// </summary>
        /// <param name="categoryId">The category ID</param>
        /// <param name="reportName">The report name</param>
        /// <returns>True, if the report has been assigned; otherwise false</returns>
        public bool IsAssignedToCategory
            (
                Guid categoryId,
                string reportName
            )
        {
            var category = _categoryRepository.GetCategory
            (
                categoryId
            );

            return category.IsReportAssigned
            (
                reportName
            );
        }

        /// <summary>
        /// Deletes a single report category
        /// </summary>
        /// <param name="id">The category ID</param>
        public void DeleteCategory
            (
                Guid id
            )
        {
            _categoryRepository.RemoveCategory(id);
            _unitOfWork.SaveChanges();
        }
    }
}
