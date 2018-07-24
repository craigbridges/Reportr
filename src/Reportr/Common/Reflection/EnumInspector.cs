namespace Reportr.Common.Reflection
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Provides various methods for inspecting enums
    /// </summary>
    public class EnumInspector
    {
        /// <summary>
        /// Gets a collection of enum item info from the enum specified
        /// </summary>
        /// <param name="e">The enum</param>
        /// <returns>A collection of matching enum info</returns>
        public IEnumerable<EnumItemInfo> GetEnumInfo
            (
                Enum e
            )
        {
            Validate.IsNotNull(e);

            return GetEnumInfo
            (
                e.GetType()
            );
        }

        /// <summary>
        /// Gets a collection of enum item info from the enum type specified
        /// </summary>
        /// <param name="enumType">The enum type</param>
        /// <returns>A collection of matching enum info</returns>
        public IEnumerable<EnumItemInfo> GetEnumInfo
            (
                Type enumType
            )
        {
            Validate.IsNotNull(enumType);

            if (false == enumType.IsEnum)
            {
                var message = "The type {0} is not a valid enum.";

                throw new ArgumentException
                (
                    String.Format
                    (
                        message,
                        enumType.Name
                    )
                );
            }

            var enumInfo = new List<EnumItemInfo>();
            var enumValues = Enum.GetValues(enumType);

            foreach (var entry in enumValues)
            {
                var value = (int)entry;
                var name = entry.ToString();
                var description = entry.GetDescription();

                var info = new EnumItemInfo
                (
                    value,
                    name,
                    description
                );

                enumInfo.Add(info);
            }

            return enumInfo;
        }
    }
}
