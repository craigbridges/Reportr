namespace Reportr.Data
{
    using NCalc;
    using Reportr.Data.Querying;
    using Reportr.Nettle;
    using System;
    using System.Linq;

    /// <summary>
    /// Represents a single data binding
    /// </summary>
    public sealed class DataBinding
    {
        /// <summary>
        /// Constructs the data binding with the details
        /// </summary>
        /// <param name="bindingType">The binding type</param>
        /// <param name="expression">The expression</param>
        public DataBinding
            (
                DataBindingType bindingType,
                string expression
            )
        {
            Validate.IsNotEmpty(expression);

            this.BindingType = bindingType;
            this.Expression = expression;
        }

        /// <summary>
        /// Gets the data binding type
        /// </summary>
        public DataBindingType BindingType { get; private set; }

        /// <summary>
        /// Gets the binding expression
        /// </summary>
        /// <remarks>
        /// The expression syntax varies depending on the binding 
        /// type, but the result is always returned as an object.
        /// </remarks>
        public string Expression { get; private set; }

        /// <summary>
        /// Resolves the data binding value using a query row
        /// </summary>
        /// <param name="row">The query row</param>
        /// <returns>The resolved value</returns>
        public object Resolve
            (
                QueryRow row
            )
        {
            Validate.IsNotNull(row);

            switch (this.BindingType)
            {
                case DataBindingType.NettleExpression:
                    return ResolveNettleExpression(row);

                case DataBindingType.MathExpression:
                    return ResolveMathExpression(row);

                default:
                    return ResolveQueryPath(row);
            }
        }

        /// <summary>
        /// Resolves the query path for a query row
        /// </summary>
        /// <param name="row">The query row</param>
        /// <returns>The resolved value</returns>
        private object ResolveQueryPath
            (
                QueryRow row
            )
        {
            var pathInfo = new QueryPathInfo
            (
                this.Expression
            );

            var currentValue = (object)row[pathInfo.ColumnName];
            var currentPath = pathInfo.ColumnName;

            foreach (var propertyName in pathInfo.NestedProperties)
            {
                if (currentValue == null)
                {
                    throw new NullReferenceException
                    (
                        String.Format
                        (
                            "The path '{0}' has a null reference.",
                            currentPath
                        )
                    );
                }

                var currentType = currentValue.GetType();

                var nextPropertyFound = currentType.GetProperties().Any
                (
                    p => p.Name == propertyName
                );
                
                if (false == nextPropertyFound)
                {
                    throw new MissingFieldException
                    (
                        String.Format
                        (
                            "The path '{0}' does not contain a property named '{1}'",
                            currentPath,
                            propertyName
                        )
                    );
                }

                var nextProperty = currentType.GetProperty
                (
                    propertyName
                );

                currentValue = nextProperty.GetValue
                (
                    currentValue
                );

                currentPath += "." + propertyName;
            }

            return currentValue;
        }

        /// <summary>
        /// Resolves the Nettle expression for a query row
        /// </summary>
        /// <param name="row">The query row</param>
        /// <returns>The resolved value</returns>
        private string ResolveNettleExpression
            (
                QueryRow row
            )
        {
            var generator = new NettleCompilerGenerator();
            var compiler = generator.GenerateCompiler();

            var template = compiler.Compile
            (
                this.Expression
            );

            return template(row);
        }

        /// <summary>
        /// Resolves the math expression for a query row
        /// </summary>
        /// <param name="row">The query row</param>
        /// <returns>The resolved value</returns>
        private object ResolveMathExpression
            (
                QueryRow row
            )
        {
            var resolvedExpression = ResolveNettleExpression
            (
                row
            );
            
            var ncalcExpression = new Expression
            (
                resolvedExpression
            );

            return ncalcExpression.Evaluate();
        }
    }
}
