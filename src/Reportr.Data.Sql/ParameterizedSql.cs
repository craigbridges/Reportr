namespace Reportr.Data.Sql
{
    using Reportr.Filtering;
    
    /// <summary>
    /// Represents a single parameterized SQL statement
    /// </summary>
    public class ParameterizedSql
    {
        /// <summary>
        /// Constructs the parameterized SQL with the details
        /// </summary>
        /// <param name="statement">The SQL statement</param>
        /// <param name="parameterValues">The parameter values</param>
        public ParameterizedSql
            (
                string statement,
                params ParameterValue[] parameterValues
            )
        {
            Validate.IsNotEmpty(statement);

            this.Statement = statement;
            this.ParameterValues = parameterValues;
        }

        /// <summary>
        /// Gets the SQL statement
        /// </summary>
        public string Statement { get; private set; }

        /// <summary>
        /// Gets the parameter values to use with the SQL statement
        /// </summary>
        public ParameterValue[] ParameterValues { get; private set; }
    }
}
