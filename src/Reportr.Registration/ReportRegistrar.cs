namespace Reportr.Registration
{
    using Reportr.Registration.Categorization;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    
    /// <summary>
    /// Represents the default report registrar implementation
    /// </summary>
    public sealed class ReportRegistrar : IReportRegistrar
    {
        private readonly IRegisteredReportRepository _reportRepository;
        private readonly IReportCategoryRepository _categoryRepository;
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// Constructs the report categorizer with required dependencies
        /// </summary>
        /// <param name="reportRepository">The report repository</param>
        /// <param name="categoryRepository">The category repository</param>
        /// <param name="unitOfWork">The unit of work</param>
        public ReportRegistrar
            (
                IRegisteredReportRepository reportRepository,
                IReportCategoryRepository categoryRepository,
                IUnitOfWork unitOfWork
            )
        {
            Validate.IsNotNull(reportRepository);
            Validate.IsNotNull(categoryRepository);
            Validate.IsNotNull(unitOfWork);

            _reportRepository = reportRepository;
            _categoryRepository = categoryRepository;
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Registers a single report with a builder source
        /// </summary>
        /// <param name="name">The name</param>
        /// <param name="title">The title</param>
        /// <param name="description">The description</param>
        /// <param name="builder">The definition builder</param>
        public void RegisterReport
            (
                string name,
                string title,
                string description,
                IReportDefinitionBuilder builder
            )
        {
            var registered = IsRegistered(name);

            if (registered)
            {
                var message = "A report named '{0}' has already been registered.";

                throw new InvalidOperationException
                (
                    String.Format
                    (
                        message,
                        name
                    )
                );
            }

            var report = new RegisteredReport
            (
                name,
                title,
                description,
                builder
            );

            _reportRepository.AddReport(report);
            _unitOfWork.SaveChanges();
        }

        /// <summary>
        /// Registers a single report with a script source
        /// </summary>
        /// <param name="name">The name</param>
        /// <param name="title">The title</param>
        /// <param name="description">The description</param>
        /// <param name="scriptSourceCode">The script source code</param>
        public void RegisterReport
            (
                string name,
                string title,
                string description,
                string scriptSourceCode
            )
        {
            var registered = IsRegistered(name);

            if (registered)
            {
                var message = "A report named '{0}' has already been registered.";

                throw new InvalidOperationException
                (
                    String.Format
                    (
                        message,
                        name
                    )
                );
            }

            var report = new RegisteredReport
            (
                name,
                title,
                description,
                scriptSourceCode
            );

            _reportRepository.AddReport(report);
            _unitOfWork.SaveChanges();
        }

        /// <summary>
        /// Determines if a report has been registered
        /// </summary>
        /// <param name="name">The name of the report</param>
        /// <returns>True, if a match was found; otherwise false</returns>
        public bool IsRegistered
            (
                string name
            )
        {
            Validate.IsNotEmpty(name);

            return _reportRepository.IsRegistered
            (
                name
            );
        }

        /// <summary>
        /// Gets a single registered report
        /// </summary>
        /// <param name="name">The name of the report</param>
        /// <returns>The matching registered report</returns>
        public RegisteredReport GetReport
            (
                string name
            )
        {
            return _reportRepository.GetReport
            (
                name
            );
        }

        /// <summary>
        /// Gets all registered reports
        /// </summary>
        /// <returns>A collection of registered reports</returns>
        public IEnumerable<RegisteredReport> GetAllReports()
        {
            return _reportRepository.GetAllReports();
        }

        /// <summary>
        /// Gets all registered reports
        /// </summary>
        /// <param name="categoryId">The category ID</param>
        /// <returns>A collection of registered reports</returns>
        public IEnumerable<RegisteredReport> GetReportsByCategory
            (
                Guid categoryId
            )
        {
            var category = _categoryRepository.GetCategory
            (
                categoryId
            );

            var reportNames = category.AssignedReports.Select
            (
                a => a.ReportName
            )
            .ToList();

            var allReports = _reportRepository.GetAllReports();

            var matchingReports = allReports.Where
            (
                r => reportNames.Any
                (
                    name => name.ToLower() == r.Name.ToLower()
                )
            );

            return matchingReports;
        }

        /// <summary>
        /// Specifies the report definition source as a builder
        /// </summary>
        /// <param name="name">The name of the report</param>
        /// <param name="builder">The definition builder</param>
        public void SpecifySource
            (
                string name,
                IReportDefinitionBuilder builder
            )
        {
            var report = _reportRepository.GetReport
            (
                name
            );

            report.SpecifySource(builder);

            _reportRepository.UpdateReport(report);
            _unitOfWork.SaveChanges();
        }

        /// <summary>
        /// Specifies the report definition source as a script
        /// </summary>
        /// <param name="name">The name of the report</param>
        /// <param name="scriptSourceCode">The script source code</param>
        public void SpecifySource
            (
                string name,
                string scriptSourceCode
            )
        {
            var report = _reportRepository.GetReport
            (
                name
            );

            report.SpecifySource(scriptSourceCode);

            _reportRepository.UpdateReport(report);
            _unitOfWork.SaveChanges();
        }

        /// <summary>
        /// De-registers a single report
        /// </summary>
        /// <param name="name">The name of the report</param>
        public void DeregisterReport
            (
                string name
            )
        {
            var registered = IsRegistered(name);

            if (false == registered)
            {
                var message = "A report named '{0}' has not been registered.";

                throw new InvalidOperationException
                (
                    String.Format
                    (
                        message,
                        name
                    )
                );
            }
            
            _reportRepository.RemoveReport(name);
            _unitOfWork.SaveChanges();
        }
    }
}
