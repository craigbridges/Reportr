namespace Reportr.Data
{
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
    }
}
