using System.Xml.Linq;

namespace TraTech.BpmnInterpreter.Core.Elements
{
    /// <summary>
    /// Represents a BPMN data object.
    /// </summary>
    public class DataObject : BpmnElement
    {
        /// <summary>
        /// The BPMN element type name for a data object.
        /// </summary>
        public static readonly string ElementTypeName = "dataObject";

        /// <summary>
        /// Initializes a new instance of the <see cref="DataObject"/> class.
        /// </summary>
        /// <param name="self">The XML element representing the data object.</param>
        public DataObject(XElement self) : base(self)
        {

        }
    }
}
