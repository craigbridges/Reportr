namespace Reportr.Integrations.Razor
{
    using RazorEngine;
    using RazorEngine.Configuration;
    using RazorEngine.Templating;
    using RazorEngine.Text;
    using Reportr.Templating;
    using System;

    /// <summary>
    /// Represents a Razor implementation of the template renderer
    /// </summary>
    public sealed class RazorTemplateRenderer : ITemplateRenderer
    {
        /// <summary>
        /// Constructs the template renderer by configuring Razor
        /// </summary>
        public RazorTemplateRenderer()
        {
            var config = new TemplateServiceConfiguration()
            {
                Language = Language.CSharp,
                EncodedStringFactory = new RawStringFactory()
            };
            
            var service = RazorEngineService.Create
            (
                config
            );

            Engine.Razor = service;
        }

        /// <summary>
        /// Renders a template with a given model
        /// </summary>
        /// <param name="templateContent">The template content</param>
        /// <param name="model">The model</param>
        /// <returns>The rendered content</returns>
        public string Render
            (
                string templateContent,
                object model
            )
        {
            var modelType = default(Type);

            if (model != null)
            {
                modelType = model.GetType();
            }

            var result = Engine.Razor.RunCompile
            (
                templateContent,
                Guid.NewGuid().ToString(),
                modelType,
                model
            );

            return result;
        }
    }
}
