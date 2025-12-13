using System.Xml.Linq;
using TraTech.BpmnInterpreter.Core.Elements;

namespace TraTech.BpmnInterpreter.Core.EventDefinitions
{
    /// <summary>
    /// Represents a BPMN message event definition.
    /// </summary>
    public class MessageEventDefinition : BpmnElement
    {
        /// <summary>
        /// The type name of the event definition.
        /// </summary>
        public static readonly string EventDefinitionTypeName = "messageEventDefinition";

        /// <summary>
        /// Initializes a new instance of the <see cref="MessageEventDefinition"/> class.
        /// </summary>
        /// <param name="self">The XML element representing the message event definition.</param>
        public MessageEventDefinition(XElement self) : base(self)
        {
        }

        /// <summary>
        /// Parses the specified XML element into a <see cref="MessageEventDefinition"/>.
        /// </summary>
        /// <param name="eventDefinition">The XML element to parse.</param>
        /// <returns>A new instance of <see cref="MessageEventDefinition"/>.</returns>
        public static MessageEventDefinition Parse(XElement eventDefinition)
        {
            return new MessageEventDefinition(eventDefinition);
        }
    }
}
