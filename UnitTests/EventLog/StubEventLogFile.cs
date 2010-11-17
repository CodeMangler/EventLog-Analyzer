using System.Collections.Generic;
using pal.EventLog;

namespace pal.EventLogAnalyzer.UnitTests.EventLog
{
    internal class StubEventLogFile : AbstractEventLogFile
    {
        public StubEventLogFile() : base("Dummy - Non Existent File")
        {
        }

        public override void Parse()
        {
            _records = generateDummyEventLogRecords();
        }

        private List<IEventLogRecord> generateDummyEventLogRecords()
        {
            List<IEventLogRecord> eventLogRecords = new List<IEventLogRecord>();
            eventLogRecords.Add(new StubEventLogRecord());
            return eventLogRecords;
        }

        public override void Dispose()
        {
        }
    }
}