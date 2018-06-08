namespace Reportr.Data
{
    using System;
    
    /// <summary>
    /// Represents a base class for data source implementations
    /// </summary>
    public abstract class DataSourceBase : IDataSource
    {
        private bool _disposeCalled = false;

        /// <summary>
        /// Constructs the data source with a connection
        /// </summary>
        /// <param name="connection">The data connection</param>
        public DataSourceBase
            (
                IDataConnection connection
            )
        {
            Validate.IsNotNull(connection);

            this.SourceId = Guid.NewGuid();
            this.Connection = connection;
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
        public virtual string Name
        {
            get
            {
                return this.GetType().Name;
            }
        }

        /// <summary>
        /// Gets the connection used by the data source
        /// </summary>
        public IDataConnection Connection { get; private set; }

        /// <summary>
        /// Gets an array of the tables held by the data source
        /// </summary>
        public abstract DataTableSchema[] Schema { get; }

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
                    this.Connection.Dispose();

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
