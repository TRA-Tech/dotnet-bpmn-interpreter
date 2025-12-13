using System.Xml.Linq;

namespace TraTech.BpmnInterpreter.Core.SequenceElements
{
    /// <summary>
    /// Represents a BPMN sub-process, which is an activity that contains other activities.
    /// </summary>
    public class SubProcess : BpmnSequenceElement
    {
        /// <summary>
        /// The BPMN element type name for a sub-process.
        /// </summary>
        public static readonly string ElementTypeName = "subProcess";

        /// <summary>
        /// Initializes a new instance of the <see cref="SubProcess"/> class.
        /// </summary>
        /// <param name="self">The XML element representing the sub-process.</param>
        /// <param name="previousElements">The list of previous elements in the sequence.</param>
        /// <param name="nextElements">The list of next elements in the sequence.</param>
        public SubProcess(
            XElement self,
            IEnumerable<BpmnSequenceElement>? previousElements = null,
            IEnumerable<BpmnSequenceElement>? nextElements = null
        ) : base(self, previousElements, nextElements)
        {

        }
    }
}
