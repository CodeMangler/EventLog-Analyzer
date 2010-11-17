using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using NUnit.Framework;

namespace pal.EventLogAnalyzer.UnitTests
{
    [TestFixture]
    public class SpikeTests
    {
        private string stripUnwantedAndSanitize(string @string)
        {
            string noGuid = Regex.Replace(@string, "{.{8}-.{4}-.{4}-.{4}-.{12}}", string.Empty);
            StringBuilder cleanString = new StringBuilder();

            MatchCollection validStrings = Regex.Matches(noGuid, "[A-Za-z\n\r\t]+");
            foreach (Match validString in validStrings)
            {
                cleanString.Append(validString);
            }
            return cleanString.ToString();
        }

        [Test]
        public void GenerateFileNamesUsingYield()
        {
            IEnumerator<string> nameGenerator = getNextFileName();
            for (int i = 0; i < 10; i++)
            {
                nameGenerator.MoveNext();
                Console.Out.WriteLine("getNextFileName() = {0}", nameGenerator.Current);
            }
        }

        private IEnumerator<string> getNextFileName()
        {
            int i = 0;
            while(i<Int32.MaxValue)
                yield return ("Group " + i++);
        }

        [Test]
        public void StringSanitizerRegExp()
        {
            Regex sanitizer =
                new Regex("[A-Za-z\n\r\t]+", RegexOptions.Singleline | RegexOptions.Compiled | RegexOptions.IgnoreCase);
            foreach (Match match in sanitizer.Matches("{2BF96958-4BD7-4b48-AC8A-CBA47ACA2957}"))
            {
                Console.Out.WriteLine(match.Value);
            }
            sanitizer =
                new Regex("({.{8}-.{4}-.{4}-.{4}-.{12}})",
                          RegexOptions.Singleline | RegexOptions.Compiled | RegexOptions.IgnoreCase);
            foreach (Match match in sanitizer.Matches("{2BF96958-4BD7-4b48-AC8A-CBA47ACA2957}"))
            {
                Console.Out.WriteLine(match.Value);
            }

            string sampleInput = "blah blah, booo | hoo \\ some\\file\\somewhere ... {} |38274982|\n"
                                 +
                                 "{2BF96958-4BD7-4b48-AC8A-CBA47ACA2957}{2BF96958-4BD7-4b48-AC8A-CBA47ACA2957}kdjfhkshfkjshrwe"
                                 + "woo hoo :)";
            Console.Out.WriteLine("sampleInput = {0}", sampleInput);

            Console.Out.WriteLine(stripUnwantedAndSanitize(sampleInput));
        }
    }
}