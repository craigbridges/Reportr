namespace Reportr.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Represents a base class for data source implementations
    /// </summary>
    public abstract class DataSourceBase : IDataSource
    {
        private bool _disposeCalled = false;

        /// <summary>
        /// Constructs the data source with default values
        /// </summary>
        /// <param name="name">The name of the data source</param>
        public DataSourceBase
            (
                string name
            )
        {
            Validate.IsNotEmpty(name);

            this.SourceId = Guid.NewGuid();
            this.Name = name;
        }

        /// <summary>
        /// Destructs the data source by disposing resources
        /// </summary>
        ~DataSourceBase()
        {
            Dispose(false);
        }

        /// <summary>
        /// Gets the unique ID of the data source
        /// </summary>
        public Guid SourceId { get; private set; }

        /// <summary>
        /// Gets the name of the data source
        /// </summary>
        public string Name { get; private set; }
        
        /// <summary>
        /// Gets an array of the tables held by the data source
        /// </summary>
        public abstract DataTableSchema[] Schema { get; }

        /// <summary>
        /// Gets a flag indicating if the data schema is unresolvable
        /// </summary>
        public bool SchemaUnresolvable { get; protected set; }

        /// <summary>
        /// Gets the error message generated while trying to resolve the schema
        /// </summary>
        public string SchemaGenerationErrorMessage { get; protected set; }

        /// <summary>
        /// Marks the data source schema as unresolvable with the error
        /// </summary>
        /// <param name="errorMessage">The error message</param>
        protected void MarkSchemaAsUnresolvable
            (
                string errorMessage
            )
        {
            this.SchemaUnresolvable = true;
            this.SchemaGenerationErrorMessage = errorMessage;
        }

        /// <summary>
        /// Gets a table from the database schema
        /// </summary>
        /// <param name="name">The table name</param>
        /// <returns>The table schema</returns>
        public DataTableSchema GetSchemaTable
            (
                string name
            )
        {
            Validate.IsNotEmpty(name);

            var table = this.Schema.FirstOrDefault
            (
                dts => dts.Name.ToLower() == name.ToLower()
            );

            if (table == null)
            {
                var message = "The data source does not contain a table named '{0}'.";

                throw new KeyNotFoundException
                (
                    String.Format(message, name)
                );
            }

            return table;
        }

        /// <summary>
        /// Provides a custom description of the data source
        /// </summary>
        /// <returns>The data source name</returns>
        public override string ToString()
        {
            return this.Name;
        }

        /// <summary>
        /// Disposes the data source by freeing up managed and unmanaged objects
        /// </summary>
        /// <param name="disposing">True, if the data source is disposing</param>
        protected virtual void Dispose
            (
                bool disposing
            )
        {
            if (false == _disposeCalled)
            {
                if (disposing)
                {
                    DisposeManagedObjects();
                }

                DisposeUnanagedObjects();
                
                _disposeCalled = true;
            }
        }

        /// <summary>
        /// Frees up any managed objects used by the data source
        /// </summary>
        protected virtual void DisposeManagedObjects()
        {
            // NOTE: nothing to free up
        }

        /// <summary>
        /// Frees up any unmanaged objects used by the data source
        /// </summary>
        protected virtual void DisposeUnanagedObjects()
        {
            // NOTE: nothing to free up
        }

        /// <summary>
        /// Performs application-defined tasks associated with 
        /// freeing, releasing, or resetting unmanaged resources
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
        }
    }
}
