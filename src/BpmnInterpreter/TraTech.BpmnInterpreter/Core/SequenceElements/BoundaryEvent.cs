using System.Xml.Linq;

namespace TraTech.BpmnInterpreter.Core.SequenceElements
{
    /// <summary>
    /// Represents a BPMN boundary event, which is attached to the boundary of an activity.
    /// </summary>
    public class BoundaryEvent : BpmnSequenceElement
    {
        /// <summary>
        /// The BPMN element type name for a boundary event.
        /// </summary>
        public static readonly string ElementTypeName = "boundaryEvent";

        /// <summary>
        /// Gets the ID of the activity to which this boundary event is attached.
        /// </summary>
        public string AttachedToRef { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="BoundaryEvent"/> class.
        /// </summary>
        /// <param name="self">The XML element representing the boundary event.</param>
        /// <param name="previousElements">The list of previous elements in the sequence.</param>
        /// <param name="nextElements">The list of next elements in the sequence.</param>
        /// <exception cref="InvalidOperationException">Thrown when the attachedToRef attribute is missing.</exception>
        public BoundaryEvent(
            XElement self,
            IEnumerable<BpmnSequenceElement>? previousElements = null,
            IEnumerable<BpmnSequenceElement>? nextElements = null
        ) : base(self, previousElements, nextElements)
        {
            AttachedToRef = self.Attribute("attachedToRef")?.Value ?? throw new InvalidOperationException("self has no dataObjectRef attribute");
        }
    }
}
