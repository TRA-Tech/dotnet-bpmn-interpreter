using System.Xml.Linq;

namespace TraTech.BpmnInterpreter.Core.Elements
{
    /// <summary>
    /// Represents a BPMN data input association, which defines how data is provided to an activity.
    /// </summary>
    public class DataInputAssociation : BpmnElement
    {
        /// <summary>
        /// The BPMN element type name for a data input association.
        /// </summary>
        public static readonly string ElementTypeName = "dataInputAssociation";

        private readonly string sourceRef = null!;
        private readonly string targetRef = null!;

        /// <summary>
        /// Gets the ID of the source element providing the data.
        /// </summary>
        public string SourceRef { get => sourceRef; }

        /// <summary>
        /// Gets the ID of the target element receiving the data.
        /// </summary>
        public string TargetRef { get => targetRef; }

        /// <summary>
        /// Initializes a new instance of the <see cref="DataInputAssociation"/> class.
        /// </summary>
        /// <param name="self">The XML element representing the data input association.</param>
        /// <exception cref="InvalidOperationException">Thrown when sourceRef or targetRef elements are missing.</exception>
        public DataInputAssociation(XElement self) : base(self)
        {
            sourceRef = self.Element(Namespace.GetName(nameof(sourceRef)))?.Value ?? throw new InvalidOperationException($"self has no {nameof(sourceRef)} element");
            targetRef = self.Element(Namespace.GetName(nameof(targetRef)))?.Value ?? throw new InvalidOperationException($"self has no {nameof(targetRef)} element");
        }
    }
}
