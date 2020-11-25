namespace Reportr.Registration.Globalization
{
    using Reportr.Globalization;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Represents the default language factory implementation
    /// </summary>
    public sealed class LanguageFactory : ILanguageFactory
    {
        private readonly Dictionary<string, Language> _languageCache;
        private readonly IRegisteredLanguageRepository _languageRepository;

        public LanguageFactory(IRegisteredLanguageRepository languageRepository)
        {
            Validate.IsNotNull(languageRepository);

            _languageCache = new Dictionary<string, Language>(StringComparer.OrdinalIgnoreCase);
            _languageRepository = languageRepository;
        }

        public Language GetLanguage(string iso)
        {
            if (_languageCache.ContainsKey(iso))
            {
                return _languageCache[iso];
            }
            else
            {
                var registeredLanguage = _languageRepository.FindLanguage(iso);

                Language language;

                if (registeredLanguage == null)
                {
                    language = new Language(iso, iso);
                }
                else
                {
                    language = registeredLanguage.ToLanguage();
                }

                _languageCache.Add(iso, language);

                return language;
            }
        }
    }
}
