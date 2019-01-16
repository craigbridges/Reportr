namespace Reportr.Components.Collections
{
    using Reportr.Data;
    using System;
    
    /// <summary>
    /// Represents a rule for a table row that specifies the importance
    /// </summary>
    public class TableRowImportanceRule
    {
        /// <summary>
        /// Constructs the rule with the rule configuration
        /// </summary>
        /// <param name="columnName">The column name</param>
        /// <param name="matchValue">The data match value</param>
        /// <param name="compareOperator">The match compare operator</param>
        /// <param name="importanceOnMatch">The importance flag for matches</param>
        public TableRowImportanceRule
            (
                string columnName,
                object matchValue,
                DataCompareOperator compareOperator,
                DataImportance importanceOnMatch
            )
        {
            Validate.IsNotEmpty(columnName);

            this.ColumnName = columnName;
            this.MatchValue = matchValue;
            this.CompareOperator = compareOperator;
            this.ImportanceOnMatch = importanceOnMatch;
        }

        /// <summary>
        /// Gets the name table column used for the data binding
        /// </summary>
        public string ColumnName { get; private set; }

        /// <summary>
        /// Gets the value to match the rule
        /// </summary>
        public object MatchValue { get; private set; }

        /// <summary>
        /// Gets the data compare operator used to match the value
        /// </summary>
        public DataCompareOperator CompareOperator { get; private set; }

        /// <summary>
        /// Gets the importance to set if the rule matches
        /// </summary>
        public DataImportance ImportanceOnMatch { get; private set; }

        /// <summary>
        /// Determines if the value specified matches the rule
        /// </summary>
        /// <param name="value">The value to check</param>
        /// <returns>True, if the rule matches; otherwise false</returns>
        public bool Matches(object value)
        {
            if (value == null)
            {
                if (this.MatchValue == null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                var comparer = new StringDataComparer();
                var matchValueString = this.MatchValue?.ToString();
                var suppliedValueString = value.ToString();
                var @operator = this.CompareOperator;

                return comparer.IsMatch
                (
                    suppliedValueString,
                    matchValueString,
                    @operator
                );
            }
        }
    }
}
