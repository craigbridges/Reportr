namespace Reportr.Registration
{
    using Reportr.Globalization;
    using Reportr.Registration.Authorization;
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
        private readonly IReportRoleAssignmentRepository _roleAssignmentRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly PhraseTranslationDictionary _translator;

        /// <summary>
        /// Constructs the report categorizer with required dependencies
        /// </summary>
        /// <param name="reportRepository">The report repository</param>
        /// <param name="categoryRepository">The category repository</param>
        /// <param name="roleAssignmentRepository">The role assignment repository</param>
        /// <param name="translatorFactory">The translator factory</param>
        /// <param name="unitOfWork">The unit of work</param>
        public ReportRegistrar
            (
                IRegisteredReportRepository reportRepository,
                IReportCategoryRepository categoryRepository,
                IReportRoleAssignmentRepository roleAssignmentRepository,
                IPhraseTranslatorFactory translatorFactory,
                IUnitOfWork unitOfWork
            )
        {
            Validate.IsNotNull(reportRepository);
            Validate.IsNotNull(categoryRepository);
            Validate.IsNotNull(roleAssignmentRepository);
            Validate.IsNotNull(translatorFactory);
            Validate.IsNotNull(unitOfWork);

            _reportRepository = reportRepository;
            _categoryRepository = categoryRepository;
            _roleAssignmentRepository = roleAssignmentRepository;
            _translator = translatorFactory.GetDictionary();
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Registers a single report with a builder source
        /// </summary>
        /// <typeparam name="TBuilder">The builder type</typeparam>
        /// <param name="configuration">The report configuration</param>
        public void RegisterReport<TBuilder>
            (
                RegisteredReportConfiguration configuration
            )

            where TBuilder : IReportDefinitionBuilder
        {
            RegisterReport
            (
                configuration,
                typeof(TBuilder)
            );
        }

        /// <summary>
        /// Registers a single report with a builder source
        /// </summary>
        /// <param name="configuration">The report configuration</param>
        /// <param name="builderType">The report builder type</param>
        public void RegisterReport
            (
                RegisteredReportConfiguration configuration,
                Type builderType
            )
        {
            Validate.IsNotNull(configuration);

            var name = configuration.Name;
            var registered = IsRegistered(name);

            if (registered)
            {
                throw new InvalidOperationException
                (
                    $"A report named '{name}' has already been registered."
                );
            }

            var report = new RegisteredReport
            (
                configuration,
                builderType
            );

            _reportRepository.AddReport(report);
            _unitOfWork.SaveChanges();
        }

        /// <summary>
        /// Registers a single report with a script source
        /// </summary>
        /// <param name="configuration">The report configuration</param>
        /// <param name="scriptSourceCode">The script source code</param>
        public void RegisterReport
            (
                RegisteredReportConfiguration configuration,
                string scriptSourceCode
            )
        {
            Validate.IsNotNull(configuration);

            var name = configuration.Name;
            var registered = IsRegistered(name);

            if (registered)
            {
                throw new InvalidOperationException
                (
                    $"A report named '{name}' has already been registered."
                );
            }

            var report = new RegisteredReport
            (
                configuration,
                scriptSourceCode
            );

            _reportRepository.AddReport(report);
            _unitOfWork.SaveChanges();
        }

        /// <summary>
        /// Auto registers multiple reports
        /// </summary>
        /// <param name="configurations">The report configurations</param>
        public void AutoRegisterReports
            (
                params AutoRegisteredReportConfiguration[] configurations
            )
        {
            Validate.IsNotNull(configurations);

            var changesMade = false;

            // Add any new reports that have not been registered yet
            foreach (var configuration in configurations)
            {
                var alreadyRegistered = _reportRepository.IsRegistered
                (
                    configuration.Name
                );

                if (false == alreadyRegistered)
                {
                    var report = new RegisteredReport
                    (
                        configuration,
                        configuration.BuilderType
                    );

                    _reportRepository.AddReport
                    (
                        report
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
        /// Configures a registered report
        /// </summary>
        /// <param name="reportName">The report name</param>
        /// <param name="configuration">The report configuration</param>
        public void ConfigureReport
            (
                string reportName,
                RegisteredReportConfiguration configuration
            )
        {
            var report = _reportRepository.GetReport
            (
                reportName
            );

            report.Configure(configuration);

            _reportRepository.UpdateReport(report);
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
        /// <param name="options">The globalization options (optional)</param>
        /// <returns>The matching registered report</returns>
        public RegisteredReport GetReport
            (
                string name,
                GlobalizationOptions options = null
            )
        {
            var report = _reportRepository.GetReport
            (
                name
            );

            report.Localize
            (
                _translator,
                options
            );

            return report;
        }

        /// <summary>
        /// Gets all registered reports
        /// </summary>
        /// <param name="options">The globalization options (optional)</param>
        /// <returns>A collection of registered reports</returns>
        public IEnumerable<RegisteredReport> GetAllReports
            (
                GlobalizationOptions options = null
            )
        {
            var reports = _reportRepository.GetAllReports();

            reports.Localize
            (
                _translator,
                options
            );

            return reports;
        }

        /// <summary>
        /// Gets all registered reports
        /// </summary>
        /// <param name="categoryName">The category name</param>
        /// <param name="options">The globalization options (optional)</param>
        /// <returns>A collection of registered reports</returns>
        public IEnumerable<RegisteredReport> GetReportsByCategory
            (
                string categoryName,
                GlobalizationOptions options = null
            )
        {
            var category = _categoryRepository.GetCategory
            (
                categoryName
            );

            var reportNames = category.AssignedReports.Select
            (
                a => a.ReportName
            )
            .ToList();

            var allReports = _reportRepository.GetAllReports();

            var matchingReports = allReports.Where
            (
                report => reportNames.Any
                (
                    name => name.ToLower() == report.Name.ToLower()
                )
            );

            matchingReports.Localize
            (
                _translator,
                options
            );

            return matchingReports;
        }

        /// <summary>
        /// Gets all registered reports for a single user
        /// </summary>
        /// <param name="userInfo">The user information</param>
        /// <param name="options">The globalization options (optional)</param>
        /// <returns>A collection of registered reports</returns>
        public IEnumerable<RegisteredReport> GetReportsForUser
            (
                ReportUserInfo userInfo,
                GlobalizationOptions options = null
            )
        {
            Validate.IsNotNull(userInfo);

            if (userInfo.Roles == null)
            {
                throw new ArgumentException
                (
                    "No user roles have been defined."
                );
            }

            var assignments = _roleAssignmentRepository.GetAssignmentsForRoles
            (
                userInfo.Roles
            );

            var reportNames = assignments.Select
            (
                a => a.ReportName
            );

            reportNames = reportNames.Distinct();

            var allReports = _reportRepository.GetAllReports();

            // Get reports that match the role assignments
            var matchingReports = allReports.Where
            (
                report => reportNames.Any
                (
                    name => report.Name.ToLower() == name.ToLower()
                )
            );

            // Get reports without roles and merge them with the matching reports
            var reportsWithoutRoles = GetReportsWithoutRoles();

            var userReports = new List<RegisteredReport>
            (
                matchingReports
            );
            
            userReports.AddRange(reportsWithoutRoles);

            userReports.Localize
            (
                _translator,
                options
            );

            return userReports.OrderBy
            (
                r => r.Name
            );
        }

        /// <summary>
        /// Gets all registered reports for a user and category
        /// </summary>
        /// <param name="userInfo">The user information</param>
        /// <param name="categoryName">The category string</param>
        /// <param name="options">The globalization options (optional)</param>
        /// <returns>A collection of registered reports</returns>
        public IEnumerable<RegisteredReport> GetReportsForUser
            (
                ReportUserInfo userInfo,
                string categoryName,
                GlobalizationOptions options = null
            )
        {
            Validate.IsNotNull(userInfo);
            Validate.IsNotEmpty(categoryName);
            
            var category = _categoryRepository.GetCategory
            (
                categoryName
            );

            var reportNames = category.AssignedReports.Select
            (
                a => a.ReportName
            )
            .ToList();

            var userReports = GetReportsForUser
            (
                userInfo
            );

            userReports = userReports.Where
            (
                report => reportNames.Any
                (
                    name => name.ToLower() == report.Name.ToLower()
                )
            );

            userReports.Localize
            (
                _translator,
                options
            );

            return userReports.OrderBy
            (
                r => r.Name
            );
        }

        /// <summary>
        /// Gets all registered reports that do not have any role assignments
        /// </summary>
        /// <returns>A collection of registered reports</returns>
        private IEnumerable<RegisteredReport> GetReportsWithoutRoles()
        {
            var allReports = _reportRepository.GetAllReports();
            var allAssignments = _roleAssignmentRepository.GetAllAssignments();

            var reportNames = allAssignments.Select
            (
                a => a.ReportName
            );

            reportNames = reportNames.Distinct();
            
            var filteredReports = allReports.Where
            (
                report => false == reportNames.Any
                (
                    name => report.Name.ToLower() == name.ToLower()
                )
            );
            
            return filteredReports.OrderBy
            (
                a => a.Name
            );
        }

        /// <summary>
        /// Specifies the report definition source as a builder
        /// </summary>
        /// <typeparam name="TBuilder">The builder type</typeparam>
        /// <param name="name">The name of the report</param>
        public void SpecifyBuilder<TBuilder>
            (
                string name
            )

            where TBuilder : IReportDefinitionBuilder
        {
            SpecifyBuilder
            (
                name,
                typeof(TBuilder)
            );
        }

        /// <summary>
        /// Specifies the report definition source as a builder
        /// </summary>
        /// <param name="name">The name of the report</param>
        /// <param name="builderType">The report builder type</param>
        public void SpecifyBuilder
            (
                string name,
                Type builderType
            )
        {
            var report = _reportRepository.GetReport
            (
                name
            );

            report.SpecifyBuilder
            (
                builderType
            );

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

            report.SpecifySource
            (
                scriptSourceCode
            );

            _reportRepository.UpdateReport(report);
            _unitOfWork.SaveChanges();
        }

        /// <summary>
        /// Disables a single registered report
        /// </summary>
        /// <param name="name">The name of the report</param>
        public void DisableReport
            (
                string name
            )
        {
            var report = _reportRepository.GetReport
            (
                name
            );

            report.Disable();

            _reportRepository.UpdateReport(report);
            _unitOfWork.SaveChanges();
        }

        /// <summary>
        /// Automatically disables multiple registered reports
        /// </summary>
        /// <param name="reportNames">The report names</param>
        public void AutoDisableReports
            (
                params string[] reportNames
            )
        {
            Validate.IsNotNull(reportNames);

            var changesMade = false;

            foreach (var name in reportNames)
            {
                var registeredReport = _reportRepository.GetReport
                (
                    name
                );
                
                if (false == registeredReport.Disabled)
                {
                    registeredReport.Disable();

                    _reportRepository.UpdateReport
                    (
                        registeredReport
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
        /// Enables a single registered report
        /// </summary>
        /// <param name="name">The name of the report</param>
        public void EnableReport
            (
                string name
            )
        {
            var report = _reportRepository.GetReport
            (
                name
            );

            report.Enable();

            _reportRepository.UpdateReport(report);
            _unitOfWork.SaveChanges();
        }

        /// <summary>
        /// Automatically enables multiple registered reports
        /// </summary>
        /// <param name="reportNames">The report names</param>
        public void AutoEnableReports
            (
                params string[] reportNames
            )
        {
            Validate.IsNotNull(reportNames);

            var changesMade = false;

            foreach (var name in reportNames)
            {
                var registeredReport = _reportRepository.GetReport
                (
                    name
                );

                if (registeredReport.Disabled)
                {
                    registeredReport.Enable();

                    _reportRepository.UpdateReport
                    (
                        registeredReport
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
                throw new InvalidOperationException
                (
                    $"A report named '{name}' has not been registered."
                );
            }
            
            _reportRepository.RemoveReport(name);
            _unitOfWork.SaveChanges();
        }
    }
}
