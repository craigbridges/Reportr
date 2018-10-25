namespace Reportr
{
    using System;

    /// <summary>
    /// Represents a utility for converting objects
    /// </summary>
    public static class ObjectConverter
    {
        /// <summary>
        /// Converts an object to the type specified
        /// </summary>
        /// <param name="value">The value to convert</param>
        /// <param name="toType">The type to convert to</param>
        /// <returns>The converted value</returns>
        public static object Convert
            (
                object value,
                Type toType
            )
        {
            var converterType = typeof(ObjectConverter<>);

            var converterArgs = new Type[]
            {
                toType
            };

            var genericType = converterType.MakeGenericType
            (
                converterArgs
            );

            var converterInstance = Activator.CreateInstance
            (
                genericType
            );

            var convertMethod = genericType.GetMethod
            (
                "Convert"
            );

            var convertedValue = convertMethod.Invoke
            (
                converterInstance,
                new object[]
                {
                    value
                }
            );

            return convertedValue;
        }
    }
}
