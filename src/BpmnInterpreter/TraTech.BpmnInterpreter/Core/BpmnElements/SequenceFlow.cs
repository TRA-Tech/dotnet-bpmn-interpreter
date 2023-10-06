using System.Xml.Linq;

namespace TraTech.BpmnInterpreter.Core.BpmnElements
{
    public class SequenceFlow : BpmnElement
    {
        public static readonly string ElementTypeName = "sequenceFlow";

        public string SourceRef { get; private set; }
        public string TargetRef { get; private set; }

        public SequenceFlow(XElement self) : base(self)
        {
            SourceRef = self.Attribute("sourceRef")?.Value ?? throw new InvalidOperationException("self has no sourceRef attribute");
            TargetRef = self.Attribute("targetRef")?.Value ?? throw new InvalidOperationException("self has no targetRef attribute");
        }
    }
}
