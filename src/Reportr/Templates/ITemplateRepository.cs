namespace Reportr.Templates
{
    using System.Collections.Generic;
    
    /// <summary>
    /// Defines a contract for a repository that manages templates
    /// </summary>
    /// <typeparam name="T">The template type</typeparam>
    public interface ITemplateRepository<T> where T : Template
    {
        /// <summary>
        /// Adds a single template to the repository
        /// </summary>
        /// <param name="template">The template</param>
        void AddTemplate
        (
            T template
        );

        /// <summary>
        /// Gets a single template from the repository
        /// </summary>
        /// <param name="name">The template name</param>
        T GetTemplate
        (
            string name
        );

        /// <summary>
        /// Gets all templates in the repository
        /// </summary>
        /// <returns>A collection of templates</returns>
        IEnumerable<T> GetAllTemplates();

        /// <summary>
        /// Removes a single template from the repository
        /// </summary>
        /// <param name="name">The template name</param>
        void RemoveTemplate
        (
            string name
        );
    }
}
