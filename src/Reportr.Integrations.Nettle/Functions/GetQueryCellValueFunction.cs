namespace Reportr.Integrations.Nettle.Functions
{
    using global::Nettle.Compiler;
    using global::Nettle.Functions;
    using Reportr.Data.Querying;
    using System;

    public sealed class GetQueryCellValueFunction : FunctionBase
    {
        public GetQueryCellValueFunction() : base()
        {
            DefineRequiredParameter("Row", "The query row.", typeof(QueryRow));
            DefineRequiredParameter("Column", "The column name.", typeof(string));
        }

        public override string Description => "Gets a cell value from a query row.";

        protected override object GenerateOutput(TemplateContext context, params object[] parameterValues)
        {
            Validate.IsNotNull(context);

            var row = GetParameterValue<QueryRow>("Row", parameterValues);
            var column = GetParameterValue<string>("Column", parameterValues);

            return row[column].Value;
        }
    }
}
