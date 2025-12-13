using System.Xml.Linq;

namespace TraTech.BpmnInterpreter.Core.SequenceElements
{
    /// <summary>
    /// Represents a BPMN parallel gateway, used to synchronize or fork multiple paths.
    /// </summary>
    public class ParallelGateway : BpmnSequenceElement
    {
        /// <summary>
        /// The BPMN element type name for a parallel gateway.
        /// </summary>
        public static readonly string ElementTypeName = "parallelGateway";

        /// <summary>
        /// Initializes a new instance of the <see cref="ParallelGateway"/> class.
        /// </summary>
        /// <param name="self">The XML element representing the parallel gateway.</param>
        /// <param name="previousElements">The list of previous elements in the sequence.</param>
        /// <param name="nextElements">The list of next elements in the sequence.</param>
        public ParallelGateway(
            XElement self,
            IEnumerable<BpmnSequenceElement>? previousElements = null,
            IEnumerable<BpmnSequenceElement>? nextElements = null
        ) : base(self, previousElements, nextElements)
        {

        }
    }
}
