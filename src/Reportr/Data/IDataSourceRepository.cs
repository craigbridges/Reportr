namespace Reportr.Data
{
    using System.Collections.Generic;

    /// <summary>
    /// Defines a contract for a repository that manages data sources
    /// </summary>
    public interface IDataSourceRepository
    {
        /// <summary>
        /// Adds a data source to the repository
        /// </summary>
        /// <param name="source">The data source</param>
        void AddDataSource
        (
            IDataSource source
        );

        /// <summary>
        /// Determines if a data source exists with the name specified
        /// </summary>
        /// <param name="name">The name of the data source</param>
        /// <returns>True, if the source exists; otherwise false</returns>
        bool DataSourceExists
        (
            string name
        );

        /// <summary>
        /// Gets a single data source by name
        /// </summary>
        /// <param name="name">The data source name</param>
        /// <returns>The data source</returns>
        IDataSource GetDataSource
        (
            string name
        );

        /// <summary>
        /// Gets all data sources in the repository
        /// </summary>
        /// <returns>A collection of data sources</returns>
        IEnumerable<IDataSource> GetAllDataSources();

        /// <summary>
        /// Removes a data source from the repository
        /// </summary>
        /// <param name="name">The data source name</param>
        void RemoveDataSource
        (
            string name
        );
    }
}
