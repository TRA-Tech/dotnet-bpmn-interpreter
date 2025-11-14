using TraTech.BpmnInterpreter.Abstractions;
using TraTech.BpmnInterpreter.Core.SequenceElements;

namespace Playground.ElementHandlers
{
    public class SubProcessHandler : ISequenceElementHandler
    {
        public void Process(BpmnSequenceElement currentElement, ISequenceElementHandlerContext context)
        {
            var data = context.DataMap.Get<Data>("object");

            Console.Out.WriteLine($"{currentElement.Name} - Processed! from {GetType().Name}");
        }
    }
}
