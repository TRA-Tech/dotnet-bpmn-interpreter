using System.Xml.Linq;
using TraTech.BpmnInterpreter.Core.Elements;

namespace TraTech.BpmnInterpreter.Core.SequenceElements
{
    /// <summary>
    /// Represents a BPMN boundary event, which is attached to the boundary of an activity.
    /// </summary>
    public class BoundaryEvent : BpmnElement
    {
        /// <summary>
        /// The BPMN element type name for a boundary event.
        /// </summary>
        public static readonly string ElementTypeName = "boundaryEvent";

        /// <summary>
        /// Gets the value of the <c>attachedToRef</c> attribute.
        /// This references the activity to which the boundary event is attached.
        /// </summary>
        public string AttachedToRef { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="BoundaryEvent"/> class.
        /// </summary>
        /// <param name="self">The XML element representing the boundary event.</param>
        /// <exception cref="InvalidOperationException">Thrown when the <c>attachedToRef</c> attribute is missing.</exception>
        public BoundaryEvent(
            XElement self
        ) : base(self)
        {
            AttachedToRef = self.Attribute("attachedToRef")?.Value ?? throw new InvalidOperationException("self has no attachedToRef attribute");
        }
    }
}
