namespace Reportr.Integrations.Nettle.Functions
{
    using global::Nettle.Compiler;
    using global::Nettle.Functions;
    using Reportr.Data.Querying;
    using System;
    using System.Linq;
    using System.Text;

    public sealed class ConcatenateQueryCellsFunction : FunctionBase
    {
        public ConcatenateQueryCellsFunction() : base()
        {
            DefineRequiredParameter
            (
                "Row",
                "The query row.",
                typeof(QueryRow)
            );

            DefineRequiredParameter
            (
                "Join",
                "The cell value joining string.",
                typeof(string)
            );
        }

        public override string Description
        {
            get
            {
                return "Concatenates multiple cell values from a query row.";
            }
        }

        protected override object GenerateOutput
            (
                TemplateContext context,
                params object[] parameterValues
            )
        {
            Validate.IsNotNull(context);

            if (parameterValues.Length < 3)
            {
                throw new ArgumentException
                (
                    "At least one column name must be specified."
                );
            }

            var row = GetParameterValue<QueryRow>
            (
                "Row",
                parameterValues
            );

            var join = GetParameterValue<string>
            (
                "Join",
                parameterValues
            );

            var columns = parameterValues.Skip(2);
            var builder = new StringBuilder();

            foreach (string column in columns)
            {
                var nextValue = row[column].Value;

                if (builder.Length > 0)
                {
                    builder.Append(join);
                }

                if (nextValue != null)
                {
                    builder.Append
                    (
                        nextValue.ToString()
                    );
                }
            }

            return builder.ToString();
        }
    }
}
