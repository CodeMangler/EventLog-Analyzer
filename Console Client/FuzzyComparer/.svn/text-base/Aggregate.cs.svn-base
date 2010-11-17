using System.Collections.Generic;

namespace pal.EventLogAnalyzer.ConsoleClient.FuzzyComparer
{
    //Understands a group of similar items
    public class Aggregate<AggregateItemType>
    {
        private readonly List<AggregateItemType> _aggregateEntries = new List<AggregateItemType>();
        private readonly IComparisonAlgorithm<string> _comparisonAlgorithm;
        private readonly string _seedAggregationCriteria;
        private readonly double _tolerableErrorMargin;

        public Aggregate(string seedAggregationCriteria, double tolerableErrorMargin)
        {
            _seedAggregationCriteria = seedAggregationCriteria;
            _tolerableErrorMargin = tolerableErrorMargin;
            _comparisonAlgorithm = new FuzzyStringComparisonAlgorithm(_seedAggregationCriteria);
        }

        public string AggregationCriteria
        {
            get { return _seedAggregationCriteria; }
        }

        public List<AggregateItemType> Entries
        {
            get { return _aggregateEntries; }
        }

        public void Hold(AggregateItemType aggregateEntry)
        {
            _aggregateEntries.Add(aggregateEntry);
        }

        public bool IsAcceptable(string aggregationCriteria)
        {
            return _comparisonAlgorithm.DisSimilarityCoefficient(aggregationCriteria) <= _tolerableErrorMargin;
        }

        public override string ToString()
        {
            return AggregationCriteria;
        }
    }
}