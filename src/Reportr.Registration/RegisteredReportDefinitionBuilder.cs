namespace Reportr.Registration
{
    using Reportr.Data.Querying;
    using System;
    
    /// <summary>
    /// Represents the default registered report definition builder implementation
    /// </summary>
    public sealed class RegisteredReportDefinitionBuilder : IRegisteredReportDefinitionBuilder
    {
        private readonly IReportDefinitionBuilderRepository _builderRepository;

        /// <summary>
        /// Constructs the report definition builder with required dependencies
        /// </summary>
        /// <param name="builderRepository">The report builder repository</param>
        public RegisteredReportDefinitionBuilder
            (
                IReportDefinitionBuilderRepository builderRepository
            )
        {
            Validate.IsNotNull(builderRepository);

            _builderRepository = builderRepository;
        }

        /// <summary>
        /// Builds the report definition
        /// </summary>
        /// <param name="registeredReport">The registered report</param>
        /// <param name="queryRepository">The query repository</param>
        /// <returns>The report definition generated</returns>
        public ReportDefinition Build
            (
                RegisteredReport registeredReport,
                IQueryRepository queryRepository
            )
        {
            Validate.IsNotNull(registeredReport);
            Validate.IsNotNull(queryRepository);

            if (registeredReport.SourceType == ReportDefinitionSourceType.Builder)
            {
                var builder = _builderRepository.GetBuilder
                (
                    registeredReport.BuilderTypeAssemblyQualifiedName
                );

                return builder.Build
                (
                    queryRepository
                );
            }
            else
            {
                throw new NotSupportedException
                (
                    "Report definition script language has not been implemented."
                );
            }
        }
    }
}
