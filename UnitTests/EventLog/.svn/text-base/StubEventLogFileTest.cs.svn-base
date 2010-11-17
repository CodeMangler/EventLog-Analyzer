using System;
using NUnit.Framework;

namespace pal.EventLogAnalyzer.UnitTests.EventLog
{
    [TestFixture]
    public class StubEventLogFileTest
    {
        [Test]
        public void JustCallToStringForNow()
        {
            StubEventLogFile stubEventLogFile = new StubEventLogFile();
            stubEventLogFile.Parse();
            Console.Out.WriteLine("eventLogFileContents = {0}", stubEventLogFile);
            for (int i = 0; i < stubEventLogFile.RecordCount; i++)
            {
                Console.Out.WriteLine("stubEventLogFile[{0}] = {1}", i, stubEventLogFile[i]);
            }
        }
    }
}