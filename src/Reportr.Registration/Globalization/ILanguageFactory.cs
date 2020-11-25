namespace Reportr.Registration.Globalization
{
    using Reportr.Globalization;
    
    /// <summary>
    /// Defines a factory that creates languages
    /// </summary>
    public interface ILanguageFactory
    {
        /// <summary>
        /// Gets a language from a language ISO name
        /// </summary>
        /// <param name="iso">The ISO name</param>
        /// <returns>The language</returns>
        Language GetLanguage(string iso);
    }
}
