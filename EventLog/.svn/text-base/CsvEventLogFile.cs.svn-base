using System.IO;

namespace pal.EventLog
{
    public class CsvEventLogFile : AbstractEventLogFile
    {
        private readonly StreamReader _fileReader;

        public CsvEventLogFile(string fileName) : base(fileName)
        {
            _fileReader = new StreamReader(_fileName);
        }

        public override void Parse()
        {
            while (!_fileReader.EndOfStream)
            {
                string line = _fileReader.ReadLine();
                if (!isComment(line))
                {
                    CsvEventLogRecord eventLogRecord = CsvEventLogRecord.Fetch(line);
                    eventLogRecord.ContainingFile = this;
                    _records.Add(eventLogRecord);
                }
            }
            _fileReader.Close();
        }

        private bool isComment(string line)
        {
            return line.Trim().StartsWith("#");
        }

        public override string ToString()
        {
            return Path.GetFileName(_fileName);
        }

        public override void Dispose()
        {
            _fileReader.Dispose();
        }
    }
}