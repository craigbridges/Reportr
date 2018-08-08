namespace Reportr.Filtering.ValueGenerators
{
    using System;
    
    /// <summary>
    /// Represents a date parameter value generator base class
    /// </summary>
    public abstract class DateValueGeneratorBase : IParameterValueGenerator
    {
        /// <summary>
        /// Gets the type of the value that will be generated
        /// </summary>
        public Type ValueType
        {
            get
            {
                return typeof(DateTime);
            }
        }

        /// <summary>
        /// Generates a new parameter value
        /// </summary>
        /// <returns>The value generated</returns>
        public abstract object GenerateValue();
    }
}
