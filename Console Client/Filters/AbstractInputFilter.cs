using pal.EventLog;

namespace pal.EventLogAnalyzer.ConsoleClient.Filters
{
    internal abstract class AbstractInputFilter<PropertyType> : IInputFilter
    {
        private readonly string _propertyName;
        protected object _parameter;

        public AbstractInputFilter(string propertyName)
        {
            _propertyName = propertyName;
        }

        #region IInputFilter Members

        public object Parameter
        {
            get { return _parameter; }
            set { _parameter = setParameter(value as string); }
        }


        public bool Filter(IEventLogRecord recordToFilter)
        {
            PropertyType propertyValue = (PropertyType) getPropertyFrom(recordToFilter);
            return recordMatchesCriteria(propertyValue);
        }

        #endregion

        protected abstract bool recordMatchesCriteria(PropertyType propertyValue);

        protected abstract PropertyType setParameter(string value);

        private object getPropertyFrom(IEventLogRecord recordToFilter)
        {
            return typeof (IEventLogRecord).GetProperty(_propertyName).GetValue(recordToFilter, null);
        }
    }
}