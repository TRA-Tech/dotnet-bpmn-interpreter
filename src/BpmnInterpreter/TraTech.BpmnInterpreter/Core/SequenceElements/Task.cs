using System.Xml.Linq;

namespace TraTech.BpmnInterpreter.Core.SequenceElements
{
    /// <summary>
    /// Represents a generic BPMN task.
    /// </summary>
    public class Task : BpmnSequenceElement
    {
        /// <summary>
        /// The BPMN element type name for a task.
        /// </summary>
        public static readonly string ElementTypeName = "task";

        /// <summary>
        /// Initializes a new instance of the <see cref="Task"/> class.
        /// </summary>
        /// <param name="self">The XML element representing the task.</param>
        /// <param name="previousElements">The list of previous elements in the sequence.</param>
        /// <param name="nextElements">The list of next elements in the sequence.</param>
        public Task(
            XElement self,
            IEnumerable<BpmnSequenceElement>? previousElements = null,
            IEnumerable<BpmnSequenceElement>? nextElements = null
        ) : base(self, previousElements, nextElements)
        {

        }
    }
}
