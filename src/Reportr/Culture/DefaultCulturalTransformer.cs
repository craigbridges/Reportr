namespace Reportr.Culture
{
    using CodeChange.Toolkit.Culture;
    
    /// <summary>
    /// Represents the default cultural transformer
    /// </summary>
    public class DefaultCulturalTransformer : ICulturalTransformer
    {
        /// <summary>
        /// Transforms an object to the culture specific variant
        /// </summary>
        /// <param name="value">The value to transform</param>
        /// <param name="localeConfiguration">The locale configuration</param>
        /// <returns>The transformed value</returns>
        public object Transform
            (
                object value,
                ILocaleConfiguration localeConfiguration
            )
        {
            return value;
        }
    }
}
