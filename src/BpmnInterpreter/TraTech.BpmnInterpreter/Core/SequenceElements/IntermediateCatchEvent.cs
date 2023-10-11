using System.Xml.Linq;

namespace TraTech.BpmnInterpreter.Core.SequenceElements
{
    public class IntermediateCatchEvent : BpmnSequenceElement
    {
        public static readonly string ElementTypeName = "intermediateCatchEvent";

        public IntermediateCatchEvent(
            XElement self,
            IEnumerable<BpmnSequenceElement>? previousElements = null,
            IEnumerable<BpmnSequenceElement>? nextElements = null
        ) : base(self, previousElements, nextElements)
        {

        }
    }
}
