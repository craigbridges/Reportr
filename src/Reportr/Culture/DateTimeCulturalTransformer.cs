namespace Reportr.Culture
{
    using CodeChange.Toolkit.Culture;
    using System;
    
    /// <summary>
    /// Represents a DateTime cultural transformer
    /// </summary>
    public class DateTimeCulturalTransformer : ICulturalTransformer
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
            Validate.IsNotNull(localeConfiguration);

            if (value == null)
            {
                return value;
            }

            var valueType = value.GetType();

            if (valueType != typeof(DateTime) && valueType != typeof(DateTime?))
            {
                throw new ArgumentException
                (
                    $"The type {valueType} is not supported by the transformer."
                );
            }

            var date = (DateTime?)value;

            return date.ToLocalTime
            (
                localeConfiguration
            );
        }
    }
}
