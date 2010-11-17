using System;
using System.Text;
using System.Text.RegularExpressions;

namespace pal.EventLogAnalyzer.ConsoleClient.FuzzyComparer
{
    public class FuzzyStringComparisonAlgorithm : IComparisonAlgorithm<string>
    {
        private const string ALLOWED_CHARACTERS = @"[A-Za-z\n\r\t]+";
        private const string GUID_PATTERN = ".{8}-.{4}-.{4}-.{4}-.{12}";
        private readonly Regex _onlyAllowedCharacters = new Regex(ALLOWED_CHARACTERS, RegexOptions.Singleline | RegexOptions.Compiled | RegexOptions.IgnoreCase);
        private readonly Regex _guidMatcher = new Regex(GUID_PATTERN, RegexOptions.Compiled);
        private readonly double _leftHash = 0;

        public FuzzyStringComparisonAlgorithm(string lhs)
        {
            _leftHash = fuzzyHash(lhs);
        }

        #region IComparisonAlgorithm<string> Members

        public double DisSimilarityCoefficient(string rhs)
        {
            return Math.Abs(_leftHash - fuzzyHash(rhs));
        }

        #endregion

        private double fuzzyHash(string @string)
        {
            double hashCode = 0;
            double positionalWeight = 0;
            string cleanString = stripUnwantedAndSanitize(@string);
            foreach (char c in cleanString)
            {
                if (char.IsControl(c))
                    //To compute for every line.. Makes the computation fuzzier.. Reduces mismatches due to same/similar strings on different lines
                    positionalWeight = 0;
                else
                    hashCode += c * positionalWeight++; //Keep it case insensitive
            }
            return hashCode;
        }

        private string stripUnwantedAndSanitize(string @string)
        {
            string withoutGuid = _guidMatcher.Replace(@string, string.Empty);

            StringBuilder cleanString = new StringBuilder();
            MatchCollection validStrings = _onlyAllowedCharacters.Matches(withoutGuid);
            foreach (Match validString in validStrings)
            {
                cleanString.Append(validString);
            }
            return cleanString.ToString();
        }
    }
}