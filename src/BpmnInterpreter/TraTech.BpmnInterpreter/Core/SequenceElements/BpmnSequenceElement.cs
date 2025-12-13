using TraTech.BpmnInterpreter.Extensions;
using System.Xml.Linq;
using TraTech.BpmnInterpreter.Core.Elements;

namespace TraTech.BpmnInterpreter.Core.SequenceElements
{
    /// <summary>
    /// Represents a BPMN element within a sequence flow, including its connections to other elements.
    /// </summary>
    public class BpmnSequenceElement : BpmnElement
    {
        /// <summary>
        /// Gets the list of elements that precede this element in the sequence.
        /// </summary>
        public List<BpmnSequenceElement> PreviousElements { get; }

        /// <summary>
        /// Gets the list of elements that follow this element in the sequence.
        /// </summary>
        public List<BpmnSequenceElement> NextElements { get; }

        /// <summary>
        /// Gets the list of boundary events attached to this element.
        /// </summary>
        public List<BpmnSequenceElement> Boundaries { get; }

        /// <summary>
        /// Gets the collection of incoming sequence flow IDs.
        /// </summary>
        public IEnumerable<string> Incomings { get; private set; }

        /// <summary>
        /// Gets the collection of outgoing sequence flow IDs.
        /// </summary>
        public IEnumerable<string> Outgoings { get; private set; }

        /// <summary>
        /// Gets a value indicating whether this element has any previous elements.
        /// </summary>
        public bool HasPreviousElements { get => PreviousElements.Any(); }

        /// <summary>
        /// Gets a value indicating whether this element has any next elements.
        /// </summary>
        public bool HasNextElements { get => NextElements.Any(); }

        /// <summary>
        /// Initializes a new instance of the <see cref="BpmnSequenceElement"/> class.
        /// </summary>
        /// <param name="self">The XML element representing the BPMN element.</param>
        /// <param name="previousElements">The list of previous elements.</param>
        /// <param name="nextElements">The list of next elements.</param>
        /// <param name="element">The base BPMN element.</param>
        /// <param name="boundaryEvents">The collection of boundary events.</param>
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
