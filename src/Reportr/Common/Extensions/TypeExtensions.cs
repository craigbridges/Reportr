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
        /// Determines if the type can be converted to the type specified from the given type value
        /// </summary>
        /// <param name="fromType">The current type</param>
        /// <param name="toType">The new type</param>
        /// <param name="fromObject">The current type value</param>
        /// <returns>True, if the type can be converted; otherwise false</returns>
        public static bool CanConvert
            (
                this Type fromType,
                Type toType,
                object fromObject
            )
        {
            Validate.IsNotNull(fromType);
            Validate.IsNotNull(toType);

            if (fromObject == null)
            {
                return toType.IsNullable();
            }
            else
            {
                var fromObjectType = fromObject.GetType();

                if (fromObjectType == toType)
                {
                    return true;
                }
                else if (toType.IsAssignableFrom(fromObjectType) 
                    || fromObjectType.IsAssignableFrom(toType))
                {
                    return true;
                }
                else
                {
                    var implementsInterface = fromObjectType.ImplementsInterface
                    (
                        typeof(IConvertible)
                    );

                    if (false == implementsInterface)
                    {
                        return false;
                    }

                    var converterType = typeof(TypeConverterChecker<,>).MakeGenericType
                    (
                        fromType,
                        toType
                    );

                    var instance = Activator.CreateInstance
                    (
                        converterType,
                        fromObject
                    );

                    var canConvertProperty = converterType.GetProperty
                    (
                        "CanConvert"
                    );

                    return (bool)canConvertProperty.GetGetMethod().Invoke
                    (
                        instance,
                        null
                    );
                }
            }
        }

        /// <summary>
        /// Gets the default value for a type at runtime
        /// </summary>
        /// <param name="t">The type to get the default value for</param>
        /// <returns>The default value</returns>
        public static object GetDefaultValue
            (
                this Type t
            )
        {
            if (t.IsValueType && Nullable.GetUnderlyingType(t) == null)
            {
                return Activator.CreateInstance(t);
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Determines if the type has a property with the name specified
        /// </summary>
        /// <param name="t">The type to check</param>
        /// <param name="propertyName">The name of the property to look for</param>
        /// <returns>True, if the property exists; otherwise false</returns>
        public static bool HasProperty
            (
                this Type t,
                string propertyName
            )
        {
            return t.GetProperty(propertyName) != null;
        }

        /// <summary>
        /// Determines if a type is numeric. Nullable numeric types are considered numeric.
        /// </summary>
        /// <param name="type">The type to check</param>
        /// <param name="acceptNullables">If true nullable numeric types are also accepted</param>
        /// <returns>True, if the type is numeric; otherwise false</returns>
        public static bool IsNumeric
            (
                this Type type,
                bool acceptNullables = true
            )
        {
            if (type == null)
            {
                return false;
            }

            switch (Type.GetTypeCode(type))
            {
                case TypeCode.Byte:
                case TypeCode.Decimal:
                case TypeCode.Double:
                case TypeCode.Int16:
                case TypeCode.Int32:
                case TypeCode.Int64:
                case TypeCode.SByte:
                case TypeCode.Single:
                case TypeCode.UInt16:
                case TypeCode.UInt32:
                case TypeCode.UInt64:
                    return true;

                case TypeCode.Object:

                    if (acceptNullables && type.IsGenericType)
                    {
                        var isNullable =
                        (
                            type.GetGenericTypeDefinition() == typeof(Nullable<>)
                        );

                        if (isNullable)
                        {
                            return IsNumeric
                            (
                                Nullable.GetUnderlyingType(type)
                            );
                        }
                    }

                    return false;
            }

            return false;
        }

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
        /// Determines if the type implements a specific interface
        /// </summary>
        /// <param name="type">The type to check</param>
        /// <param name="interfaceType">The interface type</param>
        /// <returns>True, if the type implements the interface; otherwise false</returns>
        public static bool ImplementsInterface
            (
                this Type type,
                Type interfaceType
            )
        {
            return type.GetInterfaces().Contains
            (
                interfaceType
            );
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
