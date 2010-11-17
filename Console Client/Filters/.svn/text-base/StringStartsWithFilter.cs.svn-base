namespace pal.EventLogAnalyzer.ConsoleClient.Filters
{
    internal class StringStartsWithFilter : AbstractInputFilter<string>
    {
        public StringStartsWithFilter(string propertyName) : base(propertyName)
        {
        }

        protected override bool recordMatchesCriteria(string propertyValue)
        {
            return ((string) _parameter).StartsWith(propertyValue);
        }

        protected override string setParameter(string value)
        {
            return value;
        }
    }
}