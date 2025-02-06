using TraTech.BpmnInterpreter.Extensions;
using System.Xml.Linq;
using TraTech.BpmnInterpreter.Core.Elements;

namespace TraTech.BpmnInterpreter.Core.SequenceElements
{
    public class BpmnSequenceElement : BpmnElement
    {
        public List<BpmnSequenceElement> PreviousElements { get; }
        public List<BpmnSequenceElement> NextElements { get; }
        public List<BpmnSequenceElement> Boundaries { get; }

        public IEnumerable<string> Incomings { get; private set; }
        public IEnumerable<string> Outgoings { get; private set; }

        public bool HasPreviousElements { get => PreviousElements.Any(); }
        public bool HasNextElements { get => NextElements.Any(); }

        public BpmnSequenceElement(
            XElement self,
            IEnumerable<BpmnSequenceElement>? previousElements = null,
            IEnumerable<BpmnSequenceElement>? nextElements = null,
            BpmnElement element = null, 
            IEnumerable<BpmnSequenceElement>? boundaryEvents = null 
        ) : base(self)
        {
            PreviousElements = previousElements == null ? new List<BpmnSequenceElement>() : new List<BpmnSequenceElement>(previousElements);
            NextElements = nextElements == null ? new List<BpmnSequenceElement>() : new List<BpmnSequenceElement>(nextElements);
            Incomings = self.GetIncomings();
            Outgoings = self.GetOutgoings();
            Boundaries = element.GetBoundaries(boundaryEvents);
        }
    }
}
