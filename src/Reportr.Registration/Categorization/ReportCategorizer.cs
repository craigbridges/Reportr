﻿namespace Reportr.Registration.Categorization
{
    using Reportr.Globalization;
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
        private readonly PhraseTranslationDictionary _translator;

        /// <summary>
        /// Constructs the report categorizer with required dependencies
        /// </summary>
        /// <param name="categoryRepository">The category repository</param>
        /// <param name="translatorFactory">The translator factory</param>
        /// <param name="unitOfWork">The unit of work</param>
        public ReportCategorizer
            (
                IReportCategoryRepository categoryRepository,
                IPhraseTranslatorFactory translatorFactory,
                IUnitOfWork unitOfWork
            )
        {
            Validate.IsNotNull(categoryRepository);
            Validate.IsNotNull(translatorFactory);
            Validate.IsNotNull(unitOfWork);

            _categoryRepository = categoryRepository;
            _translator = translatorFactory.GetDictionary();
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
            Validate.IsNotNull(configuration);

            var name = configuration.Name;

            var nameAvailable = _categoryRepository.IsNameAvailable
            (
                name
            );

            if (false == nameAvailable)
            {
                throw new InvalidOperationException
                (
                    $"The category name '{name}' is not available."
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
            Validate.IsNotEmpty(parentCategoryName);
            Validate.IsNotNull(configuration);

            var name = configuration.Name;

            var nameAvailable = _categoryRepository.IsNameAvailable
            (
                name
            );

            if (false == nameAvailable)
            {
                throw new InvalidOperationException
                (
                    $"The category name '{name}' is not available."
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

            foreach (var configuration in configurations)
            {
                var nameAvailable = _categoryRepository.IsNameAvailable
                (
                    configuration.Name
                );

                if (nameAvailable)
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
                    config => config.Name.Equals(report.Name, StringComparison.OrdinalIgnoreCase)
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
                var nameAvailable = _categoryRepository.IsNameAvailable
                (
                    configuration.Name
                );

                if (nameAvailable)
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
        /// <param name="options">The globalization options (optional)</param>
        /// <returns>The matching category</returns>
        public ReportCategory GetCategory
            (
                string name,
                GlobalizationOptions options = null
            )
        {
            var category = _categoryRepository.GetCategory
            (
                name
            );

            category.Localize
            (
                _translator,
                options
            );

            return category;
        }

        /// <summary>
        /// Gets all root level report categories
        /// </summary>
        /// <param name="options">The globalization options (optional)</param>
        /// <returns>A collection of report categories</returns>
        public IEnumerable<ReportCategory> GetAllCategories
            (
                GlobalizationOptions options = null
            )
        {
            var categories = _categoryRepository.GetAllCategories();

            categories.Localize
            (
                _translator,
                options
            );

            return categories;
        }

        /// <summary>
        /// Gets the root report categories in the repository
        /// </summary>
        /// <param name="options">The globalization options (optional)</param>
        /// <returns>A collection of categories</returns>
        public IEnumerable<ReportCategory> GetRootCategories
            (
                GlobalizationOptions options = null
            )
        {
            var categories = _categoryRepository.GetRootCategories();

            categories.Localize
            (
                _translator,
                options
            );

            return categories;
        }

        /// <summary>
        /// Gets sub report categories from a parent category
        /// </summary>
        /// <param name="parentCategoryName">The parent category name</param>
        /// <param name="options">The globalization options (optional)</param>
        /// <returns>A collection of report categories</returns>
        public IEnumerable<ReportCategory> GetSubCategories
            (
                string parentCategoryName,
                GlobalizationOptions options = null
            )
        {
            var categories = _categoryRepository.GetSubCategories
            (
                parentCategoryName
            );

            categories.Localize
            (
                _translator,
                options
            );

            return categories;
        }

        /// <summary>
        /// Configures a report category
        /// </summary>
        /// <param name="categoryName">The category name</param>
        /// <param name="configuration">The category configuration</param>
        public void ConfigureCategory
            (
                string categoryName,
                ReportCategoryConfiguration configuration
            )
        {
            var category = _categoryRepository.GetCategory
            (
                categoryName
            );

            category.Configure(configuration);

            _categoryRepository.UpdateCategory(category);
            _unitOfWork.SaveChanges();
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
        /// Gets a collection of category assignments for a report
        /// </summary>
        /// <param name="reportName">The report name</param>
        /// <returns>A collection of report category assignments</returns>
        public IEnumerable<ReportCategoryAssignment> GetCategoryAssignments
            (
                string reportName
            )
        {
            return _categoryRepository.GetCategoryAssignments
            (
                reportName
            );
        }
    }
}
