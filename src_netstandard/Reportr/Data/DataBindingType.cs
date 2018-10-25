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
        /// Template content uses the power of a templating
        /// engine to evaluate the binding value.
        /// </summary>
        [Description("Report Template Content")]
        TemplateContent = 1,

        /// <summary>
        /// Math expressions can be used to calculate a double
        /// value from a string based expression.
        /// </summary>
        [Description("Math Expression")]
        MathExpression = 2
    }
}
