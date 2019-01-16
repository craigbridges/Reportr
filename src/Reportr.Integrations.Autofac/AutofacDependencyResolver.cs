namespace Reportr.Integrations.Autofac
{
    using global::Autofac;
    using Reportr.IoC;
    using System;

    /// <summary>
    /// Represents an Autofac dependency resolver implementation
    /// </summary>
    public sealed class AutofacDependencyResolver : IDependencyResolver
    {
        private readonly ILifetimeScope _context;

        /// <summary>
        /// Constructs a dependency resolver with a component context
        /// </summary>
        /// <param name="context">The Autofac component context</param>
        public AutofacDependencyResolver
            (
                ILifetimeScope context
            )
        {
            Validate.IsNotNull(context);

            _context = context;
        }

        /// <summary>
        /// Resolves an instance of the type specified
        /// </summary>
        /// <typeparam name="TService">The type to resolve</typeparam>
        /// <returns>An instance of the type specified</returns>
        public TService Resolve<TService>()
            where TService : class
        {
            return _context.Resolve<TService>();
        }
    }
}
