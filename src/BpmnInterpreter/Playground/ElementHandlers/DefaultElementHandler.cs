using TraTech.BpmnInterpreter.Abstractions;
using TraTech.BpmnInterpreter.Core.SequenceElements;

namespace Playground.ElementHandlers
{
    public class DefaultElementHandler : ISequenceElementHandler
    {
        public void Process(BpmnSequenceElement currentElement, ISequenceElementHandlerContext context)
        {
            Console.Out.WriteLine($"{currentElement.Id} - Processed type is {currentElement.Type}! from {GetType().Name}");
        }
    }
}
