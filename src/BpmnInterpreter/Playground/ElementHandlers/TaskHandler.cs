using TraTech.BpmnInterpreter.Abstractions;
using TraTech.BpmnInterpreter.Core.SequenceElements;

namespace Playground.ElementHandlers
{
    public class TaskHandler : ISequenceElementHandler
    {
        public void Process(BpmnSequenceElement currentElement, ISequenceElementHandlerContext context)
        {
            var data = context.GetData<Data>("object");
            data.Number++;

            var result = context.TryGetData<Data>("object", out var data2);
            if (data2 is not null)
                data2.Number++;

            Console.Out.WriteLine($"{currentElement.Id} - Processed! from {nameof(TaskHandler)}");
        }
    }
}
