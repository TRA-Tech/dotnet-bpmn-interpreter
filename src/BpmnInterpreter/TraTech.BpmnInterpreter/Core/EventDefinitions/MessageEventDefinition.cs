using System.Xml.Linq;
using TraTech.BpmnInterpreter.Core.Elements;

namespace TraTech.BpmnInterpreter.Core.EventDefinitions
{
    public class MessageEventDefinition : BpmnElement
    {
        public static readonly string EventDefinitionTypeName = "messageEventDefinition";

        public MessageEventDefinition(XElement self) : base(self)
        {
        }

        public static MessageEventDefinition Parse(XElement eventDefinition)
        {
            return new MessageEventDefinition(eventDefinition);
        }
    }
}
