using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarCodeLib
{
    public static class StringExtensions
    {
        /// <summary>
        /// Extension method that strips all the non-numeric characters from a given string.
        /// </summary>
        /// <param name="target">The string for which the numeric value must be retrieved.</param>        
        /// <returns>The numericValue of the given string.</returns>
        public static string ToNumericValue(this String target)
        {
            var expression = new System.Text.RegularExpressions.Regex("\\D");

            return expression.Replace(target, string.Empty);
        }
    }
}
