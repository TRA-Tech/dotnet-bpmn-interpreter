using System.Xml.Linq;

namespace TraTech.BpmnInterpreter.Core.SequenceElements
{
    public class ExclusiveGateway : BpmnSequenceElement
    {

        public static readonly string ElementTypeName = "exclusiveGateway";

        public ExclusiveGateway(
            XElement self,
            IEnumerable<BpmnSequenceElement>? previousElements = null,
            IEnumerable<BpmnSequenceElement>? nextElements = null
        ) : base(self, previousElements, nextElements)
        {

        }
    }
}
