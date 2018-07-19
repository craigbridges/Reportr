namespace System
{
    using Reportr;

    /// <summary>
    /// Various extension methods for the string class
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Spacifies a string value by adding a separator (usually space) between words
        /// </summary>
        /// <param name="value">The string value to spacify</param>
        /// <param name="seperator">The separator value to use, default is a space</param>
        /// <returns>The spacified string value</returns>
        public static string Spacify
            (
                this string value,
                string seperator = " "
            )
        {
            Validate.IsNotEmpty(value);

            if (String.IsNullOrEmpty(value))
            {
                return value;
            }
            else
            {
                var result = String.Empty;
                var previousChar = '\0';

                foreach (var currentChar in value)
                {
                    if (previousChar != Char.MinValue && Char.IsLetter(currentChar))
                    {
                        if (Char.IsNumber(previousChar) & !Char.IsNumber(currentChar)
                            || Char.IsUpper(currentChar) & Char.IsLower(previousChar))
                        {
                            result += seperator + Convert.ToString(currentChar);
                        }
                        else
                        {
                            result += Convert.ToString(currentChar);
                        }
                    }
                    else
                    {
                        result += Convert.ToString(currentChar);
                    }

                    previousChar = currentChar;
                }

                if (false == String.IsNullOrEmpty(result))
                {
                    result = result.Replace
                    (
                        "_",
                        seperator
                    );

                    result = result.Replace
                    (
                        "  ",
                        " "
                    );
                }

                return result;
            }
        }
    }
}
