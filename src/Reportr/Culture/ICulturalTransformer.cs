namespace Reportr.Culture
{
    using CodeChange.Toolkit.Culture;
    using System;
    
    /// <summary>
    /// Defines a contract for a service that transforms an object to a culture
    /// </summary>
    public interface ICulturalTransformer
    {
        /// <summary>
        /// Transforms an object to the culture specific variant
        /// </summary>
        /// <param name="value">The value to transform</param>
        /// <param name="localeConfiguration">The locale configuration</param>
        /// <returns>The transformed value</returns>
        object Transform
        (
            object value,
            ILocaleConfiguration localeConfiguration
        );
    }
}
