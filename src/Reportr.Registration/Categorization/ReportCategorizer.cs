namespace Reportr.Registration.Categorization
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

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
        /// <param name="configuration">The category configuration</param>
        /// <returns>The category created</returns>
        public ReportCategory CreateCategory
            (
                ReportCategoryConfiguration configuration
            )
        {
            var nameAvailable = _categoryRepository.IsNameAvailable
            (
                configuration.Name
            );

            if (false == nameAvailable)
            {
                var message = "The category name '{0}' is not available.";

                throw new InvalidOperationException
                (
                    String.Format
                    (
                        message,
                        configuration.Name
                    )
                );
            }

            var category = new ReportCategory
            (
                configuration
            );

            _categoryRepository.AddCategory(category);
            _unitOfWork.SaveChanges();

            return category;
        }

        /// <summary>
        /// Creates a single report sub category
        /// </summary>
        /// <param name="parentCategoryName">The parent category name</param>
        /// <param name="configuration">The category configuration</param>
        /// <returns>The category created</returns>
        public ReportCategory CreateSubCategory
            (
                string parentCategoryName,
                ReportCategoryConfiguration configuration
            )
        {
            var nameAvailable = _categoryRepository.IsNameAvailable
            (
                configuration.Name
            );

            if (false == nameAvailable)
            {
                var message = "The category name '{0}' is not available.";

                throw new InvalidOperationException
                (
                    String.Format
                    (
                        message,
                        configuration.Name
                    )
                );
            }

            var parentCategory = _categoryRepository.GetCategory
            (
                parentCategoryName
            );

            var subCategory = parentCategory.CreateSubCategory
            (
                configuration
            );

            _categoryRepository.AddCategory(subCategory);
            _unitOfWork.SaveChanges();

            return subCategory;
        }

        /// <summary>
        /// Auto registers the report categories specified
        /// </summary>
        /// <param name="configurations">The category configurations</param>
        public void AutoRegisterCategories
            (
                params ReportCategoryConfiguration[] configurations
            )
        {
            Validate.IsNotNull(configurations);

            var changesMade = false;
            //var allCategories = _categoryRepository.GetAllCategories();

            //// Find all categories that don't match the configurations and remove
            //var unmatchedCategories = allCategories.Where
            //(
            //    report => false == configurations.Any
            //    (
            //        config => config.Name.ToLower() == report.Name.ToLower()
            //    )
            //);

            //foreach (var category in unmatchedCategories.ToList())
            //{
            //    _categoryRepository.RemoveCategory
            //    (
            //        category.Name
            //    );

            //    changesMade = true;
            //}

            // Add any new categories that have not been registered yet
            foreach (var configuration in configurations)
            {
                var categoryExists = _categoryRepository.IsNameAvailable
                (
                    configuration.Name
                );

                if (false == categoryExists)
                {
                    var category = new ReportCategory
                    (
                        configuration
                    );

                    _categoryRepository.AddCategory
                    (
                        category
                    );

                    changesMade = true;
                }
            }

            if (changesMade)
            {
                _unitOfWork.SaveChanges();
            }
        }

        /// <summary>
        /// Auto registers the report sub categories specified
        /// </summary>
        /// <param name="parentCategoryName">The parent category name</param>
        /// <param name="configurations">The category configurations</param>
        public void AutoRegisterCategories
            (
                string parentCategoryName,
                params ReportCategoryConfiguration[] configurations
            )
        {
            Validate.IsNotNull(configurations);

            var changesMade = false;
            var parentCategory = default(ReportCategory);
            
            var allCategories = _categoryRepository.GetSubCategories
            (
                parentCategoryName
            );

            // Find all categories that don't match the configurations and remove
            var unmatchedCategories = allCategories.Where
            (
                report => false == configurations.Any
                (
                    config => config.Name.ToLower() == report.Name.ToLower()
                )
            );

            foreach (var category in unmatchedCategories.ToList())
            {
                _categoryRepository.RemoveCategory
                (
                    category.Name
                );

                changesMade = true;
            }
            
            // Add any new categories that have not been registered yet
            foreach (var configuration in configurations)
            {
                var categoryExists = _categoryRepository.IsNameAvailable
                (
                    configuration.Name
                );

                if (false == categoryExists)
                {
                    if (parentCategory == null)
                    {
                        parentCategory = _categoryRepository.GetCategory
                        (
                            parentCategoryName
                        );
                    }

                    var subCategory = parentCategory.CreateSubCategory
                    (
                        configuration
                    );

                    _categoryRepository.AddCategory
                    (
                        subCategory
                    );

                    changesMade = true;
                }
            }

            if (changesMade)
            {
                _unitOfWork.SaveChanges();
            }
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
            return _categoryRepository.IsNameAvailable
            (
                categoryName
            );
        }

        /// <summary>
        /// Gets a single report category
        /// </summary>
        /// <param name="name">The category name</param>
        /// <returns>The matching category</returns>
        public ReportCategory GetCategory
            (
                string name
            )
        {
            return _categoryRepository.GetCategory
            (
                name
            );
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
        /// <param name="parentCategoryName">The parent category name</param>
        /// <returns>A collection of report categories</returns>
        public IEnumerable<ReportCategory> GetSubCategories
            (
                string parentCategoryName
            )
        {
            return _categoryRepository.GetSubCategories
            (
                parentCategoryName
            );
        }

        /// <summary>
        /// Assigns a single report to a category
        /// </summary>
        /// <param name="categoryName">The category name</param>
        /// <param name="reportName">The report name</param>
        public void AssignToCategory
            (
                string categoryName,
                string reportName
            )
        {
            var category = _categoryRepository.GetCategory
            (
                categoryName
            );

            category.AssignReport(reportName);

            _categoryRepository.UpdateCategory(category);
            _unitOfWork.SaveChanges();
        }

        /// <summary>
        /// Auto assigns multiple reports to categories
        /// </summary>
        /// <param name="assignments">The category-report assignments</param>
        /// <remarks>
        /// Each assignment key-value pair represents a category name 
        /// (the key) and report name (the value).
        /// </remarks>
        public void AutoAssignReports
            (
                params KeyValuePair<string, string>[] assignments
            )
        {
            Validate.IsNotNull(assignments);

            var changesMade = false;

            foreach (var pair in assignments)
            {
                var categoryName = pair.Key;
                var reportName = pair.Value;

                var category = _categoryRepository.GetCategory
                (
                    categoryName
                );

                var isAssigned = category.IsReportAssigned
                (
                    reportName
                );

                if (false == isAssigned)
                {
                    category.AssignReport(reportName);

                    _categoryRepository.UpdateCategory
                    (
                        category
                    );
                }

                changesMade = true;
            }

            if (changesMade)
            {
                _unitOfWork.SaveChanges();
            }
        }

        /// <summary>
        /// Unassigns a single report from a category
        /// </summary>
        /// <param name="categoryName">The category name</param>
        /// <param name="reportName">The report name</param>
        public void UnassignFromCategory
            (
                string categoryName,
                string reportName
            )
        {
            var category = _categoryRepository.GetCategory
            (
                categoryName
            );

            category.UnassignReport(reportName);

            _categoryRepository.UpdateCategory(category);
            _unitOfWork.SaveChanges();
        }

        /// <summary>
        /// Determines if a report has been assigned to a category
        /// </summary>
        /// <param name="categoryName">The category name</param>
        /// <param name="reportName">The report name</param>
        /// <returns>True, if the report has been assigned; otherwise false</returns>
        public bool IsAssignedToCategory
            (
                string categoryName,
                string reportName
            )
        {
            var category = _categoryRepository.GetCategory
            (
                categoryName
            );

            return category.IsReportAssigned
            (
                reportName
            );
        }

        /// <summary>
        /// Deletes a single report category
        /// </summary>
        /// <param name="name">The category name</param>
        public void DeleteCategory
            (
                string name
            )
        {
            _categoryRepository.RemoveCategory(name);
            _unitOfWork.SaveChanges();
        }
    }
}
