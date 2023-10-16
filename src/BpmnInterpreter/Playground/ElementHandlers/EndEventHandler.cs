using TraTech.BpmnInterpreter.Abstractions;
using TraTech.BpmnInterpreter.Core.SequenceElements;

namespace Playground.ElementHandlers
{
    public class EndEventHandler : ISequenceElementHandler
    {
        public void Process(BpmnSequenceElement currentElement, ISequenceElementHandlerContext context)
        {
            var data = context.GetData<Data>("object");

            Console.Out.WriteLine($"{currentElement.Id} - Processed data.Number is {data.Number}! from {nameof(EndEventHandler)}");
        }
    }
}
