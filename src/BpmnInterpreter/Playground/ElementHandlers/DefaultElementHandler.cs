using TraTech.BpmnInterpreter.Abstractions;
using TraTech.BpmnInterpreter.Core.SequenceElements;

namespace Playground.ElementHandlers
{
    public class DefaultElementHandler : ISequenceElementHandler
    {
        public System.Threading.Tasks.Task<SequenceNextDecision> ProcessAsync(BpmnSequenceElement currentElement, ISequenceElementHandlerContext context, CancellationToken cancellationToken = default)
        {
            Console.Out.WriteLine($"{currentElement.Name} - Processed type is {currentElement.Type}! from {GetType().Name}");

            return System.Threading.Tasks.Task.FromResult(SequenceNextDecision.UseDefault());
        }
    }
}
