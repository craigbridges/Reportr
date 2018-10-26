namespace Reportr.Registration.Entity.Configurations
{
    using Humanizer;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    /// <summary>
    /// Represents a base entity configuration for all aggregate entities
    /// </summary>
    /// <typeparam name="TEntity">The entity type to configure</typeparam>
    public abstract class AggregateEntityTypeConfiguration<TEntity> : IEntityTypeConfiguration<TEntity>
        where TEntity : class, IAggregate
    {
        public virtual void Configure
            (
                EntityTypeBuilder<TEntity> builder
            )
        {
            ApplyCustomConfiguration(builder);

            builder.HasKey(m => m.Id);
            builder.HasIndex(m => m.Id);

            builder.ToTable
            (
                typeof(TEntity).Name.Pluralize()
            );
        }

        protected virtual void ApplyCustomConfiguration
            (
                EntityTypeBuilder<TEntity> builder
            )
        { }
    }
}
