using System.Xml.Linq;

namespace TraTech.BpmnInterpreter.Core.SequenceElements
{
    /// <summary>
    /// Represents a BPMN end event.
    /// </summary>
    public class EndEvent : BpmnSequenceElement
    {
        /// <summary>
        /// The BPMN element type name for an end event.
        /// </summary>
        public static readonly string ElementTypeName = "endEvent";

        /// <summary>
        /// Initializes a new instance of the <see cref="EndEvent"/> class.
        /// </summary>
        /// <param name="self">The XML element representing the end event.</param>
        /// <param name="nextElements">The list of next elements in the sequence (usually null for end events).</param>
        public EndEvent(XElement self, IEnumerable<BpmnSequenceElement>? nextElements = null) : base(self, null, nextElements)
        {

        }
    }
}
