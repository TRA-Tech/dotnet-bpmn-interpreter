using TraTech.BpmnInterpreter.Abstractions;
using TraTech.BpmnInterpreter.Core.Elements;
using TraTech.BpmnInterpreter.Core.SequenceElements;

namespace Playground.ElementHandlers
{
    public class ScriptTaskHandler : ISequenceElementHandler
    {
        public System.Threading.Tasks.Task<SequenceNextDecision> ProcessAsync(BpmnSequenceElement currentElement, ISequenceElementHandlerContext context, CancellationToken cancellationToken = default)
        {
            var diaXElement = currentElement.Children.FirstOrDefault(f => f.Name.LocalName == DataInputAssociation.ElementTypeName);

            var dia = new DataInputAssociation(diaXElement);

            Console.Out.WriteLine($"{currentElement.Name} - Processed! from {GetType().Name}");

            return System.Threading.Tasks.Task.FromResult(SequenceNextDecision.UseDefault());
        }
    }
}
