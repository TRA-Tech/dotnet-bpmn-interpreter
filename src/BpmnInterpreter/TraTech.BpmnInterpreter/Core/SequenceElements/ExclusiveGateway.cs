using System.Xml.Linq;

namespace TraTech.BpmnInterpreter.Core.SequenceElements
{
    /// <summary>
    /// Represents a BPMN exclusive gateway, used to create alternative paths in a process flow.
    /// </summary>
    public class ExclusiveGateway : BpmnSequenceElement
    {
        /// <summary>
        /// The BPMN element type name for an exclusive gateway.
        /// </summary>
        public static readonly string ElementTypeName = "exclusiveGateway";

        /// <summary>
        /// Initializes a new instance of the <see cref="ExclusiveGateway"/> class.
        /// </summary>
        /// <param name="self">The XML element representing the exclusive gateway.</param>
        /// <param name="previousElements">The list of previous elements in the sequence.</param>
        /// <param name="nextElements">The list of next elements in the sequence.</param>
        public ExclusiveGateway(
            XElement self,
            IEnumerable<BpmnSequenceElement>? previousElements = null,
            IEnumerable<BpmnSequenceElement>? nextElements = null
        ) : base(self, previousElements, nextElements)
        {

        }
    }
}
