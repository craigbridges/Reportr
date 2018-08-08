namespace Reportr.Filtering.ValueGenerators
{
    using System;
    
    /// <summary>
    /// Represents a "today" date parameter value generator
    /// </summary>
    public sealed class TodayValueGenerator : DateValueGeneratorBase
    {
        /// <summary>
        /// Generates a new parameter value
        /// </summary>
        /// <returns>The value generated</returns>
        public override object GenerateValue()
        {
            return DateTime.Today;
        }
    }
}
