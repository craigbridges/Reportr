namespace System.Data.Entity
{
    using Reportr;
    using System.Data.Entity.Core.Objects;
    using System.Data.Entity.Infrastructure;
    using System.Linq;
    using System.Reflection;

    /// <summary>
    /// Provides various extension methods for the Entity Framework DbContext
    /// </summary>
    internal static class DbContextExtensions
    {
        /// <summary>
        /// Reads all date time values as UTC kind for every entity that is materialized
        /// </summary>
        /// <param name="context">The DbContext</param>
        /// <remarks>
        /// Adapted from http://stackoverflow.com/a/11683020
        /// </remarks>
        public static void ReadAllDateTimeValuesAsUtc
            (
                this DbContext context
            )
        {
            Validate.IsNotNull(context);

            var objectContext = ((IObjectContextAdapter)context).ObjectContext;

            objectContext.ObjectMaterialized += ReadAllDateTimeValuesAsUtc;
        }

        /// <summary>
        /// Uses reflection to read all date time properties and set the kind to UTC
        /// </summary>
        /// <param name="sender">The sender</param>
        /// <param name="e">The object materialized event arguments</param>
        private static void ReadAllDateTimeValuesAsUtc
            (
                object sender,
                ObjectMaterializedEventArgs e
            )
        {
            // Extract all DateTime properties of the object type
            var properties = e.Entity.GetType().GetProperties().Where
                (
                    property => property.PropertyType == typeof(DateTime)
                        || property.PropertyType == typeof(DateTime?)
                );

            // Set all DateTimeKinds to UTC
            properties.ToList().ForEach
            (
                property => SpecifyUtcKind(property, e.Entity)
            );
        }

        /// <summary>
        /// Specifies that the kind for a specified date time property is set to UTC
        /// </summary>
        /// <param name="property">The property to update</param>
        /// <param name="value">The value to set</param>
        private static void SpecifyUtcKind
            (
                PropertyInfo property,
                object value
            )
        {
            Validate.IsNotNull(property);

            // Get the date time value
            var datetime = property.GetValue(value, null);

            // Set DateTimeKind to UTC
            if (property.PropertyType == typeof(DateTime))
            {
                datetime = DateTime.SpecifyKind
                (
                    (DateTime)datetime,
                    DateTimeKind.Utc
                );
            }
            else if (property.PropertyType == typeof(DateTime?))
            {
                var nullable = (DateTime?)datetime;

                if (false == nullable.HasValue)
                {
                    return;
                }

                datetime = (DateTime?)DateTime.SpecifyKind
                (
                    nullable.Value,
                    DateTimeKind.Utc
                );
            }
            else
            {
                return;
            }

            // And finally set the UTC DateTime value
            property.SetValue
            (
                value,
                datetime,
                null
            );
        }
    }
}
