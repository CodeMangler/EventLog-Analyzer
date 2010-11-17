using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Xml;

namespace pal.EventLog
{
    public class XmlEventLogRecord : IEventLogRecord
    {
        private const string DATETIME_MATCHER =
            @"(?<year>\d{4})(?<month>\d{2})(?<day>\d{2})(?<hour>\d{2})(?<minute>\d{2})(?<second>\d{2})\.(?<millisecond>\d*)";

        private const string COMPUTER_XPATH = "Computer";
        private const string EVENT_TYPE_XPATH = "Type";
        private const string USER_XPATH = "User";
        private const string EVENT_SOURCE_XPATH = "Source";
        private const string EVENT_ID_XPATH = "EventId";
        private const string EVENT_MESSAGE_XPATH = "Message";
        private const string GENERATED_TIME_XPATH = "TimeGenerated";

        private static readonly Regex _dateTimeMatcher = new Regex(DATETIME_MATCHER, RegexOptions.Compiled);
        private ushort _category;
        private string _computer;
        private IEnumerable<IEventLogRecord> _containingFile;
        private int _eventId;
        private DateTime _generatedTime;
        private string _message;
        private string _source;
        private EventLogEntryType _type;
        private string _user;

        #region IEventLogRecord Members

        public EventLogEntryType Type
        {
            get { return _type; }
        }

        public DateTime GeneratedTime
        {
            get { return _generatedTime; }
        }

        public string Source
        {
            get { return _source; }
        }

        public ushort Category
        {
            get { return _category; }
        }

        public int EventId
        {
            get { return _eventId; }
        }

        public string User
        {
            get { return _user; }
        }

        public string Computer
        {
            get { return _computer; }
        }

        public string Message
        {
            get { return _message; }
        }

        public IEnumerable<IEventLogRecord> ContainingFile
        {
            get { return _containingFile; }
            set { _containingFile = value; }
        }

        public bool Matches(LogRecordSearchCriteria searchCriteria)
        {
            if (_source.IndexOf(searchCriteria.Source) != -1
                || _message.IndexOf(searchCriteria.Message) != -1)
                return true;
            return false;
        }

        #endregion

        public static XmlEventLogRecord Fetch(XmlNode xmlNode)
        {
            //ComputerName, [Logfile], [CategoryString], Type, User, [EventCode], SourceName, EventIdentifier, Message, [RecordNumber], TimeGenerated
            XmlEventLogRecord eventLogRecord = new XmlEventLogRecord();
            eventLogRecord._computer = xmlNode.SelectSingleNode(COMPUTER_XPATH).InnerText;
            eventLogRecord._type = getEventLogEntryType(xmlNode.SelectSingleNode(EVENT_TYPE_XPATH).InnerText);
            eventLogRecord._user = xmlNode.SelectSingleNode(USER_XPATH).InnerText;
            eventLogRecord._source = xmlNode.SelectSingleNode(EVENT_SOURCE_XPATH).InnerText;
            eventLogRecord._eventId = Int32.Parse(xmlNode.SelectSingleNode(EVENT_ID_XPATH).InnerText);
            eventLogRecord._message = xmlNode.SelectSingleNode(EVENT_MESSAGE_XPATH).InnerText;
            eventLogRecord._generatedTime = parseDateTime(xmlNode.SelectSingleNode(GENERATED_TIME_XPATH).InnerText);
            return eventLogRecord;
        }

        private static DateTime parseDateTime(string dateTimeString)
        {
            Match match = _dateTimeMatcher.Match(dateTimeString);
            int year = Int32.Parse(match.Groups["year"].Value);
            int month = Int32.Parse(match.Groups["month"].Value);
            int day = Int32.Parse(match.Groups["day"].Value);
            int hour = Int32.Parse(match.Groups["hour"].Value);
            int minute = Int32.Parse(match.Groups["minute"].Value);
            int second = Int32.Parse(match.Groups["second"].Value);
            int millisecond = Int32.Parse(match.Groups["millisecond"].Value);

            return new DateTime(year, month, day, hour, minute, second, millisecond);
        }

        private static EventLogEntryType getEventLogEntryType(string entryAsString)
        {
            //HACK: change logparser output instead?
            if (entryAsString.ToLower().Contains("audit"))
            {
                if (entryAsString.ToLower().Contains("success")) return EventLogEntryType.SuccessAudit;
                if (entryAsString.ToLower().Contains("failure")) return EventLogEntryType.FailureAudit;
            }
            return (EventLogEntryType) Enum.Parse(typeof (EventLogEntryType), entryAsString, true);
        }
    }
}