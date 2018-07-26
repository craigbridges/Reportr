namespace Reportr
{
    using System;

    /// <summary>
    /// Converter implementation for converting an object to a type value
    /// </summary>
    /// <typeparam name="T">The type to convert to</typeparam>
    public class ObjectConverter<T>
    {
        /// <summary>
        /// Converts an object value to a value of the type specified, if a conversion is possible
        /// </summary>
        /// <param name="value">The object value to convert</param>
        /// <returns>The converted value</returns>
        public T Convert(object value)
        {
            if (value == null)
            {
                return default(T);
            }

            var valueType = value.GetType();

            if (typeof(T) == valueType || valueType.IsSubclassOf(typeof(T)))
            {
                return (T)value;
            }
            else
            {
                var canConvert = valueType.CanConvert
                (
                    typeof(T),
                    value
                );

                if (canConvert)
                {
                    var convertType = typeof(T);

                    if (convertType.IsGenericType 
                        && convertType.GetGenericTypeDefinition() == typeof(Nullable<>))
                    {
                        convertType = Nullable.GetUnderlyingType(convertType);
                    }

                    return (T)System.Convert.ChangeType
                    (
                        value,
                        convertType
                    );
                }
                else if (value.GetType() == typeof(string))
                {
                    return ConvertFromString((string)value);
                }
                else
                {
                    RaiseCannotConvertException(value);

                    return default(T);
                }
            }
        }

        /// <summary>
        /// Converts a string value to the type specified
        /// </summary>
        /// <param name="value">The string value to convert</param>
        /// <returns>The converted value</returns>
        private T ConvertFromString(string value)
        {
            var convertedValue = default(object);
            var convertType = typeof(T);
            var makeNullable = false;

            if (String.IsNullOrEmpty(value))
            {
                return default(T);
            }

            if (convertType.IsNullable())
            {
                convertType = Nullable.GetUnderlyingType(convertType);
                makeNullable = true;
            }

            if (convertType == typeof(DateTime))
            {
                if (makeNullable)
                {
                    convertedValue = (DateTime?)System.Convert.ToDateTime(value);
                }
                else
                {
                    convertedValue = System.Convert.ToDateTime(value);
                }
            }
            else if (convertType == typeof(bool))
            {
                if (makeNullable)
                {
                    convertedValue = (bool?)System.Convert.ToBoolean(value);
                }
                else
                {
                    convertedValue = System.Convert.ToBoolean(value);
                }
            }
            else if (convertType == typeof(double))
            {
                if (makeNullable)
                {
                    convertedValue = (double?)System.Convert.ToDouble(value);
                }
                else
                {
                    convertedValue = System.Convert.ToDouble(value);
                }
            }
            else if (convertType == typeof(Single))
            {
                if (makeNullable)
                {
                    convertedValue = (Single?)System.Convert.ToSingle(value);
                }
                else
                {
                    convertedValue = System.Convert.ToSingle(value);
                }
            }
            else if (convertType == typeof(decimal))
            {
                if (makeNullable)
                {
                    convertedValue = (decimal?)System.Convert.ToDecimal(value);
                }
                else
                {
                    convertedValue = System.Convert.ToDecimal(value);
                }
            }
            else if (convertType == typeof(long))
            {
                if (makeNullable)
                {
                    convertedValue = (long?)System.Convert.ToInt64(value);
                }
                else
                {
                    convertedValue = System.Convert.ToInt64(value);
                }
            }
            else if (convertType == typeof(int))
            {
                if (makeNullable)
                {
                    convertedValue = (int?)System.Convert.ToInt32(value);
                }
                else
                {
                    convertedValue = System.Convert.ToInt32(value);
                }
            }
            else if (convertType == typeof(short))
            {
                if (makeNullable)
                {
                    convertedValue = (short?)System.Convert.ToInt16(value);
                }
                else
                {
                    convertedValue = System.Convert.ToInt16(value);
                }
            }
            else if (convertType == typeof(char))
            {
                if (makeNullable)
                {
                    convertedValue = (char?)System.Convert.ToChar(value);
                }
                else
                {
                    convertedValue = System.Convert.ToChar(value);
                }
            }
            else if (convertType == typeof(byte))
            {
                if (makeNullable)
                {
                    convertedValue = (byte?)System.Convert.ToByte(value);
                }
                else
                {
                    convertedValue = System.Convert.ToByte(value);
                }
            }
            else if (convertType.IsEnum)
            {
                if (false == Enum.IsDefined(convertType, value))
                {
                    RaiseCannotConvertException(value);
                }
                else
                {
                    convertedValue = (T)(object)Enum.Parse
                    (
                        convertType,
                        value
                    );
                }
            }
            else
            {
                RaiseCannotConvertException(value);
            }

            return (T)convertedValue;
        }

        /// <summary>
        /// Raises an exception to indicate that the value could not be converted
        /// </summary>
        /// <param name="value">The value that could not be converted</param>
        private void RaiseCannotConvertException
            (
                object value
            )
        {
            var valueString = value.ToString();
            var typeName = typeof(T).ToString();

            var message = "The value '{0}' cannot be converted to the type '{1}'.";

            throw new InvalidCastException
            (
                String.Format
                (
                    message,
                    valueString,
                    typeName
                )
            );
        }
    }
}
