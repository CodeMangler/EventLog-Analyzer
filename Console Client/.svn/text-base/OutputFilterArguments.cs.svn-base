using System.Collections.Generic;

namespace pal.EventLogAnalyzer.ConsoleClient
{
    internal class OutputFilterArguments
    {
        private const string DEFAULT_REPORT_FIELDS = "All";
        private readonly List<string> _knownOutputArguments = new List<string>();
        private readonly Dictionary<string, string> _providedOutputArguments = new Dictionary<string, string>();

        public OutputFilterArguments()
        {
            _knownOutputArguments.Add("-include");
            _knownOutputArguments.Add("-output");
        }

        public string OutputFolder
        {
            get { return _providedOutputArguments["-output"]; }
        }

        public object ReportFields
        {
            get
            {
                string argument = DEFAULT_REPORT_FIELDS;
                if (_providedOutputArguments.ContainsKey("-include"))
                    argument = _providedOutputArguments["-include"];

                return argument;
            }
        }

        public bool IsKnown(string @switch)
        {
            return _knownOutputArguments.Contains(@switch);
        }

        public void Add(string @switch, string parameter)
        {
            _providedOutputArguments.Add(@switch, parameter);
        }
    }
}