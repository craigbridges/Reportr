namespace Reportr.Integrations
{
    using System;
    
    /// <summary>
    /// Defines a contract for a dependency registrar
    /// </summary>
    public interface IDependencyRegistrar
    {
        /// <summary>
        /// Registers an implementation type against a specified type
        /// </summary>
        /// <typeparam name="TType">The registered type</typeparam>
        /// <typeparam name="TImpl">The implementation type</typeparam>
        void Register<TType, TImpl>()
            where TType : class
            where TImpl : class, TType;

        /// <summary>
        /// Registers an instance of the type specified
        /// </summary>
        /// <typeparam name="TType">The instance type</typeparam>
        /// <param name="instance">The instance to register</param>
        void RegisterInstance<TType>
            (
                TType instance
            )
            where TType : class;
    }
}
