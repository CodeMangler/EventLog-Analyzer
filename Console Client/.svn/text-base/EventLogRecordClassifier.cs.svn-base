using System.Collections.Generic;
using pal.EventLog;
using pal.EventLogAnalyzer.ConsoleClient.FuzzyComparer;

namespace pal.EventLogAnalyzer.ConsoleClient
{
    public class EventLogRecordClassifier : ILogRecordClassifier<IEventLogRecord>
    {
        private readonly List<Aggregate<IEventLogRecord>> _aggregates = new List<Aggregate<IEventLogRecord>>();

        public List<Aggregate<IEventLogRecord>> Aggregates
        {
            get { return _aggregates; }
        }

        #region ILogRecordClassifier<IEventLogRecord> Members

        public void Classify(List<IEventLogRecord> records, double toleranceMargin)
        {
            //TODO: Multi-threaded processing? Equi-split records, assign each to independent thread..
            //Finally collate all results
            //Run thru all the resulting aggregations and merge them if multiple threads ended up creating same aggregations

            foreach (IEventLogRecord record in records)
            {
                bool foundExistingAggregate = false;
                foreach (Aggregate<IEventLogRecord> aggregate in _aggregates)
                {
                    if (aggregate.IsAcceptable(record.Message))
                    {
                        aggregate.Hold(record);
                        foundExistingAggregate = true;
                    }
                }
                if (!foundExistingAggregate)
                {
                    Aggregate<IEventLogRecord> newAggregate =
                        new Aggregate<IEventLogRecord>(record.Message, toleranceMargin);
                    newAggregate.Hold(record);
                    _aggregates.Add(newAggregate);
                }
            }
        }

        #endregion
    }
}