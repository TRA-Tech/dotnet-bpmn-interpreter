using TraTech.BpmnInterpreter.Abstractions;
using TraTech.BpmnInterpreter.Core.SequenceElements;

namespace Playground.ElementHandlers
{
    public class DefaultBoundaryEventHandler : IBoundaryEventHandler
    {
        public System.Threading.Tasks.Task ProcessAsync(BoundaryEvent boundaryElement, ISequenceElementHandlerContext context, CancellationToken cancellationToken = default)
        {
            var data = context.DataMap.Get<Data>("object");

            Console.Out.WriteLine($"{boundaryElement.Name} - Processed data.Number is {data.Number}! from {nameof(BoundaryEventHandler)}");

            return System.Threading.Tasks.Task.CompletedTask;
        }
    }
}
