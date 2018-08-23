namespace Reportr.Data.Querying
{
    using System;
    using System.Linq;

    /// <summary>
    /// Represents information about a single query path
    /// </summary>
    internal sealed class QueryPathInfo
    {
        /// <summary>
        /// Constructs the path info with a path
        /// </summary>
        /// <param name="path">The path</param>
        public QueryPathInfo
            (
                string path
            )
        {
            this.FullPath = path;

            ParsePath(path);
        }

        /// <summary>
        /// Parses the path into its atomic components
        /// </summary>
        /// <param name="path">The path</param>
        private void ParsePath
            (
                string path
            )
        {
            if (String.IsNullOrWhiteSpace(path))
            {
                throw new ArgumentException
                (
                    "The path cannot be empty."
                );
            }

            var segments = path.Split('.');

            if (segments.Contains(String.Empty))
            {
                throw new ArgumentException
                (
                    $"The path '{path}' is invalid."
                );
            }

            this.ColumnName = segments[0];

            if (segments.Length > 1)
            {
                this.NestedProperties = segments.Skip(1).ToArray();
            }
            else
            {
                this.NestedProperties = new string[] { };
            }
        }

        /// <summary>
        /// Gets the full original path
        /// </summary>
        public string FullPath { get; private set; }

        /// <summary>
        /// Gets the column name specified in the path
        /// </summary>
        public string ColumnName { get; private set; }

        /// <summary>
        /// Gets an array of path nested properties
        /// </summary>
        public string[] NestedProperties { get; private set; }
    }
}
