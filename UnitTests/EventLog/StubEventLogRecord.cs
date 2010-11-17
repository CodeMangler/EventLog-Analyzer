using System;
using pal.EventLog;

namespace pal.EventLogAnalyzer.UnitTests.EventLog
{
    internal class StubEventLogRecord : BinaryEventLogRecord
    {
        public StubEventLogRecord()
        {
            _computerName = Environment.GetEnvironmentVariable("COMPUTERNAME");
            _message = "Boo Hoo";
            _sourceName = "Stupid source";
            _userSid = new byte[0];
            _metadata = new Metadata();
            _metadata.EventCategory = 0;
            _metadata.EventID = 0;
            _metadata.EventType = 0;
            _metadata.TimeGenerated = 0;
        }
    }
}