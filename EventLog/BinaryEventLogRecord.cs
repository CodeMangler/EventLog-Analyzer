using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace pal.EventLog
{
    public class BinaryEventLogRecord : IEventLogRecord
    {
        protected string _computerName;
        protected IEnumerable<IEventLogRecord> _containingFile;
        protected byte[] _data;
        protected string _message;
        protected Metadata _metadata;
        protected string _sourceName;
        protected byte[] _userSid;

        public EventLogEntryType Type
        {
            get { return (EventLogEntryType) Enum.Parse(typeof (EventLogEntryType), _metadata.EventType.ToString()); }
        }

        public DateTime GeneratedTime
        {
            get { return new DateTime(1970, 1, 1, 0, 0, 0, 0).AddSeconds(_metadata.TimeGenerated); }
        }

        public string Source
        {
            get { return _sourceName; }
        }

        public ushort Category
        {
            get { return _metadata.EventCategory; }
        }

        public int EventId
        {
            get { return (int) _metadata.EventID; }
        }

        public string User
        {
            get { return _userSid.Length > 0 ? userSidHexString() : "N/A"; }
        }

        public string Computer
        {
            get { return _computerName; }
        }

        public IEnumerable<IEventLogRecord> ContainingFile
        {
            get { return _containingFile; }
            set { _containingFile = value; }
        }

        public string Message
        {
            get { return _message; }
        }

        public static IEventLogRecord NULL
        {
            get { return new BinaryEventLogRecord(); }
        }

        public static BinaryEventLogRecord Fetch(BinaryReader reader)
        {
            uint recordLength = reader.ReadUInt32();
            reader.BaseStream.Seek(-4, SeekOrigin.Current);
            byte[] recordBytes = reader.ReadBytes((int) recordLength);
            int nextStartIndexInBuffer = 0;

            BinaryEventLogRecord eventLog = new BinaryEventLogRecord();
            eventLog._metadata = Metadata.Fetch(recordBytes);
            nextStartIndexInBuffer += Globals.MetadataSize;
            eventLog._sourceName = extractString(recordBytes, nextStartIndexInBuffer);
            nextStartIndexInBuffer += eventLog._sourceName.Length*Globals.UnicodeCharSize + Globals.NullCharSize;
            eventLog._computerName = extractString(recordBytes, nextStartIndexInBuffer);
            nextStartIndexInBuffer += eventLog._computerName.Length*Globals.UnicodeCharSize + Globals.NullCharSize;
            eventLog._userSid = new byte[eventLog._metadata.UserSidLength];
            Array.Copy(recordBytes, nextStartIndexInBuffer, eventLog._userSid, 0, eventLog._metadata.UserSidLength);
            nextStartIndexInBuffer += (int) eventLog._metadata.UserSidLength;
            eventLog._message =
                Encoding.Unicode.GetString(recordBytes, nextStartIndexInBuffer,
                                           (int) (eventLog._metadata.DataOffset - eventLog._metadata.StringOffset));
            eventLog._data = new byte[eventLog._metadata.DataLength];
            Array.Copy(recordBytes, eventLog._data, eventLog._metadata.DataLength);
            return eventLog;
        }

        public static bool CanHaveALogRecord(BinaryReader reader)
        {
            return !endOfRecords(reader);
        }

        public bool Matches(LogRecordSearchCriteria searchCriteria)
        {
            if (_sourceName.IndexOf(searchCriteria.Source) != -1
                || _message.IndexOf(searchCriteria.Message) != -1)
                return true;
            return false;
        }

        private string userSidHexString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (byte @byte in _userSid)
            {
                sb.AppendFormat("{0:X}", @byte);
            }
            return "0X" + sb;
        }

        //TODO: Still to add: ProperUser

        public override string ToString()
        {
            return
                string.Format("{0} ~~ {1} ~~ {2} ~~ {3} ~~ {4} ~~ {5} ~~ {6}", Type, Source, Category, EventId, Computer,
                              User, Message);
        }

        private static string extractString(byte[] recordBytes, int startIndex)
        {
            StringBuilder extractedString = new StringBuilder();
            for (int i = startIndex; i < recordBytes.Length; i += 2)
            {
                char character = Convert.ToChar(Encoding.Unicode.GetString(recordBytes, i, 2));
                if (character == '\0')
                    break;
                else
                    extractedString.Append(character);
            }
            return extractedString.ToString();
        }

        private static bool endOfRecords(BinaryReader reader)
        {
            return (reader.BaseStream.Length - reader.BaseStream.Position) <= Globals.FooterSize;
        }
    }
}