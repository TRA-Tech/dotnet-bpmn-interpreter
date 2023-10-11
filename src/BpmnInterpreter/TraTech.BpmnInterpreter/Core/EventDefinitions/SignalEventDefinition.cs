using System.Xml.Linq;
using TraTech.BpmnInterpreter.Core.Elements;

namespace TraTech.BpmnInterpreter.Core.EventDefinitions
{
    public class SignalEventDefinition : BpmnElement
    {
        public static readonly string EventDefinitionTypeName = "signalEventDefinition";

        public SignalEventDefinition(XElement self) : base(self)
        {
        }

        public static SignalEventDefinition Parse(XElement eventDefinition)
        {
            return new SignalEventDefinition(eventDefinition);
        }
    }
}
