namespace Reportr.Integrations.NCalc
{
    using global::NCalc;
    
    /// <summary>
    /// Represents an NCalc implementation of the math expression evaluator
    /// </summary>
    public sealed class NCalcEvaluator : IMathExpressionEvaluator
    {
        /// <summary>
        /// Evaluates the math expression specified
        /// </summary>
        /// <param name="expression">The math expression</param>
        /// <returns>The result</returns>
        public object Evaluate
            (
                string expression
            )
        {
            var ncalcExpression = new Expression
            (
                expression,
                EvaluateOptions.RoundAwayFromZero
            );

            return ncalcExpression.Evaluate();
        }
    }
}
