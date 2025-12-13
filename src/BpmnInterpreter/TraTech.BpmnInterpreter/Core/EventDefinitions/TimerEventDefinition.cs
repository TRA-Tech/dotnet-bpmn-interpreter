using System.Xml.Linq;
using TraTech.BpmnInterpreter.Core.Elements;

namespace TraTech.BpmnInterpreter.Core.EventDefinitions
{
    /// <summary>
    /// Represents a BPMN timer event definition.
    /// </summary>
    public class TimerEventDefinition : BpmnElement
    {
        /// <summary>
        /// The type name of the event definition.
        /// </summary>
        public static readonly string EventDefinitionTypeName = "timerEventDefinition";

        /// <summary>
        /// Initializes a new instance of the <see cref="TimerEventDefinition"/> class.
        /// </summary>
        /// <param name="self">The XML element representing the timer event definition.</param>
        public TimerEventDefinition(XElement self) : base(self)
        {
        }

        /// <summary>
        /// Parses the specified XML element into a <see cref="TimerEventDefinition"/>.
        /// </summary>
        /// <param name="eventDefinition">The XML element to parse.</param>
        /// <returns>A new instance of <see cref="TimerEventDefinition"/>.</returns>
        public static TimerEventDefinition Parse(XElement eventDefinition)
        {
            return new TimerEventDefinition(eventDefinition);
        }
    }
}
