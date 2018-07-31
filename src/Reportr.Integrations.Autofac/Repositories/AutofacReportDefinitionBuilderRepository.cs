namespace Reportr.Integrations.Autofac.Repositories
{
    using global::Autofac;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Represents an Autofac report definition builder repository implementation
    /// </summary>
    public sealed class AutofacReportDefinitionBuilderRepository : IReportDefinitionBuilderRepository
    {
        private readonly ILifetimeScope _context;
        private List<IReportDefinitionBuilder> _builders;

        /// <summary>
        /// Constructs the repository with a component context
        /// </summary>
        /// <param name="context">The Autofac component context</param>
        public AutofacReportDefinitionBuilderRepository
            (
                ILifetimeScope context
            )
        {
            Validate.IsNotNull(context);

            _context = context;
            _builders = context.Resolve<IEnumerable<IReportDefinitionBuilder>>().ToList();
        }

        /// <summary>
        /// Adds a report builder to the repository
        /// </summary>
        /// <param name="builder">The report builder</param>
        public void AddBuilder
            (
                IReportDefinitionBuilder builder
            )
        {
            Validate.IsNotNull(builder);

            var builderType = builder.GetType();

            // Ensure the builder has not already been added
            var matchFound = _builders.Any
            (
                b => b.GetType() == builderType
            );

            if (matchFound)
            {
                var message = "The report builder {0} has already been registered.";

                throw new InvalidOperationException
                (
                    String.Format
                    (
                        message,
                        builderType.Name
                    )
                );
            }

            _builders.Add(builder);
        }

        /// <summary>
        /// Gets a report builder for the type specified
        /// </summary>
        /// <typeparam name="T">The builder type</typeparam>
        /// <returns>The matching report builder</returns>
        public IReportDefinitionBuilder GetBuilder<T>()
        {
            return GetBuilder
            (
                typeof(T)
            );
        }

        /// <summary>
        /// Gets a report builder for the type specified
        /// </summary>
        /// <param name="builderType">The builder type</param>
        /// <returns>The matching report builder</returns>
        public IReportDefinitionBuilder GetBuilder
            (
                Type builderType
            )
        {
            Validate.IsNotNull(builderType);

            var builder = _builders.FirstOrDefault
            (
                b => b.GetType() == builderType
            );

            if (builder == null)
            {
                var message = "The type {0} did not match any report builders.";

                throw new KeyNotFoundException
                (
                    String.Format
                    (
                        message,
                        builderType.Name
                    )
                );
            }

            return builder;
        }

        /// <summary>
        /// Gets a report builder for the type specified
        /// </summary>
        /// <param name="fullTypeName">The builder full type name</param>
        /// <returns>The matching report builder</returns>
        public IReportDefinitionBuilder GetBuilder
            (
                string fullTypeName
            )
        {
            Validate.IsNotEmpty(fullTypeName);

            var builderType = Type.GetType(fullTypeName);

            if (builderType == null)
            {
                var message = "The report builder type '{0}' could not be resolved.";

                throw new KeyNotFoundException
                (
                    String.Format
                    (
                        message,
                        fullTypeName
                    )
                );
            }

            return GetBuilder(builderType);
        }

        /// <summary>
        /// Gets all report builders in the repository
        /// </summary>
        /// <returns>A collection of report builders</returns>
        public IEnumerable<IReportDefinitionBuilder> GetAllBuilders()
        {
            return _builders.ToArray();
        }
    }
}
