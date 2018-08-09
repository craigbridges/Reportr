namespace Reportr.Filtering.ValueGenerators
{
    using System;
    
    /// <summary>
    /// Represents a "last day" date parameter value generator
    /// </summary>
    public sealed class LastDayValueGenerator : DateValueGeneratorBase
    {
        private readonly DatePeriod _period;
        private readonly DayOfWeek _firstDayOfWeek;

        /// <summary>
        /// Constructs the last day value generator with a period
        /// </summary>
        /// <param name="period">The date period</param>
        /// <param name="firstDayOfWeek">The first day of the week</param>
        public LastDayValueGenerator
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
                        12,
                        31
                    );

                case DatePeriod.Month:

                    var startDate = new DateTime
                    (
                        today.Year,
                        today.Month,
                        1
                    );

                    var endDate = startDate.AddMonths(1).AddDays(-1);

                    return endDate;

                default:
                    return today.EndOfWeek
                    (
                        _firstDayOfWeek
                    );
            }
        }
    }
}
