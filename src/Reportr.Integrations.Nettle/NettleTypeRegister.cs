namespace Reportr.Integrations.Nettle
{
    using Reportr.Integrations;
    using Reportr.Templating;
    using System.Collections.Generic;
    
    /// <summary>
    /// Represents a Nettle type register implementation
    /// </summary>
    public sealed class NettleTypeRegister : ITypeRegister
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
                    typeof(NettleTemplateRenderer)
                )
            };
        }
    }
}
