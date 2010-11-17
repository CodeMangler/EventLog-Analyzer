namespace pal.EventLogAnalyzer.ConsoleClient.Filters
{
    internal class IntegerEqualityFilter : AbstractInputFilter<int>
    {
        public IntegerEqualityFilter(string propertyName) : base(propertyName)
        {
        }

        protected override bool recordMatchesCriteria(int propertyValue)
        {
            return propertyValue == (int) _parameter;
        }

        protected override int setParameter(string value)
        {
            return int.Parse(value);
        }
    }
}