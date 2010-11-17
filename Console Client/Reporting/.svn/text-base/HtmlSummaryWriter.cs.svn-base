using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using pal.EventLog;
using pal.EventLogAnalyzer.ConsoleClient.FuzzyComparer;

namespace pal.EventLogAnalyzer.ConsoleClient.Reporting
{
    internal class HtmlSummaryWriter : AbstractSummaryWriter
    {
        public HtmlSummaryWriter(ICollection<Aggregate<IEventLogRecord>> aggregates, FileNameGenerator fileNameGenerator)
            : base(aggregates, fileNameGenerator)
        {
        }

        public override void WriteIn(string folder)
        {
            StreamWriter summary = new StreamWriter(File.Open(folder + "\\Summary.htm", FileMode.Create));
            StringBuilder summaryHtml = new StringBuilder();
            summaryHtml.Append("<html>");
            summaryHtml.Append("<head>");
            summaryHtml.Append("<style>");
            summaryHtml.Append(".tableStyle {border: none; }").Append(Environment.NewLine);
            summaryHtml.Append(".rowStyle {border:none; }").Append(Environment.NewLine);
            summaryHtml.Append(".cellStyle {border-width: 1px; border-style: solid; }").Append(Environment.NewLine);
            summaryHtml.Append(".headerStyle {border-width: 1px; border-style: solid; }").Append(Environment.NewLine);
            summaryHtml.Append("</style>");
            summaryHtml.Append("</head>");
            summaryHtml.Append("<body>");
            summaryHtml.Append("<h2>Analysis Summary</h2><br>Generated on: ").Append(DateTime.Now);
            summaryHtml.Append("<br>No of groups: ").Append(_aggregates.Count).Append("<br>");
            summaryHtml.Append("<h3>Grouping details</h3>");
            summaryHtml.Append(
                "<table id='summaryTable' class='tableStyle'><tr><th class='headerStyle'>Number of occurences</th><th class='headerStyle'>Message</th><th class='headerStyle'>Details</th></tr>");

            double totalEntries = 0;
            DateTime minTime = DateTime.MaxValue, maxTime = DateTime.MinValue;

            foreach (Aggregate<IEventLogRecord> aggregate in _aggregates)
            {
                string groupFileName = _fileNameGenerator.NextName;
                summaryHtml.Append("<tr class='rowStyle'><td class='cellStyle'>").Append(aggregate.Entries.Count)
                    .Append("</td><td class='cellStyle'><pre>").Append(aggregate.AggregationCriteria)
                    .Append("</pre></td><td class='cellStyle'>").Append("<a href='./").Append(groupFileName)
                    .Append("'>").Append(groupFileName).Append("</a></td></tr>");

                totalEntries += aggregate.Entries.Count;

                foreach (IEventLogRecord eventLogRecord in aggregate.Entries)
                {
                    if (eventLogRecord.GeneratedTime.CompareTo(minTime) < 0) minTime = eventLogRecord.GeneratedTime;
                    if (eventLogRecord.GeneratedTime.CompareTo(maxTime) > 0) maxTime = eventLogRecord.GeneratedTime;
                }
            }
            summaryHtml.Append("</table>");

            summaryHtml.Append("<br><br><b>Total number of records processed:</b> <i>").Append(totalEntries).Append("</i><br>");
            summaryHtml.Append("<b>Time range:</b> [From: <i>").Append(minTime).Append("</i> Till:<i>").Append(maxTime).Append("</i>]<br>");

            summaryHtml.Append("<br><br>");
            summaryHtml.Append("</body>");
            summaryHtml.Append("</html>");

            summary.Write(summaryHtml);
            summary.Close();
        }
    }
}