namespace Reportr
{
    using Reportr.Templating;
    using System;
    
    /// <summary>
    /// Represents the entry point for all Reportr actions
    /// </summary>
    public static class ReportrEngine
    {
        private static object _engineLock = new object();
        private static TimeZoneInfo _defaultTimeZone = TimeZoneInfo.Local;

        /// <summary>
        /// Gets or sets the template renderer instance
        /// </summary>
        public static ITemplateRenderer TemplateRenderer { get; set; }

        /// <summary>
        /// Gets or sets the math expression evaluator instance
        /// </summary>
        public static IMathExpressionEvaluator MathEvaluator { get; set; }
                
        /// <summary>
        /// Gets the default time zone to use for dates
        /// </summary>
        public static TimeZoneInfo DefaultTimeZone
        {
            get
            {
                return _defaultTimeZone;
            }
        }

        /// <summary>
        /// Sets the default time zone to use for all date times
        /// </summary>
        /// <param name="timeZoneId">The time zone ID</param>
        public static void SetDefaultTimeZone
            (
                string timeZoneId
            )
        {
            Validate.IsNotEmpty(timeZoneId);

            lock (_engineLock)
            {
                _defaultTimeZone = TimeZoneInfo.FindSystemTimeZoneById
                (
                    timeZoneId
                );
            }
        }
    }
}
