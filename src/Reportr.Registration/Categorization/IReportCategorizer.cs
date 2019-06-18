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
        /// <param name="configuration">The category configuration</param>
        /// <returns>The category created</returns>
        ReportCategory CreateCategory
        (
            ReportCategoryConfiguration configuration
        );

        /// <summary>
        /// Creates a single report sub category
        /// </summary>
        /// <param name="parentCategoryName">The parent category name</param>
        /// <param name="configuration">The category configuration</param>
        /// <returns>The category created</returns>
        ReportCategory CreateSubCategory
        (
            string parentCategoryName,
            ReportCategoryConfiguration configuration
        );

        /// <summary>
        /// Auto registers the report categories specified
        /// </summary>
        /// <param name="configurations">The category configurations</param>
        void AutoRegisterCategories
        (
            params ReportCategoryConfiguration[] configurations
        );

        /// <summary>
        /// Auto registers the report sub categories specified
        /// </summary>
        /// <param name="parentCategoryName">The parent category name</param>
        /// <param name="configurations">The category configurations</param>
        void AutoRegisterCategories
        (
            string parentCategoryName,
            params ReportCategoryConfiguration[] configurations
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
        /// Configures a report category
        /// </summary>
        /// <param name="categoryName">The category name</param>
        /// <param name="configuration">The category configuration</param>
        void ConfigureCategory
        (
            string categoryName,
            ReportCategoryConfiguration configuration
        );

        /// <summary>
        /// Deletes a single report category
        /// </summary>
        /// <param name="name">The category name</param>
        void DeleteCategory
        (
            string name
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
        /// Auto assigns multiple reports to categories
        /// </summary>
        /// <param name="assignments">The category-report assignments</param>
        /// <remarks>
        /// Each assignment key-value pair represents a category name 
        /// (the key) and report name (the value).
        /// </remarks>
        void AutoAssignReports
        (
            params KeyValuePair<string, string>[] assignments
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
        /// Gets a collection of category assignments for a report
        /// </summary>
        /// <param name="reportName">The report name</param>
        /// <returns>A collection of report category assignments</returns>
        IEnumerable<ReportCategoryAssignment> GetCategoryAssignments
        (
            string reportName
        );
    }
}
