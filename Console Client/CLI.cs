using System;
using System.Collections.Generic;
using pal.EventLog;
using pal.EventLogAnalyzer.ConsoleClient.Properties;
using pal.EventLogAnalyzer.ConsoleClient.Reporting;

namespace pal.EventLogAnalyzer.ConsoleClient
{
    public class CLI
    {
        private static readonly List<IEventLogRecord> _filteredRecords = new List<IEventLogRecord>();
        private static readonly EventLogFileCollection _inputFiles = new EventLogFileCollection();

        public static void Main(string[] args)
        {
            #region Extract Arguments

            if (args.Length == 0) printHelpAndExit();

            CommandLineArgumentHandler argumentHandler = new CommandLineArgumentHandler();
            for (int i = 0; i < args.Length; i += 2)
            {
                if (args[i].ToLower().Contains("help"))
                {
                    printHelpAndExit();
                }

                if (argumentHandler.IsKnown(args[i]))
                    argumentHandler.PickArgument(args[i], args[i + 1]);
                else
                {
                    IEventLogFile eventLogFile = new EventLogFileFactory(args[i]).EventLogFile;
                    eventLogFile.Parse();
                    _inputFiles.Add(eventLogFile);
                }
            }

            #endregion

            #region Apply Input Filters

            foreach (IEventLogRecord eventLogRecord in _inputFiles)
            {
                if (argumentHandler.Filters.Filter(eventLogRecord))
                    _filteredRecords.Add(eventLogRecord);
            }

            #endregion

            disposeAllInputFilesToReduceMemoryFootprint();

            #region Auto-classify filtered records

            EventLogRecordClassifier classifier = new EventLogRecordClassifier();
            classifier.Classify(_filteredRecords, argumentHandler.ProcessArguments.ToleranceMargin);

            #endregion

            #region Generate reports

            ReportGenerator reportGenerator =
                new ReportGenerator(argumentHandler.OutputArguments.OutputFolder,
                                    argumentHandler.OutputArguments.ReportFields);
            reportGenerator.WriteReportsFor(classifier.Aggregates);

            #endregion
        }

        private static void disposeAllInputFilesToReduceMemoryFootprint()
        {
            _inputFiles.Dispose();
        }

        private static void printHelpAndExit()
        {
            Console.Out.WriteLine(Resources.HelpText);
            Environment.Exit(1);
        }
    }
}