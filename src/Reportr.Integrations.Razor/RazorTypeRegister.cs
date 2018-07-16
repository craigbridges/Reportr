namespace Reportr.Integrations.Razor
{
    using Reportr.Integrations;
    using Reportr.Templating;
    using System.Collections.Generic;
    
    /// <summary>
    /// Represents a Razor type register implementation
    /// </summary>
    public sealed class RazorTypeRegister : ITypeRegister
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
                    typeof(ITemplateRenderer),
                    typeof(RazorTemplateRenderer)
                )
            };
        }
    }
}
