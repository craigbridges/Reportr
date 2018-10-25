namespace Reportr.Templating
{
    /// <summary>
    /// Defines a contract for a service that renders templates
    /// </summary>
    public interface ITemplateRenderer
    {
        /// <summary>
        /// Renders a template with a given model
        /// </summary>
        /// <param name="templateContent">The template content</param>
        /// <param name="model">The model</param>
        /// <returns>The rendered content</returns>
        string Render
        (
            string templateContent,
            object model
        );
    }
}
