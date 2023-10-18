using TraTech.BpmnInterpreter.Abstractions;
using TraTech.BpmnInterpreter.Core.SequenceElements;

namespace Playground.ElementHandlers
{
    public class StartEventHandler : ISequenceElementHandler
    {
        public void Process(BpmnSequenceElement currentElement, ISequenceElementHandlerContext context)
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
            Console.Out.WriteLine($"{currentElement.Id} - Processed! from {nameof(StartEventHandler)}");
        }
    }
}
