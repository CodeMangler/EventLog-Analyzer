using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace pal.EventLog
{
    public interface IEventLogRecord
    {
        EventLogEntryType Type { get; }

        DateTime GeneratedTime { get; }

        string Source { get; }

        ushort Category { get; }

        int EventId { get; }

        string User { get; }

        string Computer { get; }

        string Message { get; }

        IEnumerable<IEventLogRecord> ContainingFile { get; set; }

        bool Matches(LogRecordSearchCriteria searchCriteria);
    }
}