namespace Reportr
{
    using System;
    using System.Runtime.CompilerServices;

    /// <summary>
    /// Provides various static parameter validation methods
    /// </summary>
    internal static class Validate
    {
        /// <summary>
        /// Checks that an object value is not null
        /// </summary>
        /// <param name="o">The value to check</param>
        /// <param name="memberName">The member name</param>
        /// <param name="sourceLineNumber">The source line number</param>
        public static void IsNotNull
            (
                object o,
                [CallerMemberName] string memberName = "",
                [CallerLineNumber] int sourceLineNumber = 0
            )
        {
            if (o == null)
            {
                var message = "The parameter {0} for {1} at line {2} was null.";

                throw new ArgumentNullException
                (
                    String.Format
                    (
                        message,
                        nameof(o),
                        memberName,
                        sourceLineNumber
                    )
                );
            }
        }

        /// <summary>
        /// Ensures a string has a value (i.e. it is not null or empty)
        /// </summary>
        /// <param name="input">The input string to validate</param>
        /// <param name="memberName">The member name</param>
        /// <param name="sourceLineNumber">The source line number</param>
        public static void IsNotEmpty
            (
                string input,
                [CallerMemberName] string memberName = "",
                [CallerLineNumber] int sourceLineNumber = 0
            )
        {
            if (String.IsNullOrEmpty(input))
            {
                var message = "The parameter {0} for {1} at line {2} was null or empty.";

                throw new ArgumentNullException
                (
                    String.Format
                    (
                        message,
                        nameof(input),
                        memberName,
                        sourceLineNumber
                    )
                );
            }
        }
    }
}
