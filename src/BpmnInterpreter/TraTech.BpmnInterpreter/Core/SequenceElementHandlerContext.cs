using TraTech.BpmnInterpreter.Abstractions;

namespace TraTech.BpmnInterpreter.Core
{
    public class SequenceElementHandlerContext : ISequenceElementHandlerContext
    {
        private readonly BaseSequenceProcessor _sequenceProcessor;
        public BaseSequenceProcessor SequenceProcessor => _sequenceProcessor;

        public SequenceElementHandlerContext(BaseSequenceProcessor bpmnSequenceProcessor)
        {
            _sequenceProcessor = bpmnSequenceProcessor;
        }
    }
}
