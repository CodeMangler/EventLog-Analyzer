namespace pal.EventLog
{
    public class LogRecordSearchCriteria
    {
        private string _message;
        private string _source;

        public LogRecordSearchCriteria(string source, string message)
        {
            _source = source;
            _message = message;
        }

        public string Source
        {
            get { return _source; }
        }

        public string Message
        {
            get { return _message; }
        }
    }
}