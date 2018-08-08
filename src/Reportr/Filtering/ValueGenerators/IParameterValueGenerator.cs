namespace Reportr.Filtering.ValueGenerators
{
    using System;

    /// <summary>
    /// Defines a contract for a parameter value generator
    /// </summary>
    public interface IParameterValueGenerator
    {
        /// <summary>
        /// Gets the type of the value that will be generated
        /// </summary>
        Type ValueType { get; }

        /// <summary>
        /// Generates a new parameter value
        /// </summary>
        /// <returns>The value generated</returns>
        object GenerateValue();
    }
}
