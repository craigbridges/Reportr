namespace Reportr.Culture
{
    using Reportr.IoC;
    using System.Collections.Generic;
    
    /// <summary>
    /// Represents the data culture type register implementation
    /// </summary>
    public sealed class CultureTypeRegister : ITypeRegister
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
                    typeof(IPhraseTranslationDictionaryFactory),
                    typeof(EmptyPhraseTranslationDictionaryFactory)
                )
            };
        }
    }
}
