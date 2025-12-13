using System.Xml.Linq;
using TraTech.BpmnInterpreter.Core.Elements;

namespace TraTech.BpmnInterpreter.Core.EventDefinitions
{
    /// <summary>
    /// Represents a BPMN signal event definition.
    /// </summary>
    public class SignalEventDefinition : BpmnElement
    {
        /// <summary>
        /// The type name of the event definition.
        /// </summary>
        public static readonly string EventDefinitionTypeName = "signalEventDefinition";

        /// <summary>
        /// Initializes a new instance of the <see cref="SignalEventDefinition"/> class.
        /// </summary>
        /// <param name="self">The XML element representing the signal event definition.</param>
        public SignalEventDefinition(XElement self) : base(self)
        {
        }

        /// <summary>
        /// Parses the specified XML element into a <see cref="SignalEventDefinition"/>.
        /// </summary>
        /// <param name="eventDefinition">The XML element to parse.</param>
        /// <returns>A new instance of <see cref="SignalEventDefinition"/>.</returns>
        public static SignalEventDefinition Parse(XElement eventDefinition)
        {
            return new SignalEventDefinition(eventDefinition);
        }
    }
}
