namespace pal.EventLogAnalyzer.ConsoleClient.Filters
{
    internal class StringEqualityFilter : AbstractInputFilter<string>
    {
        public StringEqualityFilter(string propertyName) : base(propertyName)
        {
        }

        protected override bool recordMatchesCriteria(string propertyValue)
        {
            return propertyValue.Equals(_parameter);
        }

        protected override string setParameter(string value)
        {
            return value;
        }
    }
}