namespace Reportr.Globalization
{
    using System;
    
    /// <summary>
    /// Represents a factory that resolves cultural transformers
    /// </summary>
    public static class CulturalTransformerFactory
    {
        /// <summary>
        /// Gets a new instance of the cultural transformer required
        /// </summary>
        /// <param name="value">The value to be transformed</param>
        /// <returns>The cultural transformer</returns>
        public static ICulturalTransformer GetInstance
            (
                object value
            )
        {
            if (value == null)
            {
                return new DefaultCulturalTransformer();
            }
            else
            {
                var valueType = value.GetType();

                if (valueType == typeof(DateTime) || valueType == typeof(DateTime?))
                {
                    return new DateTimeCulturalTransformer();
                }
                else
                {
                    return new DefaultCulturalTransformer();
                }
            }
        }
    }
}
