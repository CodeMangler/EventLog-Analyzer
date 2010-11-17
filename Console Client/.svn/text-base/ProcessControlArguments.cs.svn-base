namespace pal.EventLogAnalyzer.ConsoleClient
{
    internal class ProcessControlArguments
    {
        private double _toleranceMargin = 0;

        public double ToleranceMargin
        {
            get { return _toleranceMargin; }
        }

        public bool IsKnown(string @switch)
        {
            return @switch.Equals("-tolerance");
        }

        public void Add(string @switch, string parameter)
        {
            _toleranceMargin = double.Parse(parameter);
        }
    }
}