namespace Reportr.Nettle
{
    using global::Nettle;
    using global::Nettle.Compiler;
    using global::Nettle.Data;
    using global::Nettle.Functions;
    using global::Nettle.NCalc;
    using global::Nettle.Web;
    using System;
    using System.Web.Configuration;

    /// <summary>
    /// Generates compilers for the Nettle templating engine
    /// </summary>
    internal sealed class NettleCompilerGenerator
    {
        /// <summary>
        /// Generates a new Nettle compiler with custom functions
        /// </summary>
        /// <param name="customFunctions">The customer functions</param>
        /// <returns>The Nettle compiler</returns>
        public INettleCompiler GenerateCompiler
            (
                params IFunction[] customFunctions
            )
        {
            var dataResolver = new NettleDataResolver();
            var webResolver = new NettleWebResolver();
            var ncalcResolver = new NettleNCalcResolver();

            var appSettings = WebConfigurationManager.AppSettings;
            var defaultTimeZoneId = appSettings["DefaultTimeZoneId"];
            
            NettleEngine.RegisterResolvers
            (
                dataResolver,
                webResolver,
                ncalcResolver
            );

            if (false == String.IsNullOrEmpty(defaultTimeZoneId))
            {
                NettleEngine.SetDefaultTimeZone
                (
                    defaultTimeZoneId
                );
            }

            return NettleEngine.GetCompiler
            (
                customFunctions
            );
        }
    }
}
