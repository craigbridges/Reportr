namespace Reportr.Culture
{
    using System;
    
    /// <summary>
    /// Represents a language
    /// </summary>
    public class Language
    {
        /// <summary>
        /// Constructs the language with the details
        /// </summary>
        /// <param name="name">The name</param>
        /// <param name="iso">The ISO</param>
        /// <param name="isDefault">Is this the default language?</param>
        public Language
            (
                string name,
                string iso,
                bool isDefault = false
            )
        {
            Validate.IsNotEmpty(name);
            Validate.IsNotEmpty(iso);

            this.Name = name;
            this.Iso = iso;
            this.Default = isDefault;
        }

        /// <summary>
        /// Gets the name of the language
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Gets the language ISO
        /// </summary>
        public string Iso { get; private set; }

        /// <summary>
        /// Gets a flag indicating if the language is the default
        /// </summary>
        public bool Default { get; private set; }

        /// <summary>
        /// Generates a custom hash code for the unit
        /// </summary>
        /// <returns>The hash code</returns>
        public override int GetHashCode()
        {
            var tuple = Tuple.Create
            (
                this.Name,
                this.Iso,
                this.Default
            );

            return tuple.GetHashCode();
        }

        /// <summary>
        /// Determines if an object is equal to the current language
        /// </summary>
        /// <param name="obj">The object to check</param>
        /// <returns>True, if both objects are equal; otherwise false</returns>
        public override bool Equals
            (
                object obj
            )
        {
            if (obj is null || false == (obj is Language))
            {
                return false;
            }
            else
            {
                var otherParam = (Language)obj;

                var isoMatches = otherParam.Iso.Equals
                (
                    this.Iso,
                    StringComparison.OrdinalIgnoreCase
                );

                return isoMatches;
            }
        }

        /// <summary>
        /// Compares two parameter value instances to determine if they are equal
        /// </summary>
        /// <param name="left">The left value</param>
        /// <param name="right">The right value</param>
        /// <returns>True, if both objects are equal; otherwise false</returns>
        public static bool operator ==(Language left, Language right)
        {
            if (left is null)
            {
                return right is null;
            }
            else
            {
                return left.Equals(right);
            }
        }

        /// <summary>
        /// Compares two parameter value instances to determine if they are not equal
        /// </summary>
        /// <param name="left">The left value</param>
        /// <param name="right">The right value</param>
        /// <returns>True, if both objects are not equal; otherwise false</returns>
        public static bool operator !=(Language left, Language right)
        {
            if (left is null)
            {
                return false == (right is null);
            }
            else
            {
                return false == left.Equals(right);
            }
        }

        /// <summary>
        /// Provides a descriptor for the objects current state
        /// </summary>
        /// <returns>The language name</returns>
        public override string ToString()
        {
            return $"{this.Name}";
        }
    }
}
