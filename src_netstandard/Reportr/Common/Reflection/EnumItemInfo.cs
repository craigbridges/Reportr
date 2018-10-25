namespace Reportr.Common.Reflection
{
    /// <summary>
    /// Represents information about a single enum item
    /// </summary>
    public sealed class EnumItemInfo
    {
        /// <summary>
        /// Constructs an enum item information with the value, name and description
        /// </summary>
        /// <param name="value">The value</param>
        /// <param name="name">The name</param>
        /// <param name="description">The description</param>
        internal EnumItemInfo
            (
                int value,
                string name,
                string description
            )
        {
            this.Value = value;
            this.Name = name;
            this.Description = description;
        }

        /// <summary>
        /// Gets the enums value
        /// </summary>
        public int Value { get; private set; }

        /// <summary>
        /// Gets the enums name
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Gets the enums description
        /// </summary>
        public string Description { get; private set; }
    }
}
