namespace pal.EventLogAnalyzer.ConsoleClient.Filters
{
    internal class StringContainsFilter : AbstractInputFilter<string>
    {
        public StringContainsFilter(string propertyName) : base(propertyName)
        {
        }

        protected override bool recordMatchesCriteria(string propertyValue)
        {
            return ((string) _parameter).Contains(propertyValue);
        }

        protected override string setParameter(string value)
        {
            return value;
        }
    }
}