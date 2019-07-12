namespace Reportr.Globalization
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Various extension methods for the ILocalizable interface
    /// </summary>
    public static class LocalizableExtensions
    {
        /// <summary>
        /// Localizes an object using globalization options
        /// </summary>
        /// <param name="item">The object to localize</param>
        /// <param name="translator">The phrase translator</param>
        /// <param name="options">The globalization options</param>
        public static void Localize
            (
                this ILocalizable item,
                PhraseTranslationDictionary translator,
                GlobalizationOptions options
            )
        {
            Validate.IsNotNull(item);

            if (options?.PreferredLanguage != null)
            {
                item.Translate
                (
                    translator,
                    options.PreferredLanguage
                );
            }
        }

        /// <summary>
        /// Localizes a collection of objects using globalization options
        /// </summary>
        /// <param name="items">The objects to localize</param>
        /// <param name="translator">The phrase translator</param>
        /// <param name="options">The globalization options</param>
        public static void Localize
            (
                this IEnumerable<ILocalizable> items,
                PhraseTranslationDictionary translator,
                GlobalizationOptions options
            )
        {
            Validate.IsNotNull(items);

            items.ToList().ForEach
            (
                item => item.Localize
                (
                    translator,
                    options
                )
            );
        }
    }
}
