namespace Reportr.Registration.Entity
{
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Linq;
    using System.Reflection;

    /// <summary>
    /// Provides various extension methods for the EF model builder
    /// </summary>
    internal static class ModelBuilderExtensions
    {
        /// <summary>
        /// Specifies the kind for all DateTime properties in an entity
        /// </summary>
        /// <typeparam name="TEntity">The entity type</typeparam>
        /// <param name="kind">The date kind to set</param>
        /// <param name="builder">The model builder</param>
        public static void SpecifyDateKind<TEntity>
            (
                this ModelBuilder builder,
                DateTimeKind kind = DateTimeKind.Utc
            )
            where TEntity : class
        {
            var allProperties = typeof(TEntity).GetProperties
            (
                BindingFlags.Public | BindingFlags.Instance
            );

            var dateProperties = allProperties.Where
            (
                info => info.PropertyType == typeof(DateTime) 
                     || info.PropertyType == typeof(DateTime?)
            );

            foreach (var property in dateProperties)
            {
                var entityBuilder = builder.Entity<TEntity>();
                var propertyName = property.Name;

                if (property.PropertyType == typeof(DateTime?))
                {
                    entityBuilder
                        .Property<DateTime?>(propertyName)
                        .HasConversion
                        (
                            date => date,
                            date => date == null ? null : (DateTime?)DateTime.SpecifyKind
                            (
                                date.Value,
                                kind
                            )
                        );
                }
                else
                {
                    entityBuilder
                        .Property<DateTime>(propertyName)
                        .HasConversion
                        (
                            date => date,
                            date => DateTime.SpecifyKind(date, kind)
                        );
                }
            }
        }
    }
}
