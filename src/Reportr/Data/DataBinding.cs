namespace Reportr.Data
{
    /// <summary>
    /// Represents a single data binding
    /// </summary>
    public sealed class DataBinding
    {
        /// <summary>
        /// Constructs the data binding with the path
        /// </summary>
        /// <param name="path">The path</param>
        public DataBinding
            (
                string path
            )
        {
            Validate.IsNotEmpty(path);

            this.Path = path;
        }

        /// <summary>
        /// Gets the path of the bound property
        /// </summary>
        public string Path { get; private set; }
    }
}
