namespace Reportr.Integrations.NCalc
{
    using Reportr.Integrations;
    using System.Collections.Generic;
    
    /// <summary>
    /// Represents an NCalc type register implementation
    /// </summary>
    public sealed class NCalcTypeRegister : ITypeRegister
    {
        /// <summary>
        /// Gets a collection of all registered types
        /// </summary>
        /// <returns>A collection of registered types</returns>
        public IEnumerable<RegisteredType> GetRegisteredTypes()
        {
            return new List<RegisteredType>()
            {
                new RegisteredType
                (
                    typeof(IMathExpressionEvaluator),
                    typeof(NCalcEvaluator)
                )
            };
        }
    }
}
