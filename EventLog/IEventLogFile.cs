using System;
using System.Collections;
using System.Collections.Generic;

namespace pal.EventLog
{
    public interface IEventLogFile : IEnumerable<IEventLogRecord>, IDisposable
    {
        int RecordCount { get; }
        IEventLogRecord this[int recordNo] { get; }
        void Parse();
        ArrayList Find(LogRecordSearchCriteria searchCriteria);
    }
}