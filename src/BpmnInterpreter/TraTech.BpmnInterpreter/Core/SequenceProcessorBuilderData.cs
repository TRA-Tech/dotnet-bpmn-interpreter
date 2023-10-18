using TraTech.BpmnInterpreter.Abstractions;

namespace TraTech.BpmnInterpreter.Core
{
    public sealed class BpmnSequenceProcessorData
    {
        public BaseSequence? BpmnSequence;

        public readonly Dictionary<string, ISequenceElementHandler> ElementHandlerMap = new();

        public ISequenceElementHandler? DefaultElementHandler;

        public IDataMap? DataMap;
    }
}
