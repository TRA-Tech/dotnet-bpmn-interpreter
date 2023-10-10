using System.Xml.Linq;

namespace TraTech.BpmnInterpreter.Core.SequenceElements
{
    public class StartEvent : BpmnSequenceElement
    {

        public static readonly string ElementTypeName = "startEvent";

        public StartEvent(XElement self, IEnumerable<BpmnSequenceElement>? nextElements = null) : base(self, null, nextElements)
        {

        }
    }
}
