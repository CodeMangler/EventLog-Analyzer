using System;
using System.Collections.Generic;
using System.IO;
using pal.EventLog;
using pal.EventLogAnalyzer.ConsoleClient.FuzzyComparer;

namespace pal.EventLogAnalyzer.ConsoleClient.Reporting
{
    internal class TextSummaryWriter : AbstractSummaryWriter
    {
        public TextSummaryWriter(ICollection<Aggregate<IEventLogRecord>> aggregates, FileNameGenerator fileNameGenerator)
            : base(aggregates, fileNameGenerator)
        {
        }

        public override void WriteIn(string folder)
        {
            StreamWriter summary = new StreamWriter(File.Open(folder + "\\Summary.txt", FileMode.Create));
            summary.WriteLine("Analysis summary: {0}", DateTime.Now);
            summary.WriteLine("No of groups: {0}", _aggregates.Count);
            summary.WriteLine();
            summary.WriteLine("Grouping details");
            summary.WriteLine("----------------------------------------------------------------");

            foreach (Aggregate<IEventLogRecord> aggregate in _aggregates)
            {
                summary.WriteLine("Following message occurs {0} times", aggregate.Entries.Count);
                summary.WriteLine();
                summary.WriteLine(aggregate.AggregationCriteria);
                summary.WriteLine("############################################################################");
            }
            summary.Close();
        }
    }
}