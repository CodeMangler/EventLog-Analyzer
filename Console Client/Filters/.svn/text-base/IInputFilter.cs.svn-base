using pal.EventLog;

namespace pal.EventLogAnalyzer.ConsoleClient.Filters
{
    internal interface IInputFilter
    {
        object Parameter { get; set; } //RHS for evaluation

        bool Filter(IEventLogRecord recordToFilter);
    }
}