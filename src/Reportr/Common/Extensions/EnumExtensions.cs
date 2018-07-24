namespace System
{
    using System.ComponentModel;

    /// <summary>
    /// Useful extension methods for enum types
    /// </summary>
    public static class EnumExtensions
    {
        /// <summary>
        /// Gets the description of an enum value using the description attribute
        /// </summary>
        /// <param name="value">The enum value to get the description for</param>
        /// <returns>A string describing the enum</returns>
        /// <remarks>
        /// Example usage:
        /// 
        /// public enum MyEnum
        /// {
        ///     [Description("Description for Foo")]
        ///     Foo,
        ///     [Description("Description for Bar")]
        ///     Bar
        /// }
        ///
        /// MyEnum x = MyEnum.Foo;
        /// string description = x.GetDescription();
        /// </remarks>
        public static string GetDescription
            (
                this object value
            )
        {
            if (value == null)
            {
                return null;
            }

            var type = value.GetType();

            if (type.IsEnum)
            {
                var name = Enum.GetName(type, value);

                if (false == String.IsNullOrEmpty(name))
                {
                    var field = type.GetField(name);

                    if (field != null)
                    {
                        var attribute = Attribute.GetCustomAttribute
                        (
                            field,
                            typeof(DescriptionAttribute)
                        );

                        if (attribute == null)
                        {
                            return name.Spacify();
                        }
                        else
                        {
                            return ((DescriptionAttribute)attribute).Description;
                        }
                    }
                }

                return null;
            }
            else
            {
                return value.ToString();
            }
        }
    }
}
