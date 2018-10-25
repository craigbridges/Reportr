namespace Reportr.Data
{
    using System;

    /// <summary>
    /// Defines a contract for a report data source
    /// </summary>
    public interface IDataSource : IDisposable
    {
        /// <summary>
        /// Gets the unique ID of the data source
        /// </summary>
        Guid SourceId { get; }

        /// <summary>
        /// Gets the name of the data source
        /// </summary>
        string Name { get; }
        
        /// <summary>
        /// Gets an array of the tables held by the data source
        /// </summary>
        DataTableSchema[] Schema { get; }

        /// <summary>
        /// Gets a flag indicating if the data schema is unresolvable
        /// </summary>
        bool SchemaUnresolvable { get; }

        /// <summary>
        /// Gets the error message generated while trying to resolve the schema
        /// </summary>
        string SchemaGenerationErrorMessage { get; }

        /// <summary>
        /// Gets a schema table from the data source
        /// </summary>
        /// <param name="name">The table name</param>
        /// <returns>The table schema</returns>
        DataTableSchema GetSchemaTable
        (
            string name
        );
    }
}
