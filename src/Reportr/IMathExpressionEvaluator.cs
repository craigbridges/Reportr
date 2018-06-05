namespace Reportr
{
    /// <summary>
    /// Defines a contract for a service that evaluates math expressions
    /// </summary>
    public interface IMathExpressionEvaluator
    {
        /// <summary>
        /// Evaluates the math expression specified
        /// </summary>
        /// <param name="expression">The math expression</param>
        /// <returns>The result</returns>
        object Evaluate
        (
            string expression
        );
    }
}
