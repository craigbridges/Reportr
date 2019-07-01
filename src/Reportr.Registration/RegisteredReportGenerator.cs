namespace Reportr.Registration
{
    using Nito.AsyncEx.Synchronous;
    using Reportr.Data.Querying;
    using Reportr.Filtering;
    using Reportr.Registration.Authorization;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Threading.Tasks;

    /// <summary>
    /// Represents the default registered report generator implementation
    /// </summary>
    public sealed class RegisteredReportGenerator : IRegisteredReportGenerator
    {
        private readonly IRegisteredReportRepository _reportRepository;
        private readonly IReportRoleAssignmentRepository _assignmentRepository;
        private readonly IRegisteredReportDefinitionBuilder _definitionBuilder;
        private readonly IQueryRepository _queryRepository;
        private readonly IReportGenerator _reportGenerator;

        private readonly Dictionary<string, List<ReportRoleAssignment>> _assignmentCache;

        /// <summary>
        /// Constructs the report generator with required dependencies
        /// </summary>
        /// <param name="reportRepository">The report repository</param>
        /// <param name="assignmentRepository">The assignment repository</param>
        /// <param name="definitionBuilder">The report definition builder</param>
        /// <param name="queryRepository">The query repository</param>
        /// <param name="reportGenerator">The report generator</param>
        public RegisteredReportGenerator
            (
                IRegisteredReportRepository reportRepository,
                IReportRoleAssignmentRepository assignmentRepository,
                IRegisteredReportDefinitionBuilder definitionBuilder,
                IQueryRepository queryRepository,
                IReportGenerator reportGenerator
            )
        {
            Validate.IsNotNull(reportRepository);
            Validate.IsNotNull(assignmentRepository);
            Validate.IsNotNull(definitionBuilder);
            Validate.IsNotNull(queryRepository);
            Validate.IsNotNull(reportGenerator);

            _reportRepository = reportRepository;
            _assignmentRepository = assignmentRepository;
            _definitionBuilder = definitionBuilder;
            _queryRepository = queryRepository;
            _reportGenerator = reportGenerator;

            _assignmentCache = new Dictionary<string, List<ReportRoleAssignment>>();
        }

        /// <summary>
        /// Generates a report from a registered report and filter values
        /// </summary>
        /// <param name="reportName">The report name</param>
        /// <param name="filterValues">The filter values</param>
        /// <param name="userInfo">The user information</param>
        /// <returns>The generated result</returns>
        public ReportGenerationResult Generate
            (
                string reportName,
                SubmittedReportFilterValues filterValues,
                ReportUserInfo userInfo
            )
        {
            var task = GenerateAsync
            (
                reportName,
                filterValues,
                userInfo
            );

            return task.WaitAndUnwrapException();
        }

        /// <summary>
        /// Asynchronously generates a report from a registered report and filter values
        /// </summary>
        /// <param name="reportName">The report name</param>
        /// <param name="filterValues">The filter values</param>
        /// <param name="userInfo">The user information</param>
        /// <returns>The generation result</returns>
        public async Task<ReportGenerationResult> GenerateAsync
            (
                string reportName,
                SubmittedReportFilterValues filterValues,
                ReportUserInfo userInfo
            )
        {
            Validate.IsNotEmpty(reportName);
            Validate.IsNotNull(filterValues);
            Validate.IsNotNull(userInfo);

            var watch = Stopwatch.StartNew();

            var hasAccess = CanUserAccessReport
            (
                reportName,
                userInfo
            );

            if (false == hasAccess)
            {
                watch.Stop();
                
                return new ReportGenerationResult
                (
                    null,
                    watch.ElapsedMilliseconds,
                    "The user is not authorized to generate the report."
                );
            }

            var registeredReport = _reportRepository.GetReport
            (
                reportName
            );

            if (registeredReport.Disabled)
            {
                watch.Stop();

                return new ReportGenerationResult
                (
                    null,
                    watch.ElapsedMilliseconds,
                    "The report cannot be generated because it is disabled."
                );
            }

            var reportDefinition = _definitionBuilder.Build
            (
                registeredReport,
                _queryRepository
            );

            var reportFilter = BuildReportFilter
            (
                registeredReport,
                reportDefinition,
                filterValues,
                userInfo
            );

            var task = _reportGenerator.GenerateAsync
            (
                reportDefinition,
                reportFilter
            );

            var result = await task.ConfigureAwait
            (
                false
            );

            return result;
        }

        /// <summary>
        /// Gets a list of role assignments for the report specified
        /// </summary>
        /// <param name="reportName">The report name</param>
        /// <returns>A list of report role assignments</returns>
        private List<ReportRoleAssignment> GetAssignmentsForReport
            (
                string reportName
            )
        {
            if (_assignmentCache.ContainsKey(reportName))
            {
                return _assignmentCache[reportName];
            }
            else
            {
                var assignments = _assignmentRepository.GetAssignmentsForReport
                (
                    reportName
                );

                var assignmentList = assignments.ToList();

                _assignmentCache.Add
                (
                    reportName,
                    assignmentList
                );

                return assignmentList;
            }
        }

        /// <summary>
        /// Determines if a user can access the report specified
        /// </summary>
        /// <param name="reportName">The report name</param>
        /// <param name="userInfo">The user information</param>
        /// <returns>True, if the user can access the report; otherwise false</returns>
        private bool CanUserAccessReport
            (
                string reportName,
                ReportUserInfo userInfo
            )
        {
            var assignments = GetAssignmentsForReport
            (
                reportName
            );

            if (assignments.Count == 0)
            {
                return true;
            }
            else
            {
                if (userInfo.Roles == null || userInfo.Roles.Count() == 0)
                {
                    return false;
                }
                else
                {
                    return assignments.Any
                    (
                        a => userInfo.Roles.Any
                        (
                            role => role.ToLower() == a.RoleName.ToLower()
                        )
                    );
                }
            }
        }

        /// <summary>
        /// Builds a report filter from the filter values specified
        /// </summary>
        /// <param name="registeredReport">The registered report</param>
        /// <param name="reportDefinition">The report definition</param>
        /// <param name="filterValues">The filter values</param>
        /// <param name="userInfo">The user information</param>
        /// <returns>The report filter</returns>
        private ReportFilter BuildReportFilter
            (
                RegisteredReport registeredReport,
                ReportDefinition reportDefinition,
                SubmittedReportFilterValues filterValues,
                ReportUserInfo userInfo
            )
        {
            var submittedParameterValues = filterValues.ParameterValues;
            var convertedParameterValues = new Dictionary<string, object>();

            var filter = new ReportFilter
            (
                reportDefinition.Parameters.ToArray()
            );

            // Process the parameter values submitted and convert them to their expected types
            if (submittedParameterValues != null && submittedParameterValues.Any())
            {
                var groupedValues = new Dictionary<string, List<object>>();

                foreach (var submittedValue in submittedParameterValues)
                {
                    var parameterName = submittedValue.ParameterName;

                    if (String.IsNullOrEmpty(parameterName))
                    {
                        throw new InvalidOperationException
                        (
                            "The submitted parameter name cannot be null."
                        );
                    }
                    
                    var convertedValue = ConvertParameterValue
                    (
                        filter,
                        submittedValue
                    );

                    if (groupedValues.ContainsKey(parameterName))
                    {
                        groupedValues[parameterName].Add
                        (
                            convertedValue
                        );
                    }
                    else
                    {
                        groupedValues.Add
                        (
                            parameterName,
                            new List<object> { convertedValue }
                        );
                    }
                }

                // Flatten the grouped values into individual parameter values
                foreach (var pair in groupedValues)
                {
                    if (pair.Value.Count > 1)
                    {
                        convertedParameterValues.Add
                        (
                            pair.Key,
                            pair.Value.ToArray()
                        );
                    }
                    else
                    {
                        convertedParameterValues.Add
                        (
                            pair.Key,
                            pair.Value.First()
                        );
                    }
                }
            }

            // Add any missing parameter values by using the definition defaults
            foreach (var definition in filter.ParameterDefinitions)
            {
                var parameter = definition.Parameter;

                var matchFound = convertedParameterValues.Any
                (
                    pair => pair.Key.Equals
                    (
                        parameter.Name,
                        StringComparison.InvariantCultureIgnoreCase
                    )
                );

                if (false == matchFound)
                {
                    convertedParameterValues.Add
                    (
                        parameter.Name,
                        parameter.DefaultValue
                    );
                }
            }

            var constrainedParameters = CompileParameterConstraints
            (
                registeredReport,
                userInfo
            );

            filter.SetParameterValues
            (
                convertedParameterValues,
                constrainedParameters
            );

            if (filterValues.SortingRules != null)
            {
                foreach (var submittedRule in filterValues.SortingRules)
                {
                    filter.SetSortingRule
                    (
                        submittedRule.SectionType,
                        submittedRule.ComponentName,
                        submittedRule.ColumnName,
                        submittedRule.Direction
                    );
                }
            }

            return filter;
        }

        /// <summary>
        /// Compiles filter parameter constraints for the user specified
        /// </summary>
        /// <param name="registeredReport">The registered report</param>
        /// <param name="userInfo">The user information</param>
        /// <param name="parameterValues">The existing parameter values</param>
        /// <param name="constrainedParameters">The constrained parameters</param>
        /// <returns>
        /// The parameter constraints (parameter name and constrain value)
        /// </returns>
        /// <remarks>
        /// The parameter constraints defined by role assignments force certain
        /// report filter parameter values to be used over those submitted.
        /// 
        /// In this method, we ensure any constraints are adhered to.
        /// </remarks>
        private Dictionary<string, object> CompileParameterConstraints
            (
                RegisteredReport registeredReport,
                ReportUserInfo userInfo
            )
        {
            var constrainedParameters = new Dictionary<string, object>();

            // NOTE:
            // We don't apply constraints when a user has no roles.

            if (userInfo.Roles.Any())
            {
                var assignments = GetAssignmentsForReport
                (
                    registeredReport.Name
                );

                assignments = assignments.Where
                (
                    a => userInfo.Roles.Any
                    (
                        role => role.ToLower() == a.RoleName.ToLower()
                    )
                )
                .ToList();
                
                if (assignments.Any())
                {
                    var constraints = assignments.SelectMany
                    (
                        assignment => assignment.ParameterConstraints
                    );

                    foreach (var constraint in constraints.ToList())
                    {
                        var applyConstraint = assignments.All
                        (
                            a => a.ParameterConstraints.Any
                            (
                                c => c.ParameterName.ToLower() == constraint.ParameterName.ToLower()
                            )
                        );

                        // Only apply the constraint if all role assignments have it
                        if (applyConstraint)
                        {
                            var parameterName = constraint.ParameterName;

                            var value = constraint.ResolveValue
                            (
                                userInfo
                            );

                            constrainedParameters[parameterName] = value;
                        }
                    }
                }
            }

            return constrainedParameters;
        }

        /// <summary>
        /// Converts a submitted parameter value into the value type expected
        /// </summary>
        /// <param name="filter">The report filter</param>
        /// <param name="submittedValue">The submitted value</param>
        /// <returns>The converted value</returns>
        private object ConvertParameterValue
            (
                ReportFilter filter,
                SubmittedParameterValue submittedValue
            )
        {
            if (submittedValue.Values == null)
            {
                return null;
            }

            var definition = filter.GetDefinition
            (
                submittedValue.ParameterName
            );

            var expectedType = definition.Parameter.ExpectedType;

            if (expectedType.IsArray)
            {
                expectedType = expectedType.GetElementType();

                var convertedValues = Array.CreateInstance
                (
                    expectedType,
                    submittedValue.Values.Length
                );

                var index = 0;

                foreach (var rawValue in submittedValue.Values)
                {
                    convertedValues.SetValue
                    (
                        ConvertValue(rawValue),
                        index
                    );

                    index++;
                }

                return convertedValues;
            }
            else if (submittedValue.Values.Length == 0)
            {
                return null;
            }
            else
            {
                return ConvertValue
                (
                    submittedValue.Values[0]
                );
            }

            object ConvertValue(string value)
            {
                if (expectedType == typeof(string))
                {
                    return value;
                }
                else
                {
                    return ObjectConverter.Convert
                    (
                        value,
                        expectedType
                    );
                }
            }
        }
    }
}
