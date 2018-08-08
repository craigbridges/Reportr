namespace Reportr.Filtering.ValueGenerators
{
    using System;
    
    /// <summary>
    /// Represents a date offset parameter value generator
    /// </summary>
    public sealed class DateOffsetValueGenerator : DateValueGeneratorBase
    {
        private readonly TimeSpan _offsetSpan;

        /// <summary>
        /// Constructs the offset value generator with a time span
        /// </summary>
        /// <param name="offsetSpan">The offset time span</param>
        public DateOffsetValueGenerator
            (
                TimeSpan offsetSpan
            )
        {
            _offsetSpan = offsetSpan;
        }

        /// <summary>
        /// Generates a new parameter value
        /// </summary>
        /// <returns>The value generated</returns>
        public override object GenerateValue()
        {
            var today = DateTime.Today;

            var dateOffset = new DateTimeOffset
            (
                today,
                _offsetSpan
            );

            return dateOffset.DateTime;
        }
    }
}
