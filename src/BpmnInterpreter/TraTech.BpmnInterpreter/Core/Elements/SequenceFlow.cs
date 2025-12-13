using System.Xml.Linq;

namespace TraTech.BpmnInterpreter.Core.Elements
{
    /// <summary>
    /// Represents a BPMN sequence flow element, connecting two elements in a process.
    /// </summary>
    public class SequenceFlow : BpmnElement
    {
        /// <summary>
        /// The BPMN element type name for a sequence flow.
        /// </summary>
        public static readonly string ElementTypeName = "sequenceFlow";

        /// <summary>
        /// Gets the ID of the source element.
        /// </summary>
        public string SourceRef { get; private set; }

        /// <summary>
        /// Gets the ID of the target element.
        /// </summary>
        public string TargetRef { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="SequenceFlow"/> class.
        /// </summary>
        /// <param name="self">The XML element representing the sequence flow.</param>
        /// <exception cref="InvalidOperationException">Thrown when sourceRef or targetRef attributes are missing.</exception>
        public SequenceFlow(XElement self) : base(self)
        {
            SourceRef = self.Attribute("sourceRef")?.Value ?? throw new InvalidOperationException("self has no sourceRef attribute");
            TargetRef = self.Attribute("targetRef")?.Value ?? throw new InvalidOperationException("self has no targetRef attribute");
        }
    }
}
