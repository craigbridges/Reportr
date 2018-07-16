namespace Reportr
{
    using Reportr.Integrations;
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
        /// Gets the template renderer instance
        /// </summary>
        public static ITemplateRenderer TemplateRenderer
        {
            get
            {
                return ResolveService<ITemplateRenderer>();
            }
        }

        /// <summary>
        /// Gets the math expression evaluator instance
        /// </summary>
        public static IMathExpressionEvaluator MathEvaluator
        {
            get
            {
                return ResolveService<IMathExpressionEvaluator>();
            }
        }

        /// <summary>
        /// Gets or sets the activator service
        /// </summary>
        public static IDependencyResolver Activator { get; set; }

        /// <summary>
        /// Resolves a service of the type specified
        /// </summary>
        /// <typeparam name="T">The service type</typeparam>
        /// <returns>The service instance</returns>
        private static T ResolveService<T>() where T : class
        {
            if (ReportrEngine.Activator == null)
            {
                throw new InvalidOperationException
                (
                    "ReportrEngine.Activator has not been configured."
                );
            }

            return ReportrEngine.Activator.Resolve<T>();
        }

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
