using System.Xml.Linq;

namespace TraTech.BpmnInterpreter.Core.SequenceElements
{
    /// <summary>
    /// Represents a BPMN intermediate catch event, which waits for a trigger to occur.
    /// </summary>
    public class IntermediateCatchEvent : BpmnSequenceElement
    {
        /// <summary>
        /// The BPMN element type name for an intermediate catch event.
        /// </summary>
        public static readonly string ElementTypeName = "intermediateCatchEvent";

        /// <summary>
        /// Initializes a new instance of the <see cref="IntermediateCatchEvent"/> class.
        /// </summary>
        /// <param name="self">The XML element representing the intermediate catch event.</param>
        /// <param name="previousElements">The list of previous elements in the sequence.</param>
        /// <param name="nextElements">The list of next elements in the sequence.</param>
        public IntermediateCatchEvent(
            XElement self,
            IEnumerable<BpmnSequenceElement>? previousElements = null,
            IEnumerable<BpmnSequenceElement>? nextElements = null
        ) : base(self, previousElements, nextElements)
        {

        }
    }
}
