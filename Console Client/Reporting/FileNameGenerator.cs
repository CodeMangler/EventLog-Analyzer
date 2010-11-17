using System;
using System.Collections.Generic;

namespace pal.EventLogAnalyzer.ConsoleClient.Reporting
{
    class FileNameGenerator
    {
        private readonly string _prefix;
        private readonly string _suffix;
        private readonly IEnumerator<string> _fileNameGenerator;

        public FileNameGenerator(string prefix, string suffix)
        {
            _prefix = prefix;
            _suffix = suffix;
            _fileNameGenerator = fileNameGenerator();
        }

        private IEnumerator<string> fileNameGenerator()
        {
            for (int i = 0; i < Int32.MaxValue; i++)
            {
                yield return _prefix + " " + i + _suffix;
            }
        }

        public string NextName
        {
            get
            {
                _fileNameGenerator.MoveNext();
                return _fileNameGenerator.Current;
            }
        }
    }
}