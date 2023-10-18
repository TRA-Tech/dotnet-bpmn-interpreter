using TraTech.BpmnInterpreter.Abstractions;

namespace TraTech.BpmnInterpreter.Core
{
    public class SequenceElementHandlerContext : ISequenceElementHandlerContext
    {
        private readonly IDataMap _dataMap;
        private readonly BaseSequence _sequence;
        private readonly BaseSequenceProcessor _sequenceProcessor;

        public IDataMap DataMap => _dataMap;

        public BaseSequence Sequence => _sequence;

        public BaseSequenceProcessor SequenceProcessor => _sequenceProcessor;

        public SequenceElementHandlerContext(IDataMap dataMap, BaseSequence sequence, BaseSequenceProcessor sequenceProcessor)
        {
            _dataMap = dataMap;
            _sequence = sequence;
            _sequenceProcessor = sequenceProcessor;
        }
    }
}
