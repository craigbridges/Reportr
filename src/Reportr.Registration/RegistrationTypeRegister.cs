namespace Reportr.Registration
{
    using Reportr.IoC;
    using Reportr.Registration.Authorization;
    using Reportr.Registration.Categorization;
    using System.Collections.Generic;
    
    /// <summary>
    /// Represents a registration type register implementation
    /// </summary>
    public sealed class RegistrationTypeRegister : ITypeRegister
    {
        /// <summary>
        /// Gets a collection of all registered types
        /// </summary>
        /// <returns>A collection of registered types</returns>
        public IEnumerable<RegisteredType> GetRegisteredTypes()
        {
            return new List<RegisteredType>()
            {
                new RegisteredType
                (
                    typeof(IReportCategorizer),
                    typeof(ReportCategorizer)
                ),
                new RegisteredType
                (
                    typeof(IReportRoleManager),
                    typeof(ReportRoleManager)
                ),
                new RegisteredType
                (
                    typeof(IReportRegistrar),
                    typeof(ReportRegistrar)
                ),
                new RegisteredType
                (
                    typeof(IRegisteredReportDefinitionBuilder),
                    typeof(RegisteredReportDefinitionBuilder)
                ),
                new RegisteredType
                (
                    typeof(IRegisteredReportGenerator),
                    typeof(RegisteredReportGenerator)
                )
            };
        }
    }
}
