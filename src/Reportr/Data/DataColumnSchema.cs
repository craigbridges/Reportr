namespace Reportr.Data
{
    using System;
    
    /// <summary>
    /// Represents schema information about a single data column
    /// </summary>
    public sealed class DataColumnSchema
    {
        /// <summary>
        /// Constructs the column schema with the details
        /// </summary>
        /// <param name="name">The column name</param>
        /// <param name="valueType">The value type</param>
        public DataColumnSchema
            (
                string name,
                Type valueType
            )
        {
            Validate.IsNotEmpty(name);
            Validate.IsNotNull(valueType);

            this.Name = name;
            this.ValueType = valueType;
        }

        /// <summary>
        /// Gets the name of the column
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Gets the value type
        /// </summary>
        public Type ValueType { get; private set; }

        /// <summary>
        /// Adds descriptors to the column schema
        /// </summary>
        /// <param name="title">The title</param>
        /// <param name="description">The description</param>
        /// <param name="format">The value format</param>
        /// <returns>The updated column schema</returns>
        public DataColumnSchema WithDescriptors
            (
                string title,
                string description,
                string format = null
            )
        {
            this.Title = title;
            this.Description = description;
            this.Format = format;

            return this;
        }

        /// <summary>
        /// Gets the title of the column
        /// </summary>
        public string Title { get; private set; }

        /// <summary>
        /// Gets a description of the column
        /// </summary>
        public string Description { get; private set; }

        /// <summary>
        /// Gets a string specifying the value format
        /// </summary>
        public string Format { get; private set; }
    }
}
