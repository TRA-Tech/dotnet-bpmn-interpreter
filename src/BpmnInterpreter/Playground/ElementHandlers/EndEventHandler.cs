using TraTech.BpmnInterpreter.Abstractions;
using TraTech.BpmnInterpreter.Core.SequenceElements;

namespace Playground.ElementHandlers
{
    public class EndEventHandler : ISequenceElementHandler
    {
        public System.Threading.Tasks.Task<SequenceNextDecision> ProcessAsync(BpmnSequenceElement currentElement, ISequenceElementHandlerContext context, CancellationToken cancellationToken = default)
        {
            var data = context.DataMap.Get<Data>("object");

            Console.Out.WriteLine($"{currentElement.Name} - Processed data.Number is {data.Number}! from {nameof(EndEventHandler)}");

            return System.Threading.Tasks.Task.FromResult(SequenceNextDecision.UseDefault());
        }
    }
}
