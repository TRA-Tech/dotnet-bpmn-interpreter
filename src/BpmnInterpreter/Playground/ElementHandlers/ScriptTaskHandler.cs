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
            if (diaXElement is null)
            {
                context.SequenceProcessor.Stop();
                return;
            }

            var dia = new DataInputAssociation(diaXElement);

            var sourceElement = context.Sequence.BpmnElements.First(f => f.Id == dia.SourceRef);
            if (sourceElement.Type != DataStoreReference.ElementTypeName)
            {
                context.SequenceProcessor.Stop();
                return;
            }

            var dsr = new DataStoreReference(sourceElement.Self);


            Console.Out.WriteLine($"{currentElement.Id} - Processed! from {GetType().Name}");
        }
    }
}
