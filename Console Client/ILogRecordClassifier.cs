using System.Collections.Generic;

namespace pal.EventLogAnalyzer.ConsoleClient
{
    public interface ILogRecordClassifier<RecordType>
    {
        void Classify(List<RecordType> records, double toleranceMargin);
    }
}