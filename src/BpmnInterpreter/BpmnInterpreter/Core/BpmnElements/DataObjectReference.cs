using System.Xml.Linq;

namespace BpmnInterpreter.Core.BpmnElements
{
    public class DataObjectReference : BpmnElement
    {
        public static readonly string ElementTypeName = "dataObjectReference";

        public string DataObjectRef { get; private set; }

        public DataObjectReference(XElement self) : base(self)
        {
            DataObjectRef = self.Attribute("dataObjectRef")?.Value ?? throw new InvalidOperationException("self has no dataObjectRef attribute");
        }
    }
}
