using TraTech.BpmnInterpreter.Abstractions;
using TraTech.BpmnInterpreter.Core.SequenceElements;

namespace Playground.ElementHandlers
{
    public class StartEventHandler : ISequenceElementHandler
    {
        public void Process(BpmnSequenceElement currentElement, ISequenceElementHandlerContext context)
        {
            context.SetData("number", 10);
            context.SetData("object", new Data()
            {
                Name = "Name1",
                Description = "Description1",
                Number = 1,
            });

            context.TrySetData("number", 10);
            context.TrySetData("value", 10);
            Console.Out.WriteLine($"{currentElement.Id} - Processed! from {nameof(StartEventHandler)}");
        }
    }
}
