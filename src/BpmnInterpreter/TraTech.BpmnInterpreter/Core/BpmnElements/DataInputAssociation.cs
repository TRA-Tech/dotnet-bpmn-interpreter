using System.Xml.Linq;

namespace TraTech.BpmnInterpreter.Core.BpmnElements
{
    public class DataInputAssociation : BpmnElement
    {
        public static readonly string ElementTypeName = "dataInputAssociation";

        public string SourceRef { get; private set; }
        public string TargetRef { get; private set; }

        public DataInputAssociation(XElement self) : base(self)
        {
            SourceRef = self.Element(Namespace.GetName("sourceRef"))?.Value ?? throw new InvalidOperationException("self has no sourceRef element");
            TargetRef = self.Element(Namespace.GetName("targetRef"))?.Value ?? throw new InvalidOperationException("self has no targetRef element");
        }
    }
}
