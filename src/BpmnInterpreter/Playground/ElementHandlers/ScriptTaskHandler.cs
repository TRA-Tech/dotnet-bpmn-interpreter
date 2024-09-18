using TraTech.BpmnInterpreter.Abstractions;
using TraTech.BpmnInterpreter.Core.Elements;
using TraTech.BpmnInterpreter.Core.SequenceElements;

namespace Playground.ElementHandlers
{
    public class ScriptTaskHandler : ISequenceElementHandler
    {
        public void Process(BpmnSequenceElement currentElement, ISequenceElementHandlerContext context)
        {
            var diaXElement = currentElement.Children.FirstOrDefault(f => f.Name.LocalName == DataInputAssociation.ElementTypeName);

            var dia = new DataInputAssociation(diaXElement);

            Console.Out.WriteLine($"{currentElement.Id} - Processed! from {GetType().Name}");
        }
    }
}
