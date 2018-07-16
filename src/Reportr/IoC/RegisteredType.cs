namespace Reportr.Integrations
{
    using System;

    /// <summary>
    /// Represents a single registered type
    /// </summary>
    public class RegisteredType
    {
        /// <summary>
        /// Constructs the registered types
        /// </summary>
        /// <param name="sourceType">The source type</param>
        /// <param name="implementationType">The implementation type</param>
        public RegisteredType
            (
                Type sourceType,
                Type implementationType
            )
        {
            Validate.IsNotNull(sourceType);
            Validate.IsNotNull(implementationType);

            if (implementationType.IsAbstract || implementationType.IsInterface)
            {
                throw new ArgumentException
                (
                    "The implementation type cannot be abstract."
                );
            }

            this.SourceType = sourceType;
            this.ImplementationType = implementationType;
        }

        /// <summary>
        /// Gets the source type
        /// </summary>
        public Type SourceType { get; private set; }

        /// <summary>
        /// Gets the implementation type
        /// </summary>
        public Type ImplementationType { get; private set; }
    }
}
