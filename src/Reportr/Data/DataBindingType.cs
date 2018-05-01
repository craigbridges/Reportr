namespace Reportr.Data
{
    using System.ComponentModel;

    /// <summary>
    /// Defines the report data binding types
    /// </summary>
    public enum DataBindingType
    {
        /// <summary>
        /// Query paths use a dot delimited syntax to reference 
        /// a specific property or field from a query result.
        /// </summary>
        /// <remarks>
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
        /// </remarks>
        [Description("Query Path")]
        QueryPath = 0,

        /// <summary>
        /// Nettle expressions use the power of the Nettle 
        /// templating language to evaluate the binding value.
        /// </summary>
        /// <remarks>
        /// See the Nettle documentation for more details:
        /// https://github.com/craigbridges/Nettle/wiki
        /// </remarks>
        [Description("Nettle Expression")]
        NettleExpression = 1,

        /// <summary>
        /// Math expressions can be used to calculate a double
        /// value from a string based expression.
        /// </summary>
        /// <remarks>
        /// Reportr uses NCalc to evaluate math expressions.
        /// See the NCalc documentation for more details:
        /// https://github.com/sheetsync/NCalc/wiki
        /// </remarks>
        [Description("Math Expression")]
        MathExpression = 2
    }
}
