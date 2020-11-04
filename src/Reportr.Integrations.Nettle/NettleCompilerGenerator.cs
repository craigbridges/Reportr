namespace Reportr.Integrations.Nettle
{
    using global::Nettle;
    using global::Nettle.Compiler;
    using global::Nettle.Data;
    using global::Nettle.Functions;
    using global::Nettle.Web;
    using System;
    using System.Configuration;

    /// <summary>
    /// Generates compilers for the Nettle templating engine
    /// </summary>
    internal sealed class NettleCompilerGenerator
    {
        private readonly IFunction[] _customFunctions;

        /// <summary>
        /// Constructs the compiler generator with customer functions
        /// </summary>
        /// <param name="customFunctions">The customer Nettle functions</param>
        public NettleCompilerGenerator(params IFunction[] customFunctions)
        {
            _customFunctions = customFunctions;
        }

        /// <summary>
        /// Generates a new Nettle compiler with custom functions
        /// </summary>
        /// <returns>The Nettle compiler</returns>
        public INettleCompiler Generate()
        {
            var dataResolver = new NettleDataResolver();
            var webResolver = new NettleWebResolver();

            var appSettings = ConfigurationManager.AppSettings;
            var defaultTimeZoneId = appSettings["DefaultTimeZoneId"];

            NettleEngine.RegisterResolvers(dataResolver, webResolver);

            if (false == String.IsNullOrEmpty(defaultTimeZoneId))
            {
                NettleEngine.SetDefaultTimeZone(defaultTimeZoneId);
            }

            return NettleEngine.GetCompiler(_customFunctions);
        }
    }
}
