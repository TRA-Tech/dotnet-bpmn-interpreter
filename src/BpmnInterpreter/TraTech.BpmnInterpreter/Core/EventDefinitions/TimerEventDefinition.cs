using System.Xml.Linq;
using TraTech.BpmnInterpreter.Core.Elements;

namespace TraTech.BpmnInterpreter.Core.EventDefinitions
{
    public class TimerEventDefinition : BpmnElement
    {
        public static readonly string EventDefinitionTypeName = "timerEventDefinition";

        public TimerEventDefinition(XElement self) : base(self)
        {
        }

        public static TimerEventDefinition Parse(XElement eventDefinition)
        {
            return new TimerEventDefinition(eventDefinition);
        }
    }
}
