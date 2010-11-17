using NUnit.Framework;
using pal.EventLogAnalyzer.ConsoleClient.FuzzyComparer;

namespace pal.EventLogAnalyzer.UnitTests
{
    [TestFixture]
    public class FuzzyStringComparisonAlgorithmTest
    {
        [Test]
        public void ShouldIgnoreGuids()
        {
            string lhs =
                "{b736467c-b1ff-4bbd-9654-a5312f1ba846\0x0 and some text and {47484565-22b5-4e72-a365-b0bdda187465}";
            string rhs1 =
                "{b736467c-b1ff-4bbd-9654-a5312f1ba846} and some text and {a3e8ee65-3c13-4c48-bc94-253da3fac798}";
            string rhs2 =
                "{b736467c-b1ff-4bbd-9654-a5312f1ba846} and some different text and {a3e8ee65-3c13-4c48-bc94-253da3fac798}";
            FuzzyStringComparisonAlgorithm comparisonAlgorithm = new FuzzyStringComparisonAlgorithm(lhs);
            Assert.AreEqual(0, comparisonAlgorithm.DisSimilarityCoefficient(rhs1));
            Assert.AreNotEqual(0, comparisonAlgorithm.DisSimilarityCoefficient(rhs2));
        }
    }
}