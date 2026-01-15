using TraTech.BpmnInterpreter.Abstractions;
using TraTech.BpmnInterpreter.Core.SequenceElements;

namespace Playground.ElementHandlers
{
    public class StartEventHandler : ISequenceElementHandler
    {
        public System.Threading.Tasks.Task<SequenceNextDecision> ProcessAsync(BpmnSequenceElement currentElement, ISequenceElementHandlerContext context, CancellationToken cancellationToken = default)
        {
            context.DataMap.Set("number", 10);
            context.DataMap.Set("object", new Data()
            {
                Name = "Name1",
                Description = "Description1",
                Number = 1,
            });

            context.DataMap.TrySet("number", 10);
            context.DataMap.TrySet("value", 10);
            Console.Out.WriteLine($"{currentElement.Name} - Processed! from {nameof(StartEventHandler)}");

            return System.Threading.Tasks.Task.FromResult(SequenceNextDecision.UseDefault());
        }
    }
}
