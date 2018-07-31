namespace Reportr
{
    /// <summary>
    /// Provides various methods for common math calculations
    /// </summary>
    public static class Calculate
    {
        /// <summary>
        /// Calculates the greatest common denominator (GCD) for two numbers
        /// </summary>
        /// <param name="a">The first number</param>
        /// <param name="b">The second number</param>
        /// <returns>The greatest common denominator</returns>
        /// <remarks>
        /// Example usage:
        /// 
        /// var gcd = GCD(A, B);
        /// 
        /// String.Format("{0}:{1}", A / gcd, B / gcd)
        /// 
        /// The code above would create a string ratio representation of GCD.
        /// </remarks>
        public static int GCD
            (
                int a,
                int b
            )
        {
            while (a != 0 && b != 0)
            {
                if (a > b)
                {
                    a %= b;
                }
                else
                {
                    b %= a;
                }
            }

            return (a == 0) ? b : a;
        }
    }
}
