using System.Xml.Linq;

namespace TraTech.BpmnInterpreter.Core.BpmnSequenceElements
{
    public class StartEvent : BpmnSequenceElement
    {

        public static readonly string ElementTypeName = "startEvent";

        public StartEvent(XElement self, IEnumerable<BpmnSequenceElement>? nextElements = null) : base(self, null, nextElements)
        {

        }
    }
}
