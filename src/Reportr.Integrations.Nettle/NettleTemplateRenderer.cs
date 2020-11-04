namespace Reportr.Integrations.Nettle
{
    using global::Nettle.Compiler;
    using Reportr.Integrations.Nettle.Functions;
    using Reportr.Templating;
    
    /// <summary>
    /// Represents a Nettle implementation of the template renderer
    /// </summary>
    public sealed class NettleTemplateRenderer : ITemplateRenderer
    {
        private INettleCompiler _nettleCompiler;
        
        /// <summary>
        /// Renders a template with a given model
        /// </summary>
        /// <param name="templateContent">The template content</param>
        /// <param name="model">The model</param>
        /// <returns>The rendered content</returns>
        public string Render(string templateContent, object model)
        {
            if (_nettleCompiler == null)
            {
                var generator = new NettleCompilerGenerator
                (
                    new ConcatenateQueryCellsFunction(),
                    new GetQueryCellValueFunction(),
                    new GcdFunction(),
                    new AsRatioFunction()
                );

                _nettleCompiler = generator.Generate();
            }

            var template = _nettleCompiler.Compile(templateContent);

            return template(model);
        }
    }
}
