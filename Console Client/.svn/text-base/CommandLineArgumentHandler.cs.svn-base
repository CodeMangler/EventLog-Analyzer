using System;

namespace pal.EventLogAnalyzer.ConsoleClient
{
    internal class CommandLineArgumentHandler
    {
        private readonly InputFilterArguments _inputFilterArguments;
        private readonly OutputFilterArguments _outputArguments;
        private readonly ProcessControlArguments _processControlArguments;

        public CommandLineArgumentHandler()
        {
            //TODO: Still to handle mandatory arguments 
            _inputFilterArguments = new InputFilterArguments();
            _outputArguments = new OutputFilterArguments();
            _processControlArguments = new ProcessControlArguments();
        }

        public InputFilterArguments Filters
        {
            get { return _inputFilterArguments; }
        }

        public ProcessControlArguments ProcessArguments
        {
            get { return _processControlArguments; }
        }

        public OutputFilterArguments OutputArguments
        {
            get { return _outputArguments; }
        }

        public void PickArgument(string argumentSwitch, string argumentParameter)
        {
            if (!IsKnown(argumentSwitch))
                throw new Exception();

            //TODO: BaseArgumentType for all three types of arguments?

            if (_inputFilterArguments.IsKnown(argumentSwitch))
            {
                _inputFilterArguments.Add(argumentSwitch, argumentParameter);
            }

            if (_outputArguments.IsKnown(argumentSwitch))
            {
                _outputArguments.Add(argumentSwitch, argumentParameter);
                return;
            }

            if (_processControlArguments.IsKnown(argumentSwitch))
            {
                _processControlArguments.Add(argumentSwitch, argumentParameter);
            }
        }

        public bool IsKnown(string @switch)
        {
            return _inputFilterArguments.IsKnown(@switch)
                   || _outputArguments.IsKnown(@switch)
                   || _processControlArguments.IsKnown(@switch);
        }
    }
}