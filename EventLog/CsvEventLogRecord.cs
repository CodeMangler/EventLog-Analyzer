using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Text.RegularExpressions;

namespace pal.EventLog
{
    public class CsvEventLogRecord : IEventLogRecord
    {
        private const int COMPUTER_NAME = 0;

        private const string DATETIME_MATCHER =
            @"(?<year>\d{4})(?<month>\d{2})(?<day>\d{2})(?<hour>\d{2})(?<minute>\d{2})(?<second>\d{2})\.(?<millisecond>\d*)";

        private const int EVENT_ID = 7;
        private const int EVENT_LOG_TYPE = 3;
        private const int GENERATED_TIME = 10;
        private const int MESSAGE = 8;
        private const int SOURCE = 6;
        private const int USER = 4;
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

        public static CsvEventLogRecord Fetch(string csvLine)
        {
            List<string> fields = new List<string>();
            string[] rawFields = csvLine.Split(',');
            for (int i = 0; i < rawFields.Length; i++)
            {
                StringBuilder fieldContent = new StringBuilder(rawFields[i]);

                if (rawFields[i].StartsWith("\""))
                {
                    if (!rawFields[i].EndsWith("\""))
                    {
                        do
                        {
                            i++;
                            fieldContent.Append(rawFields[i]);
                        } while (!rawFields[i].Trim().EndsWith("\""));
                    }
                }

                fields.Add(fieldContent.ToString());
            }

            for (int i = 0; i < fields.Count; i++)
            {
                fields[i] = fields[i].Replace("<NL>", "\n");
            }

            //ComputerName, [Logfile], [CategoryString], Type, User, [EventCode], SourceName, EventIdentifier, Message, [RecordNumber], TimeGenerated
            CsvEventLogRecord eventLogRecord = new CsvEventLogRecord();
            eventLogRecord._computer = fields[COMPUTER_NAME];
            eventLogRecord._type =
                getEventLogEntryType(fields[EVENT_LOG_TYPE]);
            eventLogRecord._user = fields[USER];
            eventLogRecord._source = fields[SOURCE];
            eventLogRecord._eventId = Int32.Parse(fields[EVENT_ID]);
            eventLogRecord._message = fields[MESSAGE];
            eventLogRecord._generatedTime = parseDateTime(fields[GENERATED_TIME]);
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