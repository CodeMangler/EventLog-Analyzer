using System;
using System.Collections;
using System.Collections.Generic;

namespace pal.EventLog
{
    public class EventLogFileCollection : IEnumerable<IEventLogRecord>, IDisposable
    {
        private List<IEventLogFile> _eventLogFiles = new List<IEventLogFile>();

        #region IEnumerable<BinaryEventLogRecord> Members

        IEnumerator<IEventLogRecord> IEnumerable<IEventLogRecord>.GetEnumerator()
        {
            foreach (IEnumerable<IEventLogRecord> file in _eventLogFiles)
            {
                foreach (IEventLogRecord eventLogRecord in file)
                {
                    yield return eventLogRecord;
                }
            }
        }

        public IEnumerator GetEnumerator()
        {
            return ((IEnumerable<IEventLogRecord>) this).GetEnumerator();
        }

        #endregion

        public void Add(IEventLogFile file)
        {
            _eventLogFiles.Add(file);
        }

        public void Dispose()
        {
            foreach (IEventLogFile eventLogFile in _eventLogFiles)
            {
                eventLogFile.Dispose();
            }
        }
    }
}