using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text.RegularExpressions;

namespace PG.Core.Utility
{
    /// <summary>

    /// Represents a wildcard running on the

    /// <see cref="System.Text.RegularExpressions"/> engine.

    /// </summary>

    public class WildCard : Regex
    {
        /// <summary>

        /// Initializes a wildcard with the given search pattern.

        /// </summary>

        /// <param name="pattern">The wildcard pattern to match.</param>

        public WildCard(string pattern)
            : base(WildCardToRegex(pattern))
        {
        }

        /// <summary>

        /// Initializes a wildcard with the given search pattern and options.

        /// </summary>

        /// <param name="pattern">The wildcard pattern to match.</param>

        /// <param name="options">A combination of one or more

        /// <see cref="System.Text.RegexOptions"/>.</param>

        public WildCard(string pattern, RegexOptions options)
            : base(WildCardToRegex(pattern), options)
        {
        }

        /// <summary>

        /// Converts a wildcard to a regex.

        /// </summary>

        /// <param name="pattern">The wildcard pattern to convert.</param>

        /// <returns>A regex equivalent of the given wildcard.</returns>

        public static string WildCardToRegex(string pattern)
        {
            return "^" + Regex.Escape(pattern).
             Replace("\\*", ".*").
             Replace("\\?", ".") + "$";
        }

        ///Example:
        ///WildCard wildcard = new WildCard("D*1*9", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
        ///wildcard.IsMatch("DHA01009");

    }
}
