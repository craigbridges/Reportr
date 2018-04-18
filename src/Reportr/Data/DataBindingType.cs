namespace Reportr.Data
{
    using System.ComponentModel;

    /// <summary>
    /// Defines data binding types
    /// </summary>
    public enum DataBindingType
    {
        /// <summary>
        /// Query paths use a dot delimited syntax to reference 
        /// a specific property or field from a query result.
        /// 
        /// The path can reference a column or a nested property
        /// within the value for that column.
        /// 
        /// To reference a column, simply enter the name of the 
        /// column. To reference a nested property, enter the 
        /// name of the column followed by the nested property
        /// chain; separated by dots.
        /// 
        /// Example: Column1.Property1.Property2
        /// 
        /// The example above references the column named Column1
        /// followed by a property named Property1, followed by
        /// a nested property named Property2. The value returned
        /// would be the value contained in Property2.
        /// </summary>
        [Description("Query Path")]
        QueryPath = 0,

        /// <summary>
        /// Nettle expressions use the power of the Nettle 
        /// templating language to evaluate the binding value.
        /// 
        /// See the Nettle documentation for more details:
        /// https://github.com/craigbridges/Nettle/wiki
        /// </summary>
        [Description("Nettle Expression")]
        NettleExpression = 1
    }
}
