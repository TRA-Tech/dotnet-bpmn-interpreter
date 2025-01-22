using TraTech.BpmnInterpreter.Abstractions;
using TraTech.BpmnInterpreter.Core.SequenceElements;

namespace Playground.ElementHandlers
{
    public class EndEventHandler : ISequenceElementHandler
    {
        public void Process(BpmnSequenceElement currentElement, ISequenceElementHandlerContext context)
        {
            var data = context.DataMap.Get<Data>("object");

            Console.Out.WriteLine($"{currentElement.Name} - Processed data.Number is {data.Number}! from {nameof(EndEventHandler)}");
        }
    }
}
