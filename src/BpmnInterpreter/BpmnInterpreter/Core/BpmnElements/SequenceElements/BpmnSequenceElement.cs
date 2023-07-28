using BpmnInterpreter.Extensions;
using System.Xml.Linq;

namespace BpmnInterpreter.Core.BpmnElements.SequenceElements
{
    public class BpmnSequenceElement : BpmnElement
    {
        public List<BpmnSequenceElement> PreviousElements { get; }
        public List<BpmnSequenceElement> NextElements { get; }

        public IEnumerable<string> Incomings { get; private set; }
        public IEnumerable<string> Outgoings { get; private set; }

        public bool HasPreviousElements { get => PreviousElements.Any(); }
        public bool HasNextElements { get => NextElements.Any(); }

        public BpmnSequenceElement(
            XElement self,
            IEnumerable<BpmnSequenceElement>? previousElements = null,
            IEnumerable<BpmnSequenceElement>? nextElements = null
        ) : base(self)
        {
            PreviousElements = previousElements == null ? new List<BpmnSequenceElement>() : new List<BpmnSequenceElement>(previousElements);
            NextElements = nextElements == null ? new List<BpmnSequenceElement>() : new List<BpmnSequenceElement>(nextElements);
            Incomings = self.GetIncomings();
            Outgoings = self.GetOutgoings();
        }
    }
}
