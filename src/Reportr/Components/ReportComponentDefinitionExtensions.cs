namespace Reportr.Components
{
    using System;
    
    /// <summary>
    /// Represents various extension methods for report component definitions
    /// </summary>
    internal static class ReportComponentDefinitionExtensions
    {
        /// <summary>
        /// Gets the component definition as the type specified
        /// </summary>
        /// <typeparam name="T">The definition type</typeparam>
        /// <param name="definition">The definition instance</param>
        /// <returns>The converted definition</returns>
        public static T As<T>
            (
                this IReportComponentDefinition definition
            )
            where T: IReportComponentDefinition
        {
            var actualType = definition.GetType();

            if (actualType != typeof(T))
            {
                if (false == typeof(T).IsAssignableFrom(actualType))
                {
                    var message = "The component {0} cannot be converted to {1}.";

                    throw new InvalidCastException
                    (
                        String.Format
                        (
                            message,
                            actualType.Name,
                            typeof(T).Name
                        )
                    );
                }
                else
                {
                    return (T)definition;
                }
            }
            else
            {
                return (T)definition;
            }
        }
    }
}
