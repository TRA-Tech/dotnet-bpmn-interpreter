using TraTech.BpmnInterpreter.Core.SequenceElements;

namespace TraTech.BpmnInterpreter.Abstractions
{
    public interface ISequenceElementHandler
    {
        void Process(
            BpmnSequenceElement currentElement,
            ISequenceElementHandlerContext context
        );
    }
}
