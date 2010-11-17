using System;

namespace pal.EventLogAnalyzer.ConsoleClient.Filters
{
    internal class AfterDateTimeFilter : AbstractInputFilter<DateTime>
    {
        public AfterDateTimeFilter(string propertyName) : base(propertyName)
        {
        }

        protected override bool recordMatchesCriteria(DateTime propertyValue)
        {
            return propertyValue.CompareTo(_parameter) < 0;
        }

        protected override DateTime setParameter(string value)
        {
            return DateTime.Parse(value);
        }
    }
}