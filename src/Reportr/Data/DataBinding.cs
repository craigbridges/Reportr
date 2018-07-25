namespace Reportr.Data
{
    using Reportr.Data.Querying;
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
        /// <param name="expression">The expression</param>
        public DataBinding
            (
                string expression
            )

            : this(DataBindingType.QueryPath, expression)
        { }

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
                case DataBindingType.TemplateContent:
                    return ResolveTemplateContent(row);

                case DataBindingType.MathExpression:
                    return ResolveMathExpression(row);

                default:
                    return ResolveQueryPath(row);
            }
        }

        /// <summary>
        /// Resolves the data binding value using a query row
        /// </summary>
        /// <typeparam name="T">The value type to resolve</typeparam>
        /// <param name="row">The query row</param>
        /// <returns>The resolved value as the type specified</returns>
        public T Resolve<T>
            (
                QueryRow row
            )
        {
            var rawValue = Resolve(row);

            return new ObjectConverter<T>().Convert
            (
                rawValue
            );
        }

        /// <summary>
        /// Resolves the query path for a query row specified
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

            var columnName = pathInfo.ColumnName;
            var currentValue = (object)row[columnName].Value;
            var currentPath = columnName;
            var currentType = currentValue.GetType();

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
        /// Resolves the template content for a query row
        /// </summary>
        /// <param name="row">The query row</param>
        /// <returns>The resolved content</returns>
        private string ResolveTemplateContent
            (
                QueryRow row
            )
        {
            var template = this.Expression;
            var renderer = ReportrEngine.TemplateRenderer;

            if (renderer == null)
            {
                throw new InvalidOperationException
                (
                    "The template renderer has not been configured."
                );
            }

            return renderer.Render
            (
                template,
                row
            );
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
            var expression = ResolveTemplateContent
            (
                row
            );

            var evaluator = ReportrEngine.MathEvaluator;

            if (evaluator == null)
            {
                throw new InvalidOperationException
                (
                    "The math expression evaluator has not been configured."
                );
            }

            return evaluator.Evaluate
            (
                expression
            );
        }
    }
}
