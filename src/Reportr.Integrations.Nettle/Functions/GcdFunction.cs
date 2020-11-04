namespace Reportr.Integrations.Nettle.Functions
{
    using global::Nettle.Compiler;
    using global::Nettle.Functions;
    using System;

    public sealed class GcdFunction : FunctionBase
    {
        public GcdFunction() : base()
        {
            DefineRequiredParameter("FirstNumber", "The first number.", typeof(int));
            DefineRequiredParameter("SecondNumber", "The second number.", typeof(string));
        }

        public override string Description
        {
            get
            {
                return "Calculates the greatest common denominator (GCD) for two numbers.";
            }
        }

        protected override object GenerateOutput(TemplateContext context, params object[] parameterValues)
        {
            Validate.IsNotNull(context);

            var firstNumber = GetParameterValue<int>("FirstNumber", parameterValues);
            var secondNumber = GetParameterValue<int>("SecondNumber", parameterValues);

            return Calculate.GCD(firstNumber, secondNumber);
        }
    }
}
