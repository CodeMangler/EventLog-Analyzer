using System.IO;
using System.Xml;

namespace pal.EventLog
{
    public class XmlEventLogFile : AbstractEventLogFile
    {
        private XmlDocument _xmlDocument;

        public XmlEventLogFile(string fileName) : base(fileName)
        {
            _xmlDocument = new XmlDocument();
        }

        public override void Parse()
        {
            _xmlDocument.Load(_fileName);

            foreach (XmlNode node in _xmlDocument.DocumentElement.ChildNodes)
            {
                XmlEventLogRecord eventLogRecord = XmlEventLogRecord.Fetch(node);
                eventLogRecord.ContainingFile = this;
                _records.Add(eventLogRecord);
            }
        }

        public override string ToString()
        {
            return Path.GetFileName(_fileName);
        }

        public override void Dispose()
        {
            _xmlDocument = null;
        }
    }
}