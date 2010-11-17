using System.Collections;
using System.Collections.Generic;

namespace pal.EventLog
{
    public abstract class AbstractEventLogFile : IEventLogFile
    {
        protected readonly string _fileName;
        protected List<IEventLogRecord> _records = new List<IEventLogRecord>();

        public AbstractEventLogFile(string fileName)
        {
            _fileName = fileName;
        }

        #region IEventLogFile Members

        public int RecordCount
        {
            get { return _records.Count; }
        }

        public IEventLogRecord this[int recordNo]
        {
            get { return _records[recordNo]; }
        }

        IEnumerator<IEventLogRecord> IEnumerable<IEventLogRecord>.GetEnumerator()
        {
            for (int i = 0; i < RecordCount; i++)
            {
                yield return this[i];
            }
        }

        public IEnumerator GetEnumerator()
        {
            return ((IEnumerable<IEventLogRecord>) this).GetEnumerator();
        }

        public abstract void Parse();

        public ArrayList Find(LogRecordSearchCriteria searchCriteria)
        {
            ArrayList searchResults = new ArrayList();
            foreach (IEventLogRecord record in _records)
            {
                if (record.Matches(searchCriteria))
                    searchResults.Add(record);
            }
            return searchResults;
        }

        public abstract void Dispose();

        #endregion
    }
}