using System.Collections.Generic;
using pal.EventLog;
using pal.EventLogAnalyzer.ConsoleClient.FuzzyComparer;

namespace pal.EventLogAnalyzer.ConsoleClient.Reporting
{
    internal abstract class AbstractSummaryWriter
    {
        protected readonly ICollection<Aggregate<IEventLogRecord>> _aggregates;
        protected readonly FileNameGenerator _fileNameGenerator;

        public AbstractSummaryWriter(ICollection<Aggregate<IEventLogRecord>> aggregates,
                                     FileNameGenerator fileNameGenerator)
        {
            _aggregates = aggregates;
            _fileNameGenerator = fileNameGenerator;
        }

        public abstract void WriteIn(string folder);
    }
}