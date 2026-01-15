using TraTech.BpmnInterpreter.Abstractions;
using TraTech.BpmnInterpreter.Core.SequenceElements;

namespace Playground.ElementHandlers
{
    public class ExclusiveGatewayHandler : ISequenceElementHandler
    {
        public System.Threading.Tasks.Task<SequenceNextDecision> ProcessAsync(BpmnSequenceElement currentElement, ISequenceElementHandlerContext context, CancellationToken cancellationToken = default)
        {
            var next = currentElement.NextElements.LastOrDefault();
            Console.Out.WriteLine($"{currentElement.Name} - Processed! from {nameof(ExclusiveGatewayHandler)}");

            return System.Threading.Tasks.Task.FromResult(next is null ? SequenceNextDecision.None() : SequenceNextDecision.WithNext(next));
        }
    }
}
