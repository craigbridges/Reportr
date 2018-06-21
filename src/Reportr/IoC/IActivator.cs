namespace Reportr.IoC
{
    /// <summary>
    /// Defines a contract for a service that activates type instances
    /// </summary>
    public interface IActivator
    {
        /// <summary>
        /// Resolves the type specified
        /// </summary>
        /// <typeparam name="T">The type to resolve</typeparam>
        /// <returns>An instance of the type specified</returns>
        T Resolve<T>();
    }
}
