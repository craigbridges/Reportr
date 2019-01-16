namespace Reportr
{
    using Reportr.Data;
    using System;
    using System.Collections;
    using System.Linq;

    /// <summary>
    /// Useful extension methods for handling types at runtime
    /// </summary>
    public static class TypeExtensions
    {
        /// <summary>
        /// Determines if the type specified is a nullable type
        /// </summary>
        /// <param name="type">The type to check</param>
        /// <returns>True, if the type is nullable; otherwise false</returns>
        public static bool IsNullable
            (
                this Type type
            )
        {
            return
            (
                false == type.IsValueType ||
                (
                    Nullable.GetUnderlyingType(type) != null
                )
            );
        }

        /// <summary>
        /// Determines if the type is a nullable enum
        /// </summary>
        /// <param name="type">The type</param>
        /// <returns>True, if the type is a nullable enum; otherwise false</returns>
        public static bool IsNullableEnum
            (
                this Type type
            )
        {
            var underlying = Nullable.GetUnderlyingType
            (
                type
            );

            return
            (
                (underlying != null) && underlying.IsEnum
            );
        }

        /// <summary>
        /// Determines if the type can have an enum value assigned to it
        /// </summary>
        /// <param name="type">The type</param>
        /// <returns>True, if the type can have an enum value; otherwise false</returns>
        public static bool IsEnumAssignable
            (
                this Type type
            )
        {
            if (type.IsEnum)
            {
                return true;
            }
            else
            {
                return type.IsNullableEnum();
            }
        }

        /// <summary>
        /// Determines if the type specified is an enumerable type
        /// </summary>
        /// <param name="type">The type to check</param>
        /// <param name="acceptStrings">If true, string types will resolve to true</param>
        /// <returns>True, if the type is enumerable; otherwise false</returns>
        /// <remarks>
        /// All enumerable types are allowed, except for string
        /// </remarks>
        public static bool IsEnumerable
            (
                this Type type,
                bool acceptStrings = true
            )
        {
            if (false == acceptStrings && type == typeof(string))
            {
                return false;
            }
            else
            {
                return type.GetInterfaces().Contains
                (
                    typeof(IEnumerable)
                );
            }
        }
        
        /// <summary>
        /// Create generic type object instance dynamically using reflection
        /// </summary>
        /// <param name="generic">The type to create</param>
        /// <param name="innerType">The inner type</param>
        /// <param name="args">An array of arguments to pass into the object instance constructor</param>
        /// <returns>The object created</returns>
        /// <remarks>
        /// Example: to create a generic List of string, use the following code:
        /// CreateGeneric(typeof(List<>), typeof(string));
        /// 
        /// See http://geekswithblogs.net/marcel/archive/2007/03/24/109722.aspx
        /// </remarks>
        public static object CreateGeneric
            (
                this Type generic,
                Type innerType,
                params object[] args
            )
        {
            var specificType = generic.MakeGenericType
            (
                new Type[] { innerType }
            );

            return Activator.CreateInstance
            (
                specificType,
                args
            );
        }

        /// <summary>
        /// Determines if a type has overridden a method of a derived class
        /// </summary>
        /// <param name="type">The type</param>
        /// <param name="methodName">The method name</param>
        /// <returns>True, if the method has been overridden</returns>
        public static bool IsMethodOverridden
            (
                this Type type,
                string methodName
            )
        {
            var method = type.GetMethod(methodName);

            return method.DeclaringType == type;
        }

        /// <summary>
        /// Determines if the type as a default constructor
        /// </summary>
        /// <param name="type">The type</param>
        /// <returns>True, if a default constructor was found; otherwise false</returns>
        public static bool HasDefaultConstructor
            (
                this Type type
            )
        {
            return
            (
                type.IsValueType
                    || type.GetConstructor(Type.EmptyTypes) != null
            );
        }

        /// <summary>
        /// Gets the data value formatting type from the type
        /// </summary>
        /// <param name="type">The type</param>
        /// <param name="value">The value to be formatted</param>
        /// <returns>The formatting type</returns>
        public static DataValueFormattingType GetFormattingType
            (
                this Type type,
                object value
            )
        {
            if (value == null)
            {
                return DataValueFormattingType.None;
            }
            else
            {
                if (type.IsAssignableFrom(typeof(bool)))
                {
                    return DataValueFormattingType.Boolean;
                }
                else if (type.IsAssignableFrom(typeof(short))
                    || type.IsAssignableFrom(typeof(int))
                    || type.IsAssignableFrom(typeof(long)))
                {
                    return DataValueFormattingType.WholeNumber;
                }
                else if (type.IsAssignableFrom(typeof(float))
                    || type.IsAssignableFrom(typeof(double))
                    || type.IsAssignableFrom(typeof(decimal)))
                {
                    return DataValueFormattingType.DecimalNumber;
                }
                else if (type.IsAssignableFrom(typeof(DateTime)))
                {
                    if (((DateTime)value).TimeOfDay.TotalMilliseconds > 0)
                    {
                        return DataValueFormattingType.DateAndTime;
                    }
                    else
                    {
                        return DataValueFormattingType.Date;
                    }
                }
                else if (type.IsAssignableFrom(typeof(TimeSpan)))
                {
                    return DataValueFormattingType.Time;
                }
                else if (type.IsEnumAssignable())
                {
                    return DataValueFormattingType.Enum;
                }
                else
                {
                    return DataValueFormattingType.None;
                }
            }
        }
    }
}
