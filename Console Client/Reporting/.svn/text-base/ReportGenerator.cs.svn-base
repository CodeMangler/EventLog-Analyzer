using System;
using System.Collections.Generic;
using System.IO;
using pal.EventLog;
using pal.EventLogAnalyzer.ConsoleClient.FuzzyComparer;

namespace pal.EventLogAnalyzer.ConsoleClient.Reporting
{
    internal class ReportGenerator
    {
        private const string FILE_PREFIX = "Group";
        private const string FILE_EXTENSION = ".csv";
        private readonly string _folder;
        private readonly object _reportFields;
        private readonly FileNameGenerator _fileNameGenerator;

        public ReportGenerator(string folder, object reportFields)
        {
            _folder = folder;
            _reportFields = reportFields;
            _fileNameGenerator = new FileNameGenerator(FILE_PREFIX, FILE_EXTENSION);

            if (!Directory.Exists(_folder))
                throw new Exception("Need an existing directory");
        }

        public void WriteReportsFor(List<Aggregate<IEventLogRecord>> aggregates)
        {
            aggregates.Sort(DescendingSortByNumberOfEntries);
            writeSummary(aggregates);
            writeDetailedReport(aggregates);
        }

        private void writeDetailedReport(List<Aggregate<IEventLogRecord>> aggregates)
        {
            aggregates.ForEach(delegate(Aggregate<IEventLogRecord> aggregate) { writeAggregateDetails(aggregate); });
        }

        private void writeAggregateDetails(Aggregate<IEventLogRecord> aggregate)
        {
            StreamWriter details =
                new StreamWriter(File.Open(_folder + "\\" + _fileNameGenerator.NextName, FileMode.Create));
            foreach (IEventLogRecord record in aggregate.Entries)
            {
                details.WriteLine(csv(record));
            }
            details.Close();
        }

        private string csv(IEventLogRecord record)
        {
            //ComputerName, [Logfile], [CategoryString], Type, User, [EventCode], SourceName, EventIdentifier, Message, [RecordNumber], TimeGenerated
            return string.Format("{0},{1},{2},{3},{4},{5},{6},\"{7}\",{8},{9}",
                                 record.Computer, string.Empty, record.Type, record.User, string.Empty, record.Source,
                                 record.EventId,
                                 sanitizeString(record), string.Empty, record.GeneratedTime);
        }

        private string sanitizeString(IEventLogRecord record)
        {
            return record.Message.Replace("\"", "\"\"");
        }

        private int DescendingSortByNumberOfEntries(Aggregate<IEventLogRecord> x, Aggregate<IEventLogRecord> y)
        {
            return y.Entries.Count.CompareTo(x.Entries.Count);
        }

        private void writeSummary(ICollection<Aggregate<IEventLogRecord>> aggregates)
        {
            AbstractSummaryWriter htmlSummaryWriter = new HtmlSummaryWriter(aggregates, new FileNameGenerator(FILE_PREFIX, FILE_EXTENSION));
            htmlSummaryWriter.WriteIn(_folder);
        }
    }
}