using System;
using System.IO;

namespace pal.EventLog
{
    public class BinaryEventLogFile : AbstractEventLogFile
    {
        private Footer _footer;
        private Header _header;
        private BinaryReader _logReader;

        public BinaryEventLogFile(string fileName) : base(fileName)
        {
        }

        public override void Parse()
        {
            FileStream logStream = new FileStream(_fileName, FileMode.Open, FileAccess.Read);
            _logReader = new BinaryReader(logStream);

            _header = Header.Fetch(_logReader);
            _header.Verify();

            while (BinaryEventLogRecord.CanHaveALogRecord(_logReader))
            {
                BinaryEventLogRecord eventLogRecord = BinaryEventLogRecord.Fetch(_logReader);
                eventLogRecord.ContainingFile = this;
                _records.Add(eventLogRecord);
            }

            _footer = Footer.Fetch(_logReader);
            _footer.Verify();

            logStream.Close();
            _logReader.Close();
        }

        public override string ToString()
        {
            return Path.GetFileName(_fileName);
        }

        public override void Dispose()
        {
            _logReader = null;
        }
    }
}