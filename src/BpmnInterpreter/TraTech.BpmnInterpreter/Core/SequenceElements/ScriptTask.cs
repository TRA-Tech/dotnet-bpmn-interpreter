using System.Xml.Linq;

namespace TraTech.BpmnInterpreter.Core.SequenceElements
{
    /// <summary>
    /// Represents a BPMN script task, which executes a script.
    /// </summary>
    public class ScriptTask : BpmnSequenceElement
    {
        /// <summary>
        /// The BPMN element type name for a script task.
        /// </summary>
        public static readonly string ElementTypeName = "scriptTask";

        /// <summary>
        /// Initializes a new instance of the <see cref="ScriptTask"/> class.
        /// </summary>
        /// <param name="self">The XML element representing the script task.</param>
        /// <param name="previousElements">The list of previous elements in the sequence.</param>
        /// <param name="nextElements">The list of next elements in the sequence.</param>
        public ScriptTask(
            XElement self,
            IEnumerable<BpmnSequenceElement>? previousElements = null,
            IEnumerable<BpmnSequenceElement>? nextElements = null
        ) : base(self, previousElements, nextElements)
        {

        }
    }
}
