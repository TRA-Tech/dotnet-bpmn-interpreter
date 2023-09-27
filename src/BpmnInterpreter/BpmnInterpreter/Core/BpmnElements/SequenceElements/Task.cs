using System.Xml.Linq;

namespace BpmnInterpreter.Core.BpmnElements.SequenceElements
{
    public class Task : BpmnSequenceElement
    {
        public static readonly string ElementTypeName = "task";

        public Task(
            XElement self,
            IEnumerable<BpmnSequenceElement>? previousElements = null,
            IEnumerable<BpmnSequenceElement>? nextElements = null
        ) : base(self, previousElements, nextElements)
        {

        }
    }
}
