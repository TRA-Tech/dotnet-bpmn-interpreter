using System.Xml.Linq;

namespace TraTech.BpmnInterpreter.Core.Elements
{
    /// <summary>
    /// Represents a BPMN data object reference, which provides a way to reuse data objects in different parts of a process.
    /// </summary>
    public class DataObjectReference : BpmnElement
    {
        /// <summary>
        /// The BPMN element type name for a data object reference.
        /// </summary>
        public static readonly string ElementTypeName = "dataObjectReference";

        /// <summary>
        /// Gets the ID of the referenced data object.
        /// </summary>
        public string DataObjectRef { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="DataObjectReference"/> class.
        /// </summary>
        /// <param name="self">The XML element representing the data object reference.</param>
        /// <exception cref="InvalidOperationException">Thrown when the dataObjectRef attribute is missing.</exception>
        public DataObjectReference(XElement self) : base(self)
        {
            DataObjectRef = self.Attribute("dataObjectRef")?.Value ?? throw new InvalidOperationException("self has no dataObjectRef attribute");
        }
    }
}
