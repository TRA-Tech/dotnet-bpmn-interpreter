using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TraTech.BpmnInterpreter.Abstractions;
using TraTech.BpmnInterpreter.Core.SequenceElements;

namespace Playground.ElementHandlers
{
    public class IntermediateCatchEventHandler : ISequenceElementHandler
    {
        public void Process(BpmnSequenceElement currentElement, ISequenceElementHandlerContext context)
        {
            var data = context.DataMap.Get<Data>("object");

            Console.Out.WriteLine($"{currentElement.Name} - Processed! from {nameof(IntermediateCatchEventHandler)}");
        }
    }
}
