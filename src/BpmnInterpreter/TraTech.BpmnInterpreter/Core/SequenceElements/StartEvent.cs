using System.Xml.Linq;

namespace TraTech.BpmnInterpreter.Core.SequenceElements
{
    /// <summary>
    /// Represents a BPMN start event.
    /// </summary>
    public class StartEvent : BpmnSequenceElement
    {
        /// <summary>
        /// The BPMN element type name for a start event.
        /// </summary>
        public static readonly string ElementTypeName = "startEvent";

        /// <summary>
        /// Initializes a new instance of the <see cref="StartEvent"/> class.
        /// </summary>
        /// <param name="self">The XML element representing the start event.</param>
        /// <param name="nextElements">The list of next elements in the sequence.</param>
        public StartEvent(XElement self, IEnumerable<BpmnSequenceElement>? nextElements = null) : base(self, null, nextElements)
        {

        }
    }
}
