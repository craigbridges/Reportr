namespace Reportr.IoC
{
    /// <summary>
    /// Defines a contract for a service that resolves dependencies
    /// </summary>
    public interface IDependencyResolver
    {
        /// <summary>
        /// Resolves an instance of the type specified
        /// </summary>
        /// <typeparam name="TService">The type to resolve</typeparam>
        /// <returns>An instance of the type specified</returns>
        TService Resolve<TService>()
            where TService : class;
    }
}
