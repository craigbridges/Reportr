namespace Reportr.Integrations.Nettle.Functions
{
    using global::Nettle.Compiler;
    using global::Nettle.Functions;
    using System;

    public sealed class AsRatioFunction : FunctionBase
    {
        public AsRatioFunction() : base()
        {
            DefineRequiredParameter
            (
                "FirstNumber",
                "The first number.",
                typeof(int)
            );

            DefineRequiredParameter
            (
                "SecondNumber",
                "The second number.",
                typeof(string)
            );
        }

        public override string Description
        {
            get
            {
                return "Represents two numbers as a ratio (e.g. 1:10, 2:3 etc).";
            }
        }

        protected override object GenerateOutput
            (
                TemplateContext context,
                params object[] parameterValues
            )
        {
            Validate.IsNotNull(context);

            var firstNumber = GetParameterValue<int>
            (
                "FirstNumber",
                parameterValues
            );

            var secondNumber = GetParameterValue<int>
            (
                "SecondNumber",
                parameterValues
            );

            var gcd = Calculate.GCD
            (
                firstNumber,
                secondNumber
            );

            var ratio = String.Format
            (
                "{0}:{1}",
                firstNumber / gcd,
                secondNumber / gcd
            );

            return ratio;
        }
    }
}
