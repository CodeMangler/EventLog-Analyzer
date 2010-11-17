using System;
using System.Diagnostics;

namespace pal.EventLogAnalyzer.ConsoleClient.Filters
{
    internal class EventTypeFilter : AbstractInputFilter<EventLogEntryType>
    {
        public EventTypeFilter(string propertyName) : base(propertyName)
        {
        }

        protected override bool recordMatchesCriteria(EventLogEntryType propertyValue)
        {
            return propertyValue == (EventLogEntryType) _parameter;
        }

        protected override EventLogEntryType setParameter(string value)
        {
            return (EventLogEntryType) Enum.Parse(typeof (EventLogEntryType), value, true);
        }
    }
}