using System.Xml.Linq;

namespace TraTech.BpmnInterpreter.Core.Elements
{
    /// <summary>
    /// Represents a BPMN data store reference.
    /// </summary>
    public class DataStoreReference : BpmnElement
    {
        /// <summary>
        /// The BPMN element type name for a data store reference.
        /// </summary>
        public static readonly string ElementTypeName = "dataStoreReference";

        /// <summary>
        /// Initializes a new instance of the <see cref="DataStoreReference"/> class.
        /// </summary>
        /// <param name="self">The XML element representing the data store reference.</param>
        public DataStoreReference(XElement self) : base(self)
        {

        }
    }
}
