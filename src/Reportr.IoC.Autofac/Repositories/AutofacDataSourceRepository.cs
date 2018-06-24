namespace Reportr.IoC.Autofac.Repositories
{
    using global::Autofac;
    using Reportr.Data;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Represents an Autofac data source repository implementation
    /// </summary>
    public sealed class AutofacDataSourceRepository : IDataSourceRepository
    {
        private readonly IComponentContext _context;
        private IEnumerable<IDataSource> _sources;

        /// <summary>
        /// Constructs a data source repository with a component context
        /// </summary>
        /// <param name="context">The Autofac component context</param>
        public AutofacDataSourceRepository
            (
                IComponentContext context
            )
        {
            Validate.IsNotNull(context);

            _context = context;
            _sources = context.Resolve<IEnumerable<IDataSource>>();
        }

        /// <summary>
        /// Adds a data source to the repository
        /// </summary>
        /// <param name="source">The data source</param>
        public void AddDataSource
            (
                IDataSource source
            )
        {
            Validate.IsNotNull(source);

            var found = DataSourceExists
            (
                source.Name
            );

            if (found)
            {
                var message = "The data source '{0}' has already been added.";

                throw new InvalidOperationException
                (
                    String.Format
                    (
                        message,
                        source
                    )
                );
            }

            var sourceList = _sources.ToList();

            sourceList.Add(source);

            _sources = sourceList.ToArray();
        }

        /// <summary>
        /// Determines if a data source exists with the name specified
        /// </summary>
        /// <param name="name">The name of the data source</param>
        /// <returns>True, if the source exists; otherwise false</returns>
        public bool DataSourceExists
            (
                string name
            )
        {
            Validate.IsNotEmpty(name);

            return _sources.Any
            (
                m => m.Name.ToLower() == name.ToLower()
            );
        }

        /// <summary>
        /// Gets a single data source by name
        /// </summary>
        /// <param name="name">The data source name</param>
        /// <returns>The data source</returns>
        public IDataSource GetDataSource
            (
                string name
            )
        {
            Validate.IsNotEmpty(name);

            var source = _sources.FirstOrDefault
            (
                m => m.Name.ToLower() == name.ToLower()
            );

            if (source == null)
            {
                var message = "No data source was found with the name '{0}'.";

                throw new KeyNotFoundException
                (
                    String.Format
                    (
                        message,
                        name
                    )
                );
            }

            return source;
        }

        /// <summary>
        /// Gets all data sources in the repository
        /// </summary>
        /// <returns>A collection of data sources</returns>
        public IEnumerable<IDataSource> GetAllDataSources()
        {
            return _sources.OrderBy
            (
                a => a.Name
            );
        }

        /// <summary>
        /// Removes a data source from the repository
        /// </summary>
        /// <param name="name">The data source name</param>
        public void RemoveDataSource
            (
                string name
            )
        {
            Validate.IsNotEmpty(name);

            var source = GetDataSource(name);
            var sourceList = _sources.ToList();

            sourceList.Remove(source);

            _sources = sourceList.ToArray();
        }
    }
}
