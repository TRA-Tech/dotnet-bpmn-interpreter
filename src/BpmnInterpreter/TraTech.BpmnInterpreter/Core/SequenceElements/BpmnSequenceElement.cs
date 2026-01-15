using TraTech.BpmnInterpreter.Extensions;
using System.Xml.Linq;
using TraTech.BpmnInterpreter.Core.Elements;

namespace TraTech.BpmnInterpreter.Core.SequenceElements
{
    /// <summary>
    /// Represents a BPMN element within a sequence, including its incoming/outgoing flow references
    /// and its resolved connections to other sequence elements.
    /// </summary>
    public class BpmnSequenceElement : BpmnElement
    {
        /// <summary>
        /// Gets the resolved sequence elements that precede this element.
        /// </summary>
        public List<BpmnSequenceElement> PreviousElements { get; }

        /// <summary>
        /// Gets the resolved sequence elements that follow this element.
        /// </summary>
        public List<BpmnSequenceElement> NextElements { get; }

        /// <summary>
        /// Gets the boundary events attached to this element.
        /// </summary>
        public List<BoundaryEvent> Boundaries { get; }

        /// <summary>
        /// Gets the IDs of incoming sequence flows.
        /// </summary>
        public IEnumerable<string> Incomings { get; private set; }

        /// <summary>
        /// Gets the IDs of outgoing sequence flows.
        /// </summary>
        public IEnumerable<string> Outgoings { get; private set; }

        /// <summary>
        /// Gets a value indicating whether this element has any previous elements.
        /// </summary>
        public bool HasPreviousElements { get => PreviousElements.Count != 0; }

        /// <summary>
        /// Gets a value indicating whether this element has any next elements.
        /// </summary>
        public bool HasNextElements { get => NextElements.Count != 0; }

        /// <summary>
        /// Initializes a new instance of the <see cref="BpmnSequenceElement"/> class.
        /// </summary>
        /// <param name="self">The XML element representing the BPMN element.</param>
        /// <param name="previousElements">The resolved previous sequence elements.</param>
        /// <param name="nextElements">The resolved next sequence elements.</param>
        /// <param name="boundaryEvents">The boundary events attached to this element.</param>
        public BpmnSequenceElement(
            XElement self,
            IEnumerable<BpmnSequenceElement>? previousElements = null,
            IEnumerable<BpmnSequenceElement>? nextElements = null,
            IEnumerable<BoundaryEvent>? boundaryEvents = null 
        ) : base(self)
        {
            PreviousElements = previousElements == null ? [] : new List<BpmnSequenceElement>(previousElements);
            NextElements = nextElements == null ? [] : new List<BpmnSequenceElement>(nextElements);
            Incomings = self.GetIncomings();
            Outgoings = self.GetOutgoings();
            Boundaries = boundaryEvents == null ? [] : new List<BoundaryEvent>(boundaryEvents);
        }
    }
}
