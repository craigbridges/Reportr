namespace Reportr.Filtering.ValueGenerators
{
    using System;
    
    /// <summary>
    /// Represents a "first day" date parameter value generator
    /// </summary>
    public sealed class FirstDayValueGenerator : DateValueGeneratorBase
    {
        private readonly DatePeriod _period;
        private readonly DayOfWeek _firstDayOfWeek;

        /// <summary>
        /// Constructs the first day value generator with a period
        /// </summary>
        /// <param name="period">The date period</param>
        /// <param name="firstDayOfWeek">The first day of the week</param>
        public FirstDayValueGenerator
            (
                DatePeriod period,
                DayOfWeek firstDayOfWeek = DayOfWeek.Monday
            )
        {
            _period = period;
            _firstDayOfWeek = firstDayOfWeek;
        }

        /// <summary>
        /// Generates a new parameter value
        /// </summary>
        /// <returns>The value generated</returns>
        public override object GenerateValue()
        {
            var today = DateTime.Today;

            switch (_period)
            {
                case DatePeriod.Year:

                    return new DateTime
                    (
                        today.Year,
                        1,
                        1
                    );

                case DatePeriod.Month:

                    return new DateTime
                    (
                        today.Year,
                        today.Month,
                        1
                    );

                default:
                    return today.StartOfWeek
                    (
                        _firstDayOfWeek
                    );
            }
        }
    }
}
